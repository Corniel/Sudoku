namespace SudokuSolver.Techniques;

/// <summary>Reduces pointing pairs/triples and box reduction.</summary>
/// <remarks>
/// If a candidate is present in only N Cells of a Square/Row/Column, then it
/// must be the solution for one of these N puzzle. If these two puzzle belong
/// to the same Row or Column, then this candidate can not be the solution in
/// any other cell of the same Row or Column, respectively. 
/// </remarks>
public abstract class PointingMultiple : Technique
{
    protected abstract int Size{ get; }

    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var region in regions)
        {
            foreach(var value in Values.Singles)
            {
                puzzle = CheckCells(puzzle, value, region, regions);
            }
        }
        return puzzle;
    }

    private Puzzle CheckCells(Puzzle puzzle, Values value, Region region, Regions regions)
    {
        var pointing = new List<Location>(Size);

        foreach(var cell in puzzle.Region(region))
        {
            if (cell.Values == value)
            {
                return puzzle;
            }
            else if (cell.Values & value)
            {
                if (pointing.Count < Size)
                {
                    pointing.Add(cell.Location);
                }
                else return puzzle;
            }
        }
        if (pointing.Count == Size)
        {
            foreach(var other in regions.Where(r => pointing.All(p => r.Contains(p))))
            {
                foreach(var loc in other.Where(l => !pointing.Contains(l)))
                {
                    puzzle = puzzle.Not(loc, value);
                }
            }
        }
        return puzzle;

    }
}
