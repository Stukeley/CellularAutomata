namespace CellularAutomata
{
	public enum Stan
	{
		Martwa, Zywa
	}

	public class Komorka : Pole
	{
		public Stan Stan { get; set; }

		public Komorka(int x, int y, Stan stan) : base(x, y)
		{
			Stan = stan;
		}
	}
}
