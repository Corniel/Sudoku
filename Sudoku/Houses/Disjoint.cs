namespace Sudoku.Houses;

public sealed class Disjoint(int index, PosSet cells) : House(index, cells)
{
    public static readonly ImmutableArray<Disjoint> All = [.. Init()];

    private static IEnumerable<Disjoint> Init()
    {
        var clues = Clues.Parse("""
        123│123│123
        456│456│456
        789│789│789
        ───┼───┼───
        123│123│123
        456│456│456
        789│789│789
        ───┼───┼───
        123│123│123
        456│456│456
        789│789│789
        """);

        return Enumerable.Range(1, 9).Select(i => new Disjoint(i, [.. clues.Where(c => c.Digit == i).Select(c => c.Pos)]));
    }
}
