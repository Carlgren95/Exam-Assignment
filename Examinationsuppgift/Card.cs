using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examinationsuppgift
{
	// Card-klassen innehåller uppgifter om kortens serienummer och vilken sökväg bilderna som representerar de olika korten har.
	// ToString-overriden innehåller endast kortens namn då det är det enda som behövs när vi skickar ett grattismeddelande till användaren.
	public class Card
	{

		public string cardID;
		public string cardImgPath;

		public Card(string indatacardID)
		{
			cardID = indatacardID;
		}

	}

	public class FireTomato : Card
	{

		public FireTomato(string indatacardID) : base(indatacardID)
		{
			cardImgPath = ServerForm.imgPath[0];
		}

		public override string ToString()
		{
			{
				return "Eldtomat";
			}
		}

	}

	public class ThunderCat : Card
	{

		public ThunderCat(string indatacardID) : base(indatacardID)
		{
			cardImgPath = ServerForm.imgPath[1];
		}
		public override string ToString()
		{
			{
				return "Dunderkatt";
			}
		}

	}

	public class CrystalHorse : Card
	{

		public CrystalHorse(string indatacardID) : base(indatacardID)
		{
			cardImgPath = ServerForm.imgPath[2];
		}

		public override string ToString()
		{
			{
				return "Kristallhäst";
			}
		}

	}

	public class OverPanda : Card
	{

		public OverPanda(string indatacardID) : base(indatacardID)
		{
			cardImgPath = ServerForm.imgPath[3];
		}

		public override string ToString()
		{
			{
				return "Överpanda";
			}
		}
	}
}
