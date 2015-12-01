using System.Collections.Generic;

namespace Corniel.Sudoku
{
	/// <summary>Represents the structure of a Sudoku puzzle.</summary>
	public abstract class SudokuPuzzle
	{
		/// <summary>Gets the Singleton instance of 2x2 puzzle.</summary>
		public static readonly SudokuPuzzle Puzzle2x2 = new SudokuPuzzle2x2();

		/// <summary>Gets the Singleton instance of 3x3 puzzle.</summary>
		public static readonly SudokuPuzzle Puzzle3x3 = new SudokuPuzzle3x3();

		/// <summary>Constructs a Sudoku puzzle based on its size.</summary>
		/// <param name="size">
		/// The size of a single square.
		/// </param>
		protected SudokuPuzzle(int size)
		{
			this.Size2 = size * size;

			Grid = new int[Size2, Size2];
			Regions = new List<SudokuRegion>();
			Lookup = new List<SudokuRegion>[Size2 * Size2];

			InitializeGrid(Size2);

			InitializeRowRegions(Size2);
			InitializeColumnRegions(Size2);
			InitializeSubSquareRegions(size);
			initializeLookup();
		}
		
		/// <summary>Initializes a square with its indexes.</summary>
		protected void InitializeGrid(int size2)
		{
			for (var x = 0; x < size2; x++)
			{
				for (var y = 0; y < size2; y++)
				{
					Grid[x, y] = x + y * size2;
				}
			}
		}
		
		/// <summary>Initializes all row regions.</summary>
		protected void InitializeRowRegions(int size2)
		{
			for (int row = 0; row < size2; row++)
			{
				var region = new SudokuRegion();
				Regions.Add(region);

				for (int width = 0; width < size2; width++)
				{
					region.Add(Grid[row, width]);
				}
			}
		}

		/// <summary>Initializes all column regions.</summary>
		protected void InitializeColumnRegions(int size2)
		{
			for (var column = 0; column < size2; column++)
			{
				var region = new SudokuRegion();
				Regions.Add(region);

				for (var height = 0; height < size2; height++)
				{
					region.Add(Grid[height, column]);
				}
			}
		}

		/// <summary>Initializes all sub square regions.</summary>
		protected void InitializeSubSquareRegions(int size)
		{
			for (var xSub = 0; xSub < size; xSub++)
			{
				for (var ySub = 0; ySub < size; ySub++)
				{
					var region = new SudokuRegion();
					Regions.Add(region);

					for (var x = 0; x < size; x++)
					{
						for (var y = 0; y < size; y++)
						{
							region.Add(Grid[xSub * size + x, ySub * size + y]);
						}
					}
				}
			}
		}
		
		/// <summary>Initializes a lookup to map each square to its corresponding regions.</summary>
		protected void initializeLookup()
		{
			for (var index = 0; index < Lookup.Length; index++)
			{
				Lookup[index] = new List<SudokuRegion>();
			}
			foreach (var region in Regions)
			{
				foreach (var square in region)
				{
					Lookup[square].Add(region);
				}
			}
		}

		/// <summary>All (distinct) regions.</summary>
		public List<SudokuRegion> Regions { get; protected set; }

		/// <summary>The lookup to map each square to its corresponding regions.</summary>
		public List<SudokuRegion>[] Lookup { get; protected set; }
		
		/// <summary>The grid with the corresponding indexes.</summary>
		protected int[,] Grid { get; set; }

		/// <summary>The highest index in the grid.</summary>
		public int MaximumIndex { get { return Grid.Length - 1; } }

		/// <summary>The squared size of a single sub square.</summary>
		public int Size2 { get; protected set; }
	}
}
