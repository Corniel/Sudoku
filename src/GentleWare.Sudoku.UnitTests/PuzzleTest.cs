using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentleWare.Sudoku.UnitTests
{
	[TestFixture]
	public class PuzzleTest
	{
		[Test]
		public void Ctor_2_AreEqual()
		{
			var puzzle = Puzzle.Create(2);
		}

		[Test]
		public void Ctor_3_AreEqual()
		{
			var puzzle = Puzzle.Create(3);
		}
	}
}
