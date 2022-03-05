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
    public Puzzle Reduce(Puzzle cells, Regions regions)
    {
        foreach (var region in regions)
        {
            cells = CheckCells(cells, region);
        }
        return cells;
    }

    private  Puzzle CheckCells(Puzzle cells, Region region)
    {
        var multiples = new List<Values>(Size);

        foreach (var values in cells.Region(region).Select(c => c.Values))
        {
            if (values.Count == Size && (multiples.Count == 0 || multiples[0] == values))
            {
                if (multiples.Count < Size)
                {
                    multiples.Add(values);
                }
                else return cells;
            }
        }
        if (multiples.Count == Size)
        {
            var multiple = multiples[0];

            foreach (var cell in cells.Region(region).Where(c => c.Values!= multiple))
            {
                cells = cells.Not(cell.Location, multiple);
            }
        }
        return cells;
    }
}
