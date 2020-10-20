namespace CellularAutomata
{
	public enum Stan
	{
		Zywa, Martwa
	}

	public class Komorka : Pole
	{
		public Stan Stan { get; private set; }

		public Komorka(int x, int y, Stan stan) : base(x, y)
		{
			Stan = stan;
		}
	}
}
