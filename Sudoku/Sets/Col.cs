namespace Sudoku.Sets;

public sealed class Col(int col, PosSet set) : House(col, set)
{
    internal static IEnumerable<Col> All() => range().Select(c => new Col(c, [.. range().Select(r => pos(r, c))]));
}
