namespace SudokuSolver.Techniques;

/// <summary>Reduces naked multiples.</summary>
public abstract class NakedMultiple : Technique
{
    protected abstract int Size { get; }

    /// <inheritdoc />
    public Cells? Reduce(Cells cells, Regions regions)
    {
        var reduced = cells;
        foreach (var region in regions)
        {
            reduced = CheckCells(reduced, region);
        }
        return reduced == cells ? null : reduced;
    }

    private  Cells CheckCells(Cells cells, Region region)
    {
        var multiples = new List<Values>(Size);

        foreach (var values in cells.Region(region).Select(c => c.Values))
        {
            if (values.Count == Size && (multiples.Count == 0 || multiples[0] == values))
            {
                // pair occurs more than twice.
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

            foreach (var index in region)
            {
                var cell = cells[index];
                if (cell != multiple)
                {
                    cells = cells.Not(index, multiple);
                }
            }
        }
        return cells;
    }
}
