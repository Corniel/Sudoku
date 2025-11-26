using System.IO;

namespace Sudoku;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct Clues : IReadOnlyCollection<Cell>
{
    public Clues(ImmutableArray<Cell> cells) => Cells = cells;

    public Clues(IEnumerable<Cell> cells) => Cells = [.. cells];

    /// <summary>No clues given.</summary>
    public static readonly Clues None = new([]);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ImmutableArray<Cell> Cells;

    /// <inheritdoc />
    public int Count => Cells.Length;

    public void WriteTo(StreamWriter writer)
    {
        for (var p = Pos.O; p < _9x9; p++)
        {
            var digit = Cells.FirstOrDefault(c => c.Pos == p).Digit;
            writer.Write(digit is 0 ? '.' : (char)(digit + '0'));
        }
    }

    /// <inheritdoc />
    public IEnumerator<Cell> GetEnumerator() => Cells.AsEnumerable().GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Clues Parse(string str)
    {
        var cells = new Cell[_9x9];
        var p = Pos.O;
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

        return new(cells[..i]);
    }
}
