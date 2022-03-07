namespace SudokuSolver.Techniques;

public abstract class HiddenMultiple : Technique
{
    protected abstract int Size { get; }
    protected abstract IReadOnlyCollection<Values> Hidden { get; }

    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var region in regions)
        {
            foreach (var values in Hidden)
            {
                puzzle = CheckCells(puzzle, values, region);
            }
        }
        return puzzle;
    }

    private Puzzle CheckCells(Puzzle puzzle, Values pair, Region region)
    {
        var hidden = new List<Location>(Size);

        foreach (var cell in puzzle.Region(region))
        {
            var and = cell.Values & pair;

            if (and == pair)
            {
                if (hidden.Count < Size)
                {
                    hidden.Add(cell.Location);
                }
                else return puzzle;
            }
            else if (and) return puzzle;
        }
        if (hidden.Count == Size)
        {
            foreach (var location in hidden)
            {
                puzzle = puzzle.And(location, pair);
            }
        }
        return puzzle;
    }
}
