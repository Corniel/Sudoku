using SmartAss.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>Reduces pointing pairs.</summary>
    /// <remarks>
    /// When a certain candidate appears only in two cells in a region, and
    /// those cells are both shared with the same other region, they are called
    /// a poiting pair. All other appearances of the candidates can be
    /// eliminated.
    /// </remarks>
    public class ReducePointingPairs : ISudokuSolver
    {
        private readonly SimpleList<int> buffer = new SimpleList<int>(3);

        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var result = ReduceResult.None;

            foreach (var region in GetRegions(puzzle))
            {
                foreach (var singleValue in puzzle.SingleValues)
                {
                    buffer.Clear();

                    foreach (var index in region)
                    {
                        if (state.IsKnown(index))
                        {
                            continue;
                        }

                        var val = state[index];

                        if ((val & singleValue) != SudokuPuzzle.Invalid)
                        {
                            buffer.Add(index);

                            if (buffer.Count == 3)
                            {
                                break;
                            }
                        }
                    }

                    // under normal circomstances, other counts are taken care off by other strategies.
                    if (buffer.Count == 2)
                    {
                        foreach (var other in region.Intersected)
                        {
                            // all are shared.
                            if (buffer.All(i => other.Contains(i)))
                            {
                                var mask = ~singleValue;
                                foreach (var index in other.Where(i => other.Contains(i)))
                                {
                                    result |= state.AndMask(index, mask);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static IEnumerable<SudokuRegion> GetRegions(SudokuPuzzle puzzle)
        {
            return puzzle.Regions.Where(region => region.RegionType == SudokuRegionType.Block);
        }
    }
}
