using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentleWare.Sudoku
{
	public class SudokuValues
	{
		public SudokuValues(int dimensions = 3)
		{
			var dim2 = dimensions * dimensions;
			values = new SudokuValue[dim2, dim2];
		}

		private SudokuValue[,] values;

		public int Dimensions { get { return (int)Math.Sqrt(values.GetLength(0)); } }
		public int Dimensions2 { get { return values.GetLength(0); } }

		public SudokuValue this[Point pt] { get { return values[pt.X, pt.Y]; } }

		public SudokuValues Copy()
		{
			var copy = new SudokuValues();
			copy.values = new SudokuValue[this.Dimensions2, this.Dimensions2];
			Array.Copy(this.values, copy.values, this.values.Length);
			return copy;
		}
	}
}
