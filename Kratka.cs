﻿namespace CellularAutomata
{
	public enum Kolor
	{
		Bialy, Czarny
	}

	public class Kratka : Pole
	{
		public Kolor Kolor { get; private set; }

		public Kratka(int x, int y, Kolor kolor) : base(x, y)
		{
			Kolor = kolor;
		}
	}
}
