using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Corniel.Sudoku.UnitTests
{
    [TestFixture]
    public class SudokuSolverTest
    {
        [Test]
        public void Solve_2x2Invalid_ThrowsInvalidPuzzleException()
        {
            Assert.Catch<InvalidPuzzleException>(() =>
            {
                var puzzle = SudokuState.Parse(@"
                .2|..
                2.|..
                --+--
                ..|..
                ..|..");
                var solver = new SudokuSolver(SudokuPuzzle.Puzzle2x2);
                solver.Solve(puzzle);
            });

        }
        [Test]
        public void Solve_2x2_SolvedPuzzle()
        {
            Solve(SudokuPuzzle.Puzzle2x2, @"
                .4|.1
                3.|..
                --+--
                ..|.4
                ..|..",
                @"
                24|31
                31|42
                --+--
                13|24
                42|13");
        }

        [Test]
        public void Solve_3x3WikipediaExample_SolvedPuzzle()
        {
            Solve(SudokuPuzzle.Puzzle3x3, @"
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
                ...|.8.|.79",
                @"
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
        }

        [Test]
        public void Solve_3x3Example1_SolvedPuzzle()
        {
            Solve(SudokuPuzzle.Puzzle3x3, @"
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
                3..|69.|..2",
                @"
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
        }

        [Test]
        public void Solve_3x3Example2_SolvedPuzzle()
        {
            Solve(SudokuPuzzle.Puzzle3x3, @"
                ...|317|.69
                16.|.92|..3
                ..9|...|...
                ---+---+---
                41.|7..|..8
                5.3|9.4|6.7
                7..|..8|.32
                ---+---+---
                ...|...|3..
                9..|83.|.46
                63.|549|...",
                @"
                254|317|869
                168|492|573
                379|685|124
                ---+---+---
                412|763|958
                583|924|617
                796|158|432
                ---+---+---
                841|276|395
                925|831|746
                637|549|281");
        }

        [Test]
        public void Solve_3x3WorldsHardestStudokuAccordingToTheTelegraph_SolvedPuzzle()
        {
            Solve(SudokuPuzzle.Puzzle3x3, @"
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
                ",
                @"
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
        }

        private void Solve(SudokuPuzzle puzzle, string input, string expected)
        {
            var state = SudokuState.Parse(input);
            var solver = new SudokuSolver(puzzle);

            var sw = Stopwatch.StartNew();
            var actual = solver.Solve(state);
            sw.Stop();

            Console.WriteLine("Elapsed: {0:#,##0.#####} ms", sw.Elapsed.TotalMilliseconds);

            Assert.IsTrue(actual.IsSolved, "The puzzle is not solved.");
            Assert.AreEqual(SudokuState.Parse(expected), actual);
        }
    }
}
