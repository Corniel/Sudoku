namespace SudokuSolver.Techniques;

public class HiddenPairs : Technique
{
    private readonly Values[] Pairs;

    public HiddenPairs()
    {
        Pairs = Values.Singles
            .SelectMany(single => Values.Singles.Select(other => single | other))
            .Where(p => p.Count == 2)
            .ToArray();
    }

    public Cells? Reduce(Cells cells, Regions regions)
    {
        var reduced = cells;
        foreach (var pair in Pairs)
        {
            foreach (var region in regions)
            {
                reduced = CheckCells(reduced, pair, region);
            }
        }
        return reduced == cells ? null : reduced;
    }

    private static Cells CheckCells(Cells cells, Values pair, Region region)
    {
        var hidden = new List<Location>(2);

        foreach (var cell in cells.Region(region))
        {
            var and = cell.Values & pair;

            // If not both are present or we already had 2, return.
            if (cell.Values == pair)
            {
                if (hidden.Count < 2)
                {
                    hidden.Add(cell.Location);
                }
                else return cells;
            }
            else if (and != default)
            {
                return cells;
            }
        }
        if (hidden.Count == 2)
        {
            foreach (var location in hidden)
            {
                cells = cells.And(location, pair);
            }
        }
        return cells;
    }
}
