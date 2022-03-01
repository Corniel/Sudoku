namespace SudokuSolver.Techniques;

public class PointingPair : Technique
{
    /// <summary>Reduces pointing pairs.</summary>
    /// <remarks>
    /// When a certain candidate appears only in two cells in a region, and
    /// those cells are both shared with the same other region, they are called
    /// a poiting pair. All other appearances of the candidates can be
    /// eliminated.
    /// </remarks>
    public Cells? Reduce(Cells cells, Regions regions)
    {
        var reduced = cells;
        foreach (var region in regions)
        {
            var locations = new List<Location>(2);

            foreach(var single in Values.Singles)
            {
                reduced = CheckCells(regions, reduced, region, locations, single);
            }
        }
        return reduced == cells ? null : reduced;
    }

    private static Cells CheckCells(Regions regions, Cells cells, Region region, List<Location> locations, Values single)
    {
        locations.Clear();

        foreach (var cell in cells.Region(region))
        {
            if ((cell.Values & single) != default)
            {
                if (locations.Count < 2)
                {
                    locations.Add(cell.Location);
                }
                else return cells;
            }
        }

        if (locations.Count == 2)
        {
            foreach (var other in regions[locations[0]].Where(r => r.Contains(locations[1])))
            {
                foreach (var cell in cells.Region(other).Where(c => !locations.Contains(c.Location)))
                {
                    cells = cells.Not(cell.Location, single);
                }
            }
        }
        return cells;
    }
}
