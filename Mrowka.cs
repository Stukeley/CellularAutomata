namespace CellularAutomata
{
	public enum Kierunek
	{
		Prawo, Lewo, Gora, Dol
	}

	public class Mrowka : Pole
	{
		public Kierunek Kierunek { get; private set; }

		public Mrowka(int x, int y, Kierunek kierunek) : base(x, y)
		{
			Kierunek = kierunek;
		}
	}
}
