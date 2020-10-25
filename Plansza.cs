namespace CellularAutomata
{
	public class Plansza
	{
		public Pole[,] Pola { get; private set; }

		public Plansza(int dimX, int dimY)
		{
			Pola = new Pole[dimX, dimY];
		}

		public Pole this[int index1, int index2]
		{
			get
			{
				return Pola[index1, index2];
			}
			set
			{
				Pola[index1, index2] = value;
			}
		}

		// Kopiuje plansze, by wykonac wszystkie operacje jednoczesnie
		public Plansza KopiujPlansze()
		{
			Plansza nowa = new Plansza(Pola.GetLength(0), Pola.GetLength(1));

			for (int i = 0; i < Pola.GetLength(0); i++)
			{
				for (int j = 0; j < Pola.GetLength(1); j++)
				{
					nowa.Pola[i, j] = Pola[i, j];
				}
			}

			return nowa;
		}
	}
}
