using System.Runtime.CompilerServices;

namespace SudokuSolver;

public static class Statics
{
    /// <summary>Is 9.</summary>
    public const int _9 = 9;

    /// <summary>Is 9 * 9 (81).</summary>
    public const int _9x9 = _9 * _9;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> range(int start, int size) => Enumerable.Range(start, size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> range(int size) => Enumerable.Range(0, size);
}
