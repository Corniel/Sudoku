namespace SudokuSolver.Constraints;

public static class Diagonal
{
    /// <summary>NW-SE diagonal.</summary>
    public static readonly NWSE NW_SE = new();

    /// <summary>NE-SW diagonal.</summary>
    public static readonly NESW NE_SW = new();

    public sealed class NWSE() : House(0, PosSet.New(range().Select(i => new Pos(i, i))));

    public sealed class NESW() : House(1, PosSet.New(range().Select(i => new Pos(i, _9 - i - 1))));
}
