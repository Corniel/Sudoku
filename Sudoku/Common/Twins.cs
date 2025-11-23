using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class Twins
{
    public static ImmutableArray<Twin> New(Pos first, Pos second)
        => [new(first, second), new(second, first)];
}
