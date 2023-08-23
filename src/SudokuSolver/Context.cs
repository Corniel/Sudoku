namespace SudokuSolver;

public ref struct Context
{
    public Context(Puzzle puzzle)
        : this(puzzle, Locations.All(puzzle.Where(c => c.Values.SingleValue()).Select(c => c.Location))) { }

    public Context(Puzzle puzzle, Locations singles)
    {
        Singles = singles;
        Cells = puzzle.cells;
    }

    public Locations Singles;
    public readonly uint[] Cells;

    public Puzzle Puzzle => new(Cells);
}
