using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Examinationsuppgift
{
	public partial class ServerForm : Form
	{
		// Strängar som representerar filnamn på de filer som data importeras från.
		public const string cardPath = "kortlista.txt";
		public const string userPath = "kundlista.txt";
		public static readonly string[] imgPath = new string[6] {"eldtomat.png", "dunderkatt.png", "kristallhäst.png", "överpanda.png", "grattis.png", "nos.png" };


		// Strängar som representerar felmeddelanden som användaren får.
		private string replyMsg;
		private string confirmMsg;
		private const string replyCardError = "Det angivna kortserienumret existerar ej eller har redan gjorts anspråk på.";
		private const string replyUserError = "Det existerar ingen användare med det ID-numret.";
		private const string replyMultiError = "Varken kortserienumret eller ID-numret existerar.";

		/* Listorna sorteras efter kortserienummer och användar-ID för att lättare kunna sökas igenom när programmet kontrollerar om de angivna uppgifterna är giltiga.
		   OrderBy-metoden använder sig av quick sort-algoritmen, vilket är viktigt om det finns ett stort antal element i listan.
		   I dagsläget är listorna med användare och kort så pass korta att det inte spelar någon roll, men det kan vara viktigt i framtiden. */
		//private List<Card> loadedCards = loadedData.loadedCards.OrderBy(card => card.cardID).ToList();
		//private List<User> loadedUsers = loadedData.loadedUsers.OrderBy(user => user.userID).ToList();
		private List<string> userBoxList = new List<string>();
		private List<string> cardBoxList = new List<string>();

		private const int port = 12345;
		private const string address = "127.0.0.1";
		private static IPAddress parsedAddress;

		TcpListener cardListener;
		TcpClient cardClient;
		List<TcpClient> connectedUsers = new List<TcpClient>();

		public ServerForm()
		{
			InitializeComponent();

			// Läser in kort- och användardata från filerna via FileLoader-klassen.
			FileLoader.LoadData(cardPath, userPath);

			// Sorterar listorna för senare användning.
			FileLoader.loadedCards.OrderBy(card => card.cardID);
			FileLoader.loadedUsers.OrderBy(user => user.userID);

			/* ListBox-elementen som representerar giltiga ID-nummer och kortserienummer fylls med uppgifter från listorna.
			   För att göra texten i ListBox-elementen mer lättläsliga skapas två listor med strängar utifrån datan i User- och Card-objekten. */
			foreach (User u in FileLoader.loadedUsers)
			{
				userBoxList.Add($"{ u.userName }, { u.userID }, { u.userLoc }");
			}
			boxUsers.DataSource = userBoxList;

			foreach (Card c in FileLoader.loadedCards)
			{
				cardBoxList.Add($"{ c.cardID }, { c.ToString() }");
			}
			boxCards.DataSource = cardBoxList;
			picLogo.Image = Image.FromFile(imgPath[5]);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			// Try-catch block som parsar strängen "address" till en IP-address och ser till att rätt uppgifter används när användaren kopplar upp sig.
			try
			{
				parsedAddress = IPAddress.Parse(address);
				cardListener = new TcpListener(parsedAddress, port);
				cardListener.Start();
			}
			catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

			// När uppgifterna kontrollerats så stängs startknappen av, och koden som kopplar användaren till servern körs.
			btnStart.Enabled = false;
			btnStart.BackColor = Color.Green;
			StartConnection();
		}

		// Upprättar en anslutning till användaren och kallar sedan metoden som låter användaren mata in information.
		// Samtilga metoder som jobbar direkt mot användaren är asynkrona för att tillåta att flera användare är uppkopplade samtidigt.
		private async void StartConnection()
		{
			try
			{
				cardClient = await cardListener.AcceptTcpClientAsync();
			}
			catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

			ReadData(cardClient);
		}

		private async void ReadData(TcpClient c)
		{
			byte[] buffer = new byte[1024];
			int b;
			// All kod som jobbar över anslutningen mot användaren finns i try-catch-satser för att fånga upp eventuella fel utan att servern kraschar.
			try
			{
				b = await c.GetStream().ReadAsync(buffer, 0, buffer.Length);
			}
			catch (Exception error) { MessageBox.Show(error.Message, Text); return; }

			// Den mottagna datan delas direkt in i två strängar som representerar kortserienummer respektive användar-ID.
			// 
			string[] recievedData = Encoding.Unicode.GetString(buffer, 0, b).Split(new string[] { "-" }, StringSplitOptions.None);
			try
			{
				SendData(recievedData[0], recievedData[1], cardClient);
			}
			catch (Exception error) { MessageBox.Show(error.Message, Text); return;  }


			// Metoden kallas igen för att låta användaren mata in flera kort om hen så önskar.
			ReadData(c);
		}

		private async void SendData(string user, string card, TcpClient client)
		{
			int cardIndex = -1;
			int userIndex = -1;
			byte[] outData;

			/* Listorna med användare och giltiga kortserienummer söks efter de värden som användaren har skickat.
			   If-satsen strax under kollar om värdena hittades. Om båda strängarna hittades så har inlösningen av kortet lyckats.
			   Om en eller flera av strängarna inte hittas så skickas ett felmeddelande beroende på vilken sträng som saknades.  */
			cardIndex = FileLoader.loadedCards.FindIndex(i => i.cardID == card);
			userIndex = FileLoader.loadedUsers.FindIndex(u => u.userID == user);
			// If-satsen nedan kollar att båda uppgifterna stämmer.
			if (cardIndex >= 0 && userIndex >= 0)
			{
				picGolden.Image = Image.FromFile(imgPath[4]);
				picCard.Image = Image.FromFile(FileLoader.loadedCards[cardIndex].cardImgPath);

				/* Meddelandet som skickas anpassas beroende på anvädaruppgifter och vilket kortserienummer som löstes in.
				   Card-objektet med det serienumret tas sedan bort ur listorna och raderas från textfilen så att det inte kan lösas in igen.*/
				replyMsg = $"Grattis { FileLoader.loadedUsers[userIndex].ToString() }, du har vunnit en { FileLoader.loadedCards[cardIndex].ToString() }!";
				confirmMsg = $"{ FileLoader.loadedUsers[userIndex].ToString() } har löst in { FileLoader.loadedCards[cardIndex].cardID } ({ FileLoader.loadedCards[cardIndex].ToString() })";

				FileLoader.loadedCards.RemoveAt(cardIndex);
				cardBoxList.RemoveAt(cardIndex);
				DeleteLine(cardIndex, cardPath);

				boxCards.DataSource = null;
				boxCards.DataSource = cardBoxList;
			}
			else
			{
				// Om en eller flera uppgifter är felaktiga så kollar den här if-satsen om båda eller en av dem är fel, och anpassar meddelandet som skickas därefter.
				if (userIndex + cardIndex <= -1)
				{
					replyMsg = replyMultiError;
					confirmMsg = $"En användare angav ett felaktigt ID och kortserienummer.";
				}
				else if (cardIndex <= -1)
				{
					replyMsg = replyCardError;
					confirmMsg = $"{ FileLoader.loadedUsers[userIndex].userName } ({ FileLoader.loadedUsers[userIndex].userID }) försökte lösa in ett felaktigt kortserienummer.";
				}
				else if (userIndex <= -1)
				{
					replyMsg = replyUserError;
					confirmMsg = $"En okänd användare försökte lösa in { FileLoader.loadedCards[cardIndex].cardID } ({ FileLoader.loadedCards[cardIndex].ToString() })";
				}
			}
			/* Ett meddelande som visar vilken användare som har löst in vilket kort och huruvida det lyckades eller inte visas på servern.
			   Ett annat meddelande skickas till NOS_Export-programmet som låter användaren veta om det lyckades eller ej.*/
			boxMsg.Text = confirmMsg;
			outData = Encoding.Unicode.GetBytes(replyMsg);
			try
			{
				await client.GetStream().WriteAsync(outData, 0, outData.Length);
			}
			catch (Exception error) { MessageBox.Show(error.Message, Text); return; }
		}

		private void DeleteLine(int index, string path)
		{
			/* För att radera kortserienumret från textiflen läses den in som en lista.
			   Listan sorteras sedan eftersom att loadedCards sorteras när programmet startas, för att undvika att fel serienummer raderas ifall filen skulle vara osorterad.
			   När listan sorterats tas rätt index bort och den gamla filen skrivs över med data från den nya, sorterade listan.
			   
			   Denna metod för att skriva data till en fil kan klart förbättras. Den funkar bra för mindre filer, t.ex. testfilen som inkluderas i denna prototyp
			   Dock skulle det vara mycket ineffektivt att läsa in all data från en större fil på det här sättet, och skulle kunna leda till prestandaproblem om filen är mycket stor.
			   Ett exempel på detta skulle kunna vara om samlarkorten växer i popularitet och antalet guldkort som trycks växer. */
			List<string> fileList = new List<string>(File.ReadAllLines(path, Encoding.Default));
			fileList.Sort();
			fileList.RemoveAt(index);
			File.WriteAllLines(path, fileList.ToArray(), Encoding.Default);
		}

		private void ServerForm_Load(object sender, EventArgs e)
		{

		}
	}
}
