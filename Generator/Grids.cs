using System.Runtime.CompilerServices;

namespace Generator;

public sealed class Grids(Random rnd) : IEnumerator<Cells>, IEnumerable<Cells>
{
    public static readonly Cells Lookup = Cells.Parse("""
            123│456│789
            456│789│123
            789│123│456
            ───┼───┼───
            231│674│895
            875│912│364
            694│538│217
            ───┼───┼───
            317│265│948
            542│897│631
            968│341│572
            """);

    private readonly Random Rnd = rnd;

    private readonly int[] Rows = [0, 1, 2, 3, 4, 5, 6, 7, 8];
    private readonly int[] Cols = [0, 1, 2, 3, 4, 5, 6, 7, 8];

    public Cells Current { get; private set; } = Cells.Empty;

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        var current = Current;
        Shuffle(Rows.AsSpan(0, 3));
        Shuffle(Rows.AsSpan(3, 3));
        Shuffle(Rows.AsSpan(6, 3));
        Shuffle(Cols.AsSpan(0, 3));
        Shuffle(Cols.AsSpan(3, 3));
        Shuffle(Cols.AsSpan(6, 3));

        for (var r = 0; r < 9; r++)
        {
            var row = Rows[r];
            for (var c = 0; c < 9; c++)
            {
                current[r, c] = Lookup[row, Cols[c]];
            }
        }
        Current = current;
        return true;
    }

    /// <remarks>
    /// This swaps 3 cells at once.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Shuffle(Span<int> i)
    {
        var s = Rnd.Next(6);
        /* [1, 0, 2] */ if (s is 000001) (i[0], i[1]) = (i[1], i[0]);
        /* [0, 2, 1] */ else if (s is 2) (i[1], i[2]) = (i[2], i[1]);
        /* [2, 1, 0] */ else if (s is 3) (i[0], i[2]) = (i[2], i[0]);
        /* [2, 0, 1] */ else if (s is 4) (i[0], i[1], i[2]) = (i[2], i[0], i[1]);
        /* [1, 2, 0] */ else if (s is 5) (i[0], i[1], i[2]) = (i[1], i[2], i[0]);
        // [0, 1, 2]    else if (s is 0) keep in same order.
    }

    void IDisposable.Dispose() { /* Nothging to dispose */ }

    void IEnumerator.Reset() => throw new NotSupportedException();

    public IEnumerator<Cells> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;
}
