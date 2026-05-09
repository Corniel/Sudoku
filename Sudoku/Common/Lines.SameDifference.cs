namespace Sudoku.Common;

public static partial class Lines
{
    public static IEnumerable<SameDifference> SameDifference(string grid)
        => Parse(grid).SelectMany(l => Group.Select(l, (a, _) => new SameDifference(a, l)));
}
