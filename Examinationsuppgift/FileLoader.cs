using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Examinationsuppgift
{
	public static class FileLoader
	{
		public static List<Card> loadedCards = new List<Card>();
		public static List<User> loadedUsers = new List<User>();

		public static void LoadData(string indataCardPath, string indataUserPath)
		{
			if (File.Exists(indataCardPath) && File.Exists(indataUserPath)) // Kontrollerar att det finns tillgängliga filer med de angivna namnen innan vi importerar data.
			{
				List<string> ItemSaver = new List<string>(); // En lista med strängar används för att tillfälligt lagra den importerade datan innan vi separerar strängarna. 
				StreamReader CardReader = new StreamReader(indataCardPath, Encoding.Default, true); // StreamReader-klassen låter os öppna en ström för att läsa data från filen.
				StreamReader UserReader = new StreamReader(indataUserPath, Encoding.Default, true);

				// Kortdata importeras först.
				// Den här loopen lägger till varje rad som en egen sträng i ItemSaver.
				// Slutet på textfilen känns av genom att avsluta loopen så fort en rad inte har något värde alls.
				string Item;
				while ((Item = CardReader.ReadLine()) != null)
				{
					ItemSaver.Add(Item);
				}

				// Går igenom alla strängar i ItemSaver och separerar dem baserat på var separatorerna ligger.
				foreach (string a in ItemSaver)
				{
					string[] vector = a.Split(new string[] { "###" }, StringSplitOptions.None);

					// Det första indexet i vektorn representerar korttypen, och används därför för att skapa objekt av rätt typ.
					switch (vector[1])
					{
						case "Dunderkatt":
							loadedCards.Add(new ThunderCat(vector[0]));
							break;

						case "Kristallhäst":
							loadedCards.Add(new CrystalHorse(vector[0]));
							break;

						case "Överpanda":
							loadedCards.Add(new OverPanda(vector[0]));
							break;

						case "Eldtomat":
							loadedCards.Add(new FireTomato(vector[0]));
							break;

						default:
							break;
					}
				}
				// ItemSaver-listan rensas så att vi kan använda oss av den för att läsa in användardata.
				ItemSaver.Clear();
				while ((Item = UserReader.ReadLine()) != null)
				{
					ItemSaver.Add(Item);
				}
				// Användardatan delas upp på samma sätt som kortdatan. Vektorn innehåller fler index än den som används för att importera korten då det finns fler separatorer i filen med användardata.
				foreach (string a in ItemSaver)
				{
					string[] vector = a.Split(new string[] { "###" }, StringSplitOptions.None);
					loadedUsers.Add(new User(vector[0], vector[1], vector[2]));
				}

				// Stänger strömmarna när all data har lästs in.
				CardReader.Close();
				UserReader.Close();
			}
			else
			{
			}
		}
	}
}
