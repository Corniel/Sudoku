using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentleWare.Sudoku
{
	public class Puzzle
	{
		private List<Point> tiles;
		private List<TileGroup> distics;
		private SudokuValues values;
		
		public static Puzzle Create(int dimensions = 3)
		{
			var puzzle = new Puzzle();
			puzzle.tiles = new List<Point>();
			puzzle.values = new SudokuValues(dimensions);
			puzzle.distics = new List<TileGroup>();

			var dim = puzzle.values.Dimensions;
			var dim2 = puzzle.values.Dimensions2;

			for (var x = 0; x < dim2; x++)
			{
				for (var y = 0; y < dim2; y++)
				{
					puzzle.tiles.Add(new Point(x, y));
				}
			}

			// rows.
			for (int i = 0; i < dim2; i++)
			{
				var group = new TileGroup();
				puzzle.distics.Add(group);
				
				for (int w = 0; w < dim2; w++)
				{
					group.Add(i, w);
				}
			}
			// cols.
			for (int i = 0; i < dim2; i++)
			{
				var group = new TileGroup();
				puzzle.distics.Add(group);

				for (int h = 0; h < dim2; h++)
				{
					group.Add(h,i);
				}
			}
			// squares.
			for (var xBlock = 0; xBlock < dim; xBlock++)
			{
				for (var yBlock = 0; yBlock < dim; yBlock++)
				{
					var group = new TileGroup();
					puzzle.distics.Add(group);

					for (var x = 0; x < dim; x++)
					{
						for (var y = 0; y < dim; y++)
						{
							group.Add(xBlock * dim + x, yBlock * dim + y);
						}
					}
				}
			}
			return puzzle;
		}

	}
}
