using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corniel.Sudoku.UnitTests.Solvers
{
    public class ReducePointingPairsTest
    {
        [Test]
        public void Reduce_PointingPair()
        {
            var state = SudokuState.Parse(@"
				47.|3..|218
				.82|4.1|7.3
				13.|.8.|.45
				---+---+---
				.1.|...|3..
				6.3|.15|4..
				74.|.3.|..1
				---+---|---
				8.1|...|539
				..7|5..|1.4
				.54|1..|.7.");

            new ReduceNakedSingles().Solve(SudokuPuzzle.Struture, state);

            var solver = new ReducePointingPairs();
            var result = solver.Solve(SudokuPuzzle.Struture, state);

            Assert.AreEqual(ReduceResult.Reduced, result);
        }
    }
}
