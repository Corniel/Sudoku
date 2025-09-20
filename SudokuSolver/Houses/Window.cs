namespace SudokuSolver.Houses;

public sealed class Window(int index, PosSet cells) : House(index, cells)
{
    public static readonly ImmutableArray<Window> All = [.. Init()];

    private static IEnumerable<Window> Init()
    {
        var clues = Clues.Parse("""
        ...|...|...
        .11|1.2|22.
        .11|1.2|22.
        ---+---+---
        .11|1.2|22.
        ...|...|...
        .33|3.4|44.
        ---+---+---
        .33|3.4|44.
        .33|3.4|44.
        ...|...|...
        """);

        return Enumerable.Range(1, 4).Select(i => new Window(i, [.. clues.Where(c => c.Value == i).Select(c => c.Pos)]));
    }
}
