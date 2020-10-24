namespace CellularAutomata
{
	public enum Kolor
	{
		Czarny, Bialy
	}

	public class Kratka : Pole
	{
		public Kolor Kolor { get; set; }

		public Kratka(int x, int y, Kolor kolor) : base(x, y)
		{
			Kolor = kolor;
		}
	}
}
