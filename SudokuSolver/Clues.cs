 namespace SudokuSolver;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct Clues(ImmutableArray<Cell> cells) : IReadOnlyCollection<Cell>
{
    /// <summary>No clues given.</summary>
    public static readonly Clues None = new([]);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ImmutableArray<Cell> Cells = cells;

    /// <inheritdoc />
    public int Count => Cells.Length;

    /// <inheritdoc />
    public IEnumerator<Cell> GetEnumerator() => Cells.AsEnumerable().GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Clues Parse(string str)
    {
        var cells = new Cell[_9x9];
        var p = Pos.First;
        var i = 0;

        foreach (var ch in str)
        {
            if (ch is '.' or '?' or '0')
            {
                p++;
            }
            else if (ch is >= '1' and <= '9')
            {
                cells[i++] = new(p++, ch - '0');
            }
        }

        return new([..cells[..i]]);
    }
}
