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
				// Najpierw losujemy pozycje mrowki
				int mrowkaX = rng.Next(0, plansza.Pola.GetLength(0));
				int mrowkaY = rng.Next(0, plansza.Pola.GetLength(1));

				// Mrowka poczatkowo obrocona w gore
				var mrowek = new Mrowka(mrowkaX, mrowkaY, Kierunek.Gora);

				plansza.Pola[mrowkaX, mrowkaY] = mrowek;

				for (int i = 0; i < plansza.Pola.GetLength(0); i++)
				{
					for (int j = 0; j < plansza.Pola.GetLength(1); j++)
					{
						if ((i, j) == (mrowkaX, mrowkaY))
						{
							continue;
						}

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

					if (znak == 'O')
					{
						Console.ForegroundColor = ConsoleColor.Green;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
					}

					Console.Write(znak);
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write(" ");
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

		// Wypisuje plansze do Mrowki Langtona
		public static void WypiszPlanszeMrowka(Plansza plansza, Mrowka mrowka)
		{
			for (int i = 0; i < plansza.Pola.GetLength(0); i++)
			{
				for (int j = 0; j < plansza.Pola.GetLength(1); j++)
				{
					if ((i, j) == (mrowka.X, mrowka.Y))
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write('M');
					}
					else
					{
						char znak = (plansza.Pola[i, j] as Kratka).Kolor == Kolor.Bialy ? '1' : '0';

						if (znak == '1')
						{
							Console.ForegroundColor = ConsoleColor.White;
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Blue;
						}

						Console.Write(znak);
					}

					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write(' ');
				}

				Console.Write('\n');
			}
		}

		// Losuje mrowke w przypadku, gdy nie zostala ona podana w pliku wejsciowym
		public static Mrowka LosujMrowke(Plansza plansza)
		{
			Random rng = new Random();

			int mrowkaX = rng.Next(0, plansza.Pola.GetLength(0));
			int mrowkaY = rng.Next(0, plansza.Pola.GetLength(1));

			Mrowka mrowka = new Mrowka(mrowkaX, mrowkaY, Kierunek.Gora);

			return mrowka;
		}

		// Porusza mrowka do przodu o jedno pole (Mrowka Langtona)
		public static void RuchDoPrzodu(Plansza plansza, Mrowka mrowka)
		{
			// Obliczamy nowa pozycje mrowki bazujac na kierunku, w ktorym jest odwrocona

			// Na start ustawiamy nowe wartosci na te poprzednie
			int noweX = mrowka.X, noweY = mrowka.Y;
			switch (mrowka.Kierunek)
			{
				case Kierunek.Gora:

					// x-1, y bez zmian
					noweX = (mrowka.X - 1 + plansza.Pola.GetLength(1)) % plansza.Pola.GetLength(1);
					break;

				case Kierunek.Lewo:

					// y-1, x bez zmian
					noweY = (mrowka.Y - 1 + plansza.Pola.GetLength(0)) % plansza.Pola.GetLength(0);
					break;

				case Kierunek.Dol:

					// x+1, y bez zmian
					noweX = (mrowka.X + 1 + plansza.Pola.GetLength(1)) % plansza.Pola.GetLength(1);
					break;

				case Kierunek.Prawo:

					// y+1, x bez zmian
					noweY = (mrowka.Y + 1 + plansza.Pola.GetLength(0)) % plansza.Pola.GetLength(0);
					break;
			}

			// Ustawiamy nowa pozycje
			mrowka.X = noweX;
			mrowka.Y = noweY;
		}

		// Generuje nastepna iteracje planszy dla Mrowki Langtona
		public static Plansza NastepnaIteracja(Plansza plansza, Mrowka mrowka)
		{
			Kratka k = plansza.Pola[mrowka.X, mrowka.Y] as Kratka;

			// Sprawdzamy kolor pola, na ktorym znajduje sie mrowka
			if (k.Kolor == Kolor.Bialy)
			{
				// Obrot w lewo - +1 (% sprawia ze nigdy nie "wyskoczymy" poza mozliwe kierunki) 0->1->2->3->0->1->...
				mrowka.Kierunek = (Kierunek)((int)(mrowka.Kierunek + 1) % 4);

				// Zmiana koloru pola
				k.Kolor = Kolor.Czarny;

				// Ruch do przodu
				RuchDoPrzodu(plansza, mrowka);
			}
			else
			{
				// Obrot w prawo - -1 (musimy sie upewnic, ze Kierunek nie jest ujemny)
				mrowka.Kierunek = mrowka.Kierunek - 1 > 0 ? mrowka.Kierunek - 1 : mrowka.Kierunek + 3;

				// Zmiana koloru pola
				k.Kolor = Kolor.Bialy;

				// Ruch do przodu
				RuchDoPrzodu(plansza, mrowka);
			}

			return plansza;
		}
	}
}
