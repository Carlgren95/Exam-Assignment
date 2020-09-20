using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examinationsuppgift
{
	// User-klassen innehåller all användardata. userID representerar användarens ID-nummer och används för att se till att rätt användare vinner kortet när strängen tas emot av servern.
	// Övrig information om namn/ort används för att skicka grattismeddelandet och för att lättare identifiera dem i ListBox-elementet som visar registrerade användare.
	public class User
	{
		public string userID;
		public string userName;
		public string userLoc;

		public User(string indatauserID, string indatauserName, string indatauserLoc)
		{
			userID = indatauserID;
			userName = indatauserName;
			userLoc = indatauserLoc;
		}

		// En ToString-override används här för att göra koden mer lättläslig på andra ställen.
		public override string ToString()
		{
			return userName;
		}
	}
}
