namespace SudokuSolver.Techniques;

/// <summary>Reduces (naked) singles.</summary>
/// <remarks>
/// Any cells which have only one candidate can safely be assigned that value.
/// 
/// It is very important whenever a value is assigned to a cell, that this
/// value is also excluded as a candidate from all other blank cells sharing
/// the same row, column and sub square.
/// </remarks>
public class NakedSingles : Technique
{
    /// <inheritdoc />
    public Cells? Reduce(Cells cells, Regions regions)
    {
        var reduced = cells;

        foreach (var index in regions.Locations)
        {
            var cell = reduced[index];
            if (cell.SingleValue())
            {
                foreach (var region in regions[index])
                {
                    foreach (var other in region.Where(i => i != index))
                    {
                        reduced = reduced.Not(other, cell);
                    }
                }
            }
        }
        return reduced == cells ? null : reduced;
    }
}
