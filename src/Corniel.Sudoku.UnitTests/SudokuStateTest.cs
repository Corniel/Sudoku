using NUnit.Framework;

namespace Corniel.Sudoku.UnitTests
{
	[TestFixture]
	public class SudokuStateTest
	{
		[Test]
		public void Parse_2x2_ReturnSudokuState2x2()
		{
			var puzzle = SudokuState.Parse(@"
				.4|.1
				3.|..
				--+--
				..|.4
				..|..");

			var act = puzzle.GetType();
			var exp = typeof(SudokuState2x2);
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Parse_3x3_ReturnSudokuState3x3()
		{
			var puzzle = SudokuState.Parse(@"
				53.|.7.|...
				6..|195|...
				.98|...|.6.
				---+---+---
				8..|.6.|..3
				4..|8.3|..1
				7..|.2.|..6
				---+---|---
				.6.|...|28.
				...|419|..5
				...|.8.|.79");

			var act = puzzle.GetType();
			var exp = typeof(SudokuState3x3);
			Assert.AreEqual(exp,act);
		}
	}
}
