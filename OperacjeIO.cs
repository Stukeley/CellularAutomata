using System.IO;

namespace CellularAutomata
{
	public static class OperacjeIO
	{
		public static Plansza WczytajPlansze(string nazwa, bool mrowka)
		{
			Plansza plansza = null;

			if (mrowka)
			{
			}
			else
			{
				// Gra w Zycie
				string[] linie = File.ReadAllLines(nazwa);

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

		public static void ZapiszPlansze(string nazwa, Plansza plansza, bool mrowka)
		{
			if (mrowka)
			{
			}
			else
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
		}
	}
}
