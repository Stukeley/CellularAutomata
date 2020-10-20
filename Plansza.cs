namespace CellularAutomata
{
	public class Plansza
	{
		public Pole[,] Pola { get; private set; }

		public Plansza(int dimX, int dimY)
		{
			Pola = new Pole[dimX, dimY];
		}
	}
}
