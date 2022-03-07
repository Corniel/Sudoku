namespace SudokuSolver.Techniques;

/// <summary>Reduces naked multiples.</summary>
/// <remarks>
/// If N Cells in a region (Row, Column or Square) contain exactly the same
/// N candidates, then one of these candidates is the solution for one of
/// these Cells and the other candidates is the solution for the other Cells.
/// 
/// Hence none of these two candidates can be the solution in any other Cell of
/// that region; these two candidates can be deleted from the other Cells of
/// that region.
/// </remarks>
public abstract class NakedMultiple : Technique
{
    protected abstract int Size { get; }

    /// <inheritdoc />
    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var region in regions)
        {
            puzzle = CheckCells(puzzle, region);
        }
        return puzzle;
    }

    private  Puzzle CheckCells(Puzzle puzzle, Region region)
    {
        var multiples = new List<Values>(Size);

        foreach (var values in puzzle.Region(region).Values())
        {
            if (values.Count == Size && (multiples.Count == 0 || multiples[0] == values))
            {
                if (multiples.Count < Size)
                {
                    multiples.Add(values);
                }
                else return puzzle;
            }
        }
        if (multiples.Count == Size)
        {
            var multiple = multiples[0];

            foreach (var cell in puzzle.Region(region).Where(c => c.Values!= multiple))
            {
                puzzle = puzzle.Not(cell.Location, multiple);
            }
        }
        return puzzle;
    }
}
