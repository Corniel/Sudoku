using NUnit.Framework;

namespace Corniel.Sudoku.UnitTests
{
	[TestFixture]
	public class SudokuSolverTest
	{
		[Test]
		public void Solve_2x2_SolvedPuzzle()
		{
			var puzzle = SudokuState.Parse(@"
				.4|.1
				3.|..
				--+--
				..|.4
				..|..");

			var solver = new SudokuSolver(SudokuPuzzle.Puzzle2x2);

			var act = solver.Solve(puzzle);
			var exp = SudokuState.Parse(@"
				24|31
				31|42
				--+--
				13|24
				42|13");

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Solve_3x3WikipediaExample_SolvedPuzzle()
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

			var solver = new SudokuSolver(SudokuPuzzle.Puzzle3x3);

			var act = solver.Solve(puzzle);
			var exp = SudokuState.Parse(@"
				534|678|912
				672|195|348
				198|342|567
				---+---+---
				859|761|423
				426|853|791
				713|924|856
				---+---+---
				961|537|284
				287|419|635
				345|286|179");

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Solve_3x3Level4_SolvedPuzzle()
		{
			var puzzle = SudokuState.Parse(@"
				6..|..4|..3
				..5|7.6|.1.
				.1.|...|7..
				---+---+---
				...|32.|6..
				75.|8..|.2.
				..4|1..|.9.
				---+---+---
				4..|5..|..7
				.6.|...|1..
				3..|69.|..2");

			var solver = new SudokuSolver(SudokuPuzzle.Puzzle3x3);

			var act = solver.Solve(puzzle);
			var exp = SudokuState.Parse(@"
				672|914|583
				845|736|219
				913|258|746
				---+---+---
				198|325|674
				756|849|321
				234|167|895
				---+---+---
				421|583|967
				569|472|138
				387|691|452");

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Solve_3x3WorldsHardestStudokuAccordingToTheTelegraph_SolvedPuzzle()
		{
			var puzzle = SudokuState.Parse(@"
				8..|...|...
				..3|6..|...
				.7.|.9.|2..
				---+---+---
				.5.|..7|...
				...|.45|7..
				...|1..|.3.
				---+---+---
				..1|...|.68
				..8|5..|.1.
				.9.|...|4..
				");

			var solver = new SudokuSolver(SudokuPuzzle.Puzzle3x3);

			var act = solver.Solve(puzzle);
			var exp = SudokuState.Parse(@"
				8..|...|...
				..3|6..|...
				.7.|.9.|2..
				---+---+---
				.5.|..7|...
				...|.45|7..
				...|1..|.3.
				---+---+---
				..1|...|.68
				..8|5..|.1.
				.9.|...|4..");

			Assert.AreEqual(exp, act);
		}
	}
}
