namespace SudokuSolver.Techniques;

public abstract class HiddenMultiple : Technique
{
    protected abstract int Size { get; }
    protected abstract IReadOnlyCollection<Values> Hidden { get; }

    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var candidate in puzzle.Where(cell => cell.Values.Count >= Size))
        {
            foreach (var region in regions)
            {
                foreach (var hidden in Hidden)
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
            if (cell.Values & hidden)
            {
                if (hiddens.Count < Size && cell.Values.IsUndecided())
                {
                    hiddens.Add(cell.Location);
                }
                else return puzzle;
            }
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
