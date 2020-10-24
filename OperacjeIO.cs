using System.IO;
using System.Linq;

namespace CellularAutomata
{
	public static class OperacjeIO
	{
		public static Plansza WczytajPlansze(string nazwa, bool mrowka)
		{
			Plansza plansza = null;

			if (mrowka)
			{
				// Mrowka Langtona - pomijamy puste linie
				string[] linie = File.ReadAllLines(nazwa).Where(x => !string.IsNullOrEmpty(x)).ToArray();

				plansza = new Plansza(linie[0].Length, linie.Length);

				for (int i = 0; i < linie.Length; i++)
				{
					for (int j = 0; j < linie[0].Length; j++)
					{
						if (linie[i][j] == 'M')
						{
							// Jesli w inpucie mamy mrowke na planszy, to zakladamy, ze pole, na ktorym stoi jest biale
							plansza.Pola[i, j] = new Kratka(i, j, Kolor.Bialy);
							continue;
						}

						Kolor kolor = linie[i][j] == '1' ? Kolor.Bialy : Kolor.Czarny;
						plansza.Pola[i, j] = new Kratka(i, j, kolor);
					}
				}
			}
			else
			{
				// Gra w Zycie - pomijamy puste linie
				string[] linie = File.ReadAllLines(nazwa).Where(x => !string.IsNullOrEmpty(x)).ToArray();

				plansza = new Plansza(linie[0].Length, linie.Length);

				for (int i = 0; i < linie.Length; i++)
				{
					for (int j = 0; j < linie[0].Length; j++)
					{
						Stan stan = linie[i][j] == 'O' ? Stan.Zywa : Stan.Martwa;
						plansza.Pola[i, j] = new Komorka(i, j, stan);
					}
				}
			}

			return plansza;
		}

		public static Mrowka WczytajMrowke(string nazwa)
		{
			string[] linie = File.ReadAllLines(nazwa).Where(x => !string.IsNullOrEmpty(x)).ToArray();

			for (int i = 0; i < linie.Length; i++)
			{
				for (int j = 0; j < linie[0].Length; j++)
				{
					if (linie[i][j] == 'M')
					{
						return new Mrowka(i, j, Kierunek.Gora);
					}
				}
			}

			// W pliku nie znaleziono mrowki - stawiamy ja w miejscu (0,0)
			return new Mrowka(0, 0, Kierunek.Gora);
		}

		public static void ZapiszPlansze(string nazwa, Plansza plansza)
		{
			// Gra w Zycie
			using (var writer = new StreamWriter(nazwa))
			{
				for (int i = 0; i < plansza.Pola.GetLength(0); i++)
				{
					for (int j = 0; j < plansza.Pola.GetLength(1); j++)
					{
						writer.Write((plansza.Pola[i, j] as Komorka).Stan == Stan.Zywa ? 'O' : 'X');
					}
					writer.WriteLine();
				}
			}
		}

		public static void ZapiszPlanszeMrowka(string nazwa, Plansza plansza, Mrowka mrowka)
		{
			// Mrowka Langtona
			using (var writer = new StreamWriter(nazwa))
			{
				for (int i = 0; i < plansza.Pola.GetLength(0); i++)
				{
					for (int j = 0; j < plansza.Pola.GetLength(1); j++)
					{
						if ((i, j) == (mrowka.X, mrowka.Y))
						{
							writer.Write('M');
							continue;
						}

						writer.Write((plansza.Pola[i, j] as Kratka).Kolor == Kolor.Bialy ? '1' : '0');
					}
					writer.WriteLine();
				}
			}
		}
	}
}
