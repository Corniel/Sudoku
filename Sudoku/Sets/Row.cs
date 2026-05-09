namespace Sudoku.Sets;

public sealed class Row(int row, PosSet set) : House(row, set)
{
    internal static IEnumerable<Row> All() => range().Select(r => new Row(r, [.. range().Select(c => pos(r, c))]));
}
