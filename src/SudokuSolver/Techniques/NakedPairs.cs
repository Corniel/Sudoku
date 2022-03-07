namespace SudokuSolver.Techniques;

/// <summary>Reduces naked pairs.</summary>
/// <remarks>
/// Two puzzle in a row, a column or a block having only the same pair of
/// candidates are called a Naked Pair.
/// 
/// All other appearances of the two candidates in the same row, column,
/// or block can be eliminated.
/// </remarks>
public class NakedPairs : Technique
{
    private const int Size = 2;

    /// <inheritdoc />
    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var cell in puzzle.Where(c => c.Values.Count == Size))
        {
            foreach (var region in regions[cell.Location])
            {
                puzzle = CheckCells(cell.Values, region, puzzle);
            }
        }
        return puzzle;
    }

    private static Puzzle CheckCells(Values naked, Region region, Puzzle puzzle)
    {
        var count = 0;
        foreach(var cell in puzzle.Region(region))
        {
            if (cell.Values == naked && ++count > Size) return puzzle;
        }
        if (count == Size)
        {
            foreach (var cell in puzzle.Region(region).Where(c => c.Values != naked))
            {
                puzzle = puzzle.Not(cell.Location, naked);
            }
        }
        return puzzle;
    }
}
