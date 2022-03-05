namespace SudokuSolver.Techniques;

/// <summary>Reduces pointing pairs/triples and box reduction.</summary>
/// <remarks>
/// If a candidate is present in only N Cells of a Square/Row/Column, then it
/// must be the solution for one of these N cells. If these two cells belong
/// to the same Row or Column, then this candidate can not be the solution in
/// any other cell of the same Row or Column, respectively. 
/// </remarks>
public abstract class PointingMultiple : Technique
{
    protected abstract int Size{ get; }

    public Cells Reduce(Cells cells, Regions regions)
    {
        foreach (var region in regions)
        {
            foreach(var value in Values.Singles)
            {
                cells = CheckCells(cells, value, region, regions);
            }
        }
        return cells;
    }

    private Cells CheckCells(Cells cells, Values value, Region region, Regions regions)
    {
        var pointing = new List<Location>(Size);

        foreach(var cell in cells.Region(region))
        {
            if (cell.Values == value)
            {
                return cells;
            }
            else if (cell.Values & value)
            {
                if (pointing.Count < Size)
                {
                    pointing.Add(cell.Location);
                }
                else return cells;
            }
        }
        if (pointing.Count == Size)
        {
            foreach(var other in regions.Where(r => pointing.All(p => r.Contains(p))))
            {
                foreach(var loc in other.Where(l => !pointing.Contains(l)))
                {
                    cells = cells.Not(loc, value);
                }
            }
        }
        return cells;

    }
}
