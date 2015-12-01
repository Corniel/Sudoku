using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentleWare.Sudoku
{
	public class TileGroup: List<Point>
	{
		public void Add(int x, int y) { Add(new Point(x, y));}
	}
}
