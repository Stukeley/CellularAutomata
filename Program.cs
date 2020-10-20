namespace CellularAutomata
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//! Wczytanie przelacznikow

			// True - mrowka langtona
			// False - gra w zycie
			bool mrowka = false;

			int iter = 0;

			string input = "";
			string output = "";

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "-automat")
				{
					string automat = args[++i].ToLower();
					if (automat == "mrowka")
					{
						mrowka = true;
					}
					else
					{
						mrowka = false;
					}
				}
				else if (args[i] == "-iteracje")
				{
					iter = int.Parse(args[++i]);
				}
				else if (args[i] == "-i")
				{
					input = args[++i];
				}
				else if (args[i] == "-o")
				{
					output = args[++i];
				}
			}

			//! End wczytanie przelacznikow
		}
	}
}
