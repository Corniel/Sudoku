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

    public Cells Reduce(Cells cells, Regions regions)
    {
        foreach (var pair in Pairs)
        {
            foreach (var region in regions)
            {
                cells = CheckCells(cells, pair, region);
            }
        }
        return cells;
    }

    private static Cells CheckCells(Cells cells, Values pair, Region region)
    {
        var hidden = new List<Location>(2);

        foreach (var cell in cells.Region(region))
        {
            var and = cell.Values & pair;

            if (and == pair)
            {
                if (hidden.Count < 2)
                {
                    hidden.Add(cell.Location);
                }
                else return cells;
            }
            else if (and) return cells;
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
