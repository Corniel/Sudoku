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

    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var pair in Pairs)
        {
            foreach (var region in regions)
            {
                puzzle = CheckCells(puzzle, pair, region);
            }
        }
        return puzzle;
    }

    private static Puzzle CheckCells(Puzzle puzzle, Values pair, Region region)
    {
        var hidden = new List<Location>(2);

        foreach (var cell in puzzle.Region(region))
        {
            var and = cell.Values & pair;

            if (and == pair)
            {
                if (hidden.Count < 2)
                {
                    hidden.Add(cell.Location);
                }
                else return puzzle;
            }
            else if (and) return puzzle;
        }
        if (hidden.Count == 2)
        {
            foreach (var location in hidden)
            {
                puzzle = puzzle.And(location, pair);
            }
        }
        return puzzle;
    }
}
