using System;

namespace KoloQR
{
	public class Ocena
	{
		public int id; 
		public string index = "brak Twojego numeru";
		public string przedmiot = "brak";
		public string nazwaSprawdzianu = "brak";
		public int ocena = 0;
		public string uwagi = "brak";
		public string data = "";

		public bool spojneDane(string ciurek)
		{
			string[] podzielone = ciurek.Split(new Char[] { '|', '&' });
			if (podzielone.Length != 4) {
				return false;
			} else {
				this.id = int.Parse (podzielone [0]);
				this.przedmiot = podzielone [1];
				this.nazwaSprawdzianu = podzielone [2];
				this.uwagi = podzielone [3];
				return true;
			}
		}

		public void osobnik(string i, int o)
		{
			this.index = i;
			this.ocena = o;
			Console.WriteLine("dodano ocene");
		}

		public bool wyszukajWynik(string ocenaZqr, string index)
		{
			string[] metallica = ocenaZqr.Split(new Char[] { '&' });

			string[] wyniki = metallica[1].Split('#');//tutaj sa wyniki i indexy

			foreach(string wynik in wyniki)
			{
				string[] podzielone = wynik.Split('|');
				if (podzielone[0] == index)
				{
					osobnik(podzielone[0], int.Parse(podzielone[1]));
					if (spojneDane (metallica [0])) {
						return true;
					}
				}
			}
			return false;
		}
	}

	public class Historia
	{
		public string przedmiot { get; set;}
		public string nazwaSprawdzianu { get; set;}
		public int ocena { get; set;}
		public string uwagi { get; set;}
		public string data { get; set;}
	}
}

