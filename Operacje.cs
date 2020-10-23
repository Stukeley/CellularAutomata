using System;

namespace CellularAutomata
{
	public static class Operacje
	{
		public static void LosujPlansze(Plansza plansza, bool mrowka)
		{
			var rng = new Random();

			if (mrowka)
			{
				for (int i = 0; i < plansza.Pola.GetLength(0); i++)
				{
					for (int j = 0; j < plansza.Pola.GetLength(1); j++)
					{
						Kolor kolor = rng.Next() % 2 == 0 ? Kolor.Bialy : Kolor.Czarny;

						plansza.Pola[i, j] = new Kratka(i, j, kolor);
					}
				}
			}
			else
			{
				for (int i = 0; i < plansza.Pola.GetLength(0); i++)
				{
					for (int j = 0; j < plansza.Pola.GetLength(1); j++)
					{
						Stan stan = rng.Next() % 2 == 0 ? Stan.Zywa : Stan.Martwa;

						plansza.Pola[i, j] = new Komorka(i, j, stan);
					}
				}
			}
		}

		// Wypisuje plansze do Gry w Zycie
		public static void WypiszPlansze(Plansza plansza)
		{
			for (int i = 0; i < plansza.Pola.GetLength(0); i++)
			{
				for (int j = 0; j < plansza.Pola.GetLength(1); j++)
				{
					char znak = (plansza.Pola[i, j] as Komorka).Stan == Stan.Zywa ? 'O' : 'X';
					Console.Write(znak);
					Console.Write(" ");
				}
				Console.Write("\n");
			}
		}

		// Wypisuje plansze do Mrowki Langtona
		public static void WypiszPlansze(Plansza plansza, Mrowka mrowka)
		{
			for (int i = 0; i < plansza.Pola.GetLength(0); i++)
			{
				for (int j = 0; j < plansza.Pola.GetLength(1); j++)
				{
					if (mrowka.X == i && mrowka.Y == j)
					{
						Console.Write('M');
						Console.Write(" ");
					}
					else
					{
						char znak = (plansza.Pola[i, j] as Kratka).Kolor == Kolor.Bialy ? '1' : '0';
						Console.Write(znak);
						Console.Write(" ");
					}
				}
				Console.Write("\n");
			}
		}

		// Oblicza ilosc sasiadow dla komorki o danych wspolrzednych na danej planszy (Gra w Zycie)
		// Zakladamy, ze komorki znajdujace sie na krawedziach maja sasiadow po wszystkich stronach (np. na samej gorze sasiaduja z tymi na samym dole)
		// Liczymy rowniez rogi (po skosie)
		public static int IloscSasiadow(Plansza plansza, int x, int y)
		{
			int ilosc = 0;

			int rozmiarX = plansza.Pola.GetLength(0);
			int rozmiarY = plansza.Pola.GetLength(1);

			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					int indexX = (x + i + rozmiarX) % rozmiarX;
					int indexY = (y + j + rozmiarY) % rozmiarY;

					ilosc += (int)(plansza.Pola[indexX, indexY] as Komorka).Stan;
				}
			}

			// Musimy uwzglednic komorke polozona na [x,y], ktora podliczylismy (a komorka nie jest dla samej siebie sasiadem)
			ilosc -= (int)(plansza.Pola[x, y] as Komorka).Stan;

			return ilosc;
		}

		// Generuje nastepne pokolenie dla danej planszy (Gra w Zycie)
		public static Plansza NastepnePokolenie(Plansza plansza)
		{
			Plansza kopia = plansza;

			for (int i = 0; i < plansza.Pola.GetLength(0); i++)
			{
				for (int j = 0; j < plansza.Pola.GetLength(1); j++)
				{
					int iloscSasiadow = IloscSasiadow(kopia, i, j);

					if (iloscSasiadow == 3)
					{
						(plansza.Pola[i, j] as Komorka).Stan = Stan.Zywa;
					}
					else if ((iloscSasiadow == 2 || iloscSasiadow == 3) && (plansza.Pola[i, j] as Komorka).Stan == Stan.Zywa)
					{
						continue;
					}
					else if (iloscSasiadow < 2 || iloscSasiadow > 3)
					{
						(plansza.Pola[i, j] as Komorka).Stan = Stan.Martwa;
					}
				}
			}

			return plansza;
		}
	}
}
