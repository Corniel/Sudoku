using SudokuSolver.Common;

namespace SudokuSolver.Houses;

public abstract class House(int index, PosSet cells) : Set(cells)
{
    /// <summary>The index of the house.</summary>
    public int Index { get; } = index;

    internal override string DebuggerDisplay => $"[{Index}]";

    internal static IEnumerable<int> range() => Enumerable.Range(0, _9);
}
