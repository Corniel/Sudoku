namespace Sudoku.Common;

public static class MagicSquare
{
    /// <inheritdoc cref="New(PosArray)" />
    public static Rules New(PosSet sq) => New([.. sq]);

    /// <summary>Creates a new Magic Square.</summary>
    /// <remarks>
    /// In Sudoku, a 3x3 Magic Square can be constructed. The constraints:
    /// * The center is a 5
    /// * The corners are even
    /// * The remaining *cross* cells are odd
    /// * The lines sum up to 15 (45/3).
    /// </remarks>
    [OverloadResolutionPriority(1)]
    public static Rules New(PosArray sq)
    {
        if (sq.Length != _9) throw new ArgumentOutOfRangeException(nameof(sq), "A magic square must have a size of 9.");

        var (a, b, c, d, e, f, g, h, i) = (sq[0], sq[1], sq[2], sq[3], sq[4], sq[5], sq[6], sq[7], sq[8]);
        return
        [
            new CellSet([.. sq], "Magic square"),

            Corner(a), Cross_(b), Corner(c),
            Cross_(d), Center(e), Cross_(f),
            Corner(g), Cross_(h), Corner(i),

            // Rows
            .. Sum(a, b, c), .. Sum(d, f), .. Sum(g, h, i),

            // Cols
            .. Sum(a, d, g), .. Sum(b, h), .. Sum(c, f, i),

            // Digs
            .. Sum(a, i), .. Sum(c, g),
        ];

        static Mask Corner(Pos p) => new(p, Digits.Even);
        static Mask Cross_(Pos p) => new(p, [1, 3, 7, 9]);
        static Mask Center(Pos p) => new(p, [5]);
        static Rules Sum(params PosSet cells) => Groups.KillerCage(cells, [cells.Count is 3 ? 15 : 10]);
    }
}
