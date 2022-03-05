namespace SudokuSolver.Techniques;

/// <summary>Reduces hidden singles.</summary>
/// <remarks>
/// A cell with multiple candidates is called a hidden single if one of the
/// candidates is the only candidate is a row, collumn or block. The single
/// candidate is the solution to the cell.
/// 
/// All other appearences of the same candidate, if any, are eliminated if
/// they can bee seen by the Single.
/// </remarks>
public class HiddenSingles : Technique
{
    /// <inheritdoc />
    public Cells Reduce(Cells cells, Regions regions)
    {
        foreach (var region in regions)
        {
            foreach (var single in Values.Singles)
            {
                cells = CheckCells(cells, single, region);
            }
        }
        return cells;
    }

    private static Cells CheckCells(Cells cells, Values single, Region region)
    {
        var hidden = Location.None;

        foreach(var cell in cells.Region(region))
        {
            // Single has already been assigned.
            if (cell.Values == single) return cells;
            
            // cell is candidate
            else if (cell.Values & single)
            {
                // Not the only candidate.
                if (hidden == Location.None)
                {
                    hidden = cell.Location;
                }
                else return cells;
            }
        }
        return hidden == Location.None
            ? cells
            : cells.And(hidden, single);
    }
}
