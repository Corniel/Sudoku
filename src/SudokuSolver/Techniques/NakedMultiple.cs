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

    protected abstract IReadOnlyCollection<Values> Naked { get; }

    /// <inheritdoc />
    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var region in regions)
        {
            foreach (var naked in Naked)
            {
                puzzle = CheckCells(puzzle, naked, region);
            }
        }
        return puzzle;
    }

    private  Puzzle CheckCells(Puzzle puzzle, Values naked, Region region)
    {
        var multiples = new List<Location>(Size);

        foreach (var cell in puzzle.Region(region))
        {
            if ((cell.Values & naked) == cell.Values)
            {
                if (multiples.Count < Size && cell.Values.IsUndecided())
                {
                    multiples.Add(cell.Location);
                }
                else return puzzle;
            }
        }
        if (multiples.Count == Size)
        {
            foreach (var location in region.Where(l => !multiples.Contains(l)))
            {
                puzzle = puzzle.Not(location, naked);
            }
        }
        return puzzle;
    }
}
