namespace CellularAutomata
{
	public enum Kierunek
	{
		Gora, Lewo, Dol, Prawo
	}

	public class Mrowka : Pole
	{
		public Kierunek Kierunek { get; set; }

		public Mrowka(int x, int y, Kierunek kierunek) : base(x, y)
		{
			Kierunek = kierunek;
		}
	}
}
