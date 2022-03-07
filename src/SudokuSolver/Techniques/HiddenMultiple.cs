namespace SudokuSolver.Techniques;

public abstract class HiddenMultiple : Technique
{
    protected abstract int Size { get; }
    protected abstract IReadOnlyCollection<Values> Hidden { get; }

    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var candidate in puzzle.Where(cell => cell.Values.Count >= Size))
        {
            foreach (var hidden in Hidden.Where(h => (h & candidate.Values) == h))
            {
                foreach (var region in regions)
                {
                    puzzle = CheckCells(puzzle, hidden, region);
                }
            }
        }
        return puzzle;
    }

    private Puzzle CheckCells(Puzzle puzzle, Values hidden, Region region)
    {
        var hiddens = new List<Location>(Size);

        foreach (var cell in puzzle.Region(region))
        {
            var and = cell.Values & hidden;

            if (and == hidden)
            {
                if (hiddens.Count < Size)
                {
                    hiddens.Add(cell.Location);
                }
                else return puzzle;
            }
            else if (and) return puzzle;
        }
        if (hiddens.Count == Size)
        {
            foreach (var location in hiddens)
            {
                puzzle = puzzle.And(location, hidden);
            }
        }
        return puzzle;
    }
}
