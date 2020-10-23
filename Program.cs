using System;

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

			int iter = 10;

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

			// DEBUG
			mrowka = false;
			input = "Wejscie.txt";
			output = "Wyjscie.txt";
			// END DEBUG

			// Stworzenie planszy

			Plansza plansza;

			if (!string.IsNullOrEmpty(input))
			{
				plansza = OperacjeIO.WczytajPlansze(input, mrowka);
			}
			else
			{
				plansza = new Plansza(8, 8);
				Operacje.LosujPlansze(plansza, mrowka);
			}

			Operacje.WypiszPlansze(plansza);


			for (int i = 0; i < iter; i++)
			{
				Console.WriteLine($"\nPokolenie {i + 1}");
				Console.WriteLine("Wpisz cokolwiek, by zobaczyc nastepne pokolenie");
				_ = Console.ReadKey();
				Console.Clear();

				plansza = Operacje.NastepnePokolenie(plansza);

				Operacje.WypiszPlansze(plansza);
			}

			if (!string.IsNullOrEmpty(output))
			{
				OperacjeIO.ZapiszPlansze(output, plansza, mrowka);
			}
			else
			{
				Console.WriteLine("Wykonano wszystkie iteracje, bez zapisu do pliku.");
			}
		}
	}
}
