namespace SudokuSolver.Techniques;

/// <summary>Reduces (naked) singles.</summary>
/// <remarks>
/// Any puzzle which have only one candidate can safely be assigned that value.
/// 
/// It is very important whenever a value is assigned to a cell, that this
/// value is also excluded as a candidate from all other blank puzzle sharing
/// the same row, column and sub square.
/// </remarks>
public class NakedSingles : Technique
{
    /// <inheritdoc />
    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var index in regions.Locations)
        {
            var cell = puzzle[index];
            if (cell.SingleValue())
            {
                foreach (var region in regions[index])
                {
                    foreach (var other in region.Where(i => i != index))
                    {
                        puzzle = puzzle.Not(other, cell);
                    }
                }
            }
        }
        return puzzle;
    }
}
