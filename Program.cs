using System;

namespace CellularAutomata
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//! Wczytanie przelacznikow
			// Przelaczniki maja ustawione bazowe wartosci w przypadku gdy nie sa podane

			// True - mrowka langtona
			// False - gra w zycie
			bool mrowka = false;

			int iter = 20;

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
#if DEBUG
			mrowka = true;
			input = "WejscieMrowka.txt";
			output = "WyjscieMrowka.txt";
#endif
			// END DEBUG

			// Stworzenie planszy

			Plansza plansza;
			Mrowka mrowek;

			if (!string.IsNullOrEmpty(input))
			{
				plansza = OperacjeIO.WczytajPlansze(input, mrowka);
				mrowek = OperacjeIO.WczytajMrowke(input);
			}
			else
			{
				plansza = new Plansza(8, 8);
				Operacje.LosujPlansze(plansza, mrowka);
				mrowek = Operacje.LosujMrowke(plansza);
			}

			if (mrowka)
			{
				for (int i = 0; i < iter; i++)
				{
					Operacje.WypiszPlanszeMrowka(plansza, mrowek);

					Console.WriteLine($"\nIteracja {i + 1}");
					Console.WriteLine("Wpisz cokolwiek, by zobaczyc nastepny ruch mrowki");
					_ = Console.ReadKey();
					Console.Clear();

					plansza = Operacje.NastepnaIteracja(plansza, mrowek);
				}
			}
			else
			{
				for (int i = 0; i < iter; i++)
				{
					Operacje.WypiszPlansze(plansza);

					Console.WriteLine($"\nPokolenie {i + 1}");
					Console.WriteLine("Wpisz cokolwiek, by zobaczyc nastepne pokolenie");
					_ = Console.ReadKey();
					Console.Clear();

					plansza = Operacje.NastepnePokolenie(plansza);
				}
			}

			if (!string.IsNullOrEmpty(output))
			{
				if (mrowka)
				{
					OperacjeIO.ZapiszPlanszeMrowka(output, plansza, mrowek);
				}
				else
				{
					OperacjeIO.ZapiszPlansze(output, plansza);
				}
			}
			else
			{
				Console.WriteLine("Wykonano wszystkie iteracje, bez zapisu do pliku.");
			}
		}
	}
}
