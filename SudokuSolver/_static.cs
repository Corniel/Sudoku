namespace SudokuSolver;

public static class Statics
{
    /// <summary>Is 9.</summary>
    public const int _9 = 9;

    /// <summary>Is 45.</summary>
    public const int _45 = 45;

    /// <summary>Is 9 * 9 (81).</summary>
    public const int _9x9 = _9 * _9;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> range(int start, int size) => Enumerable.Range(start, size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> range(int size) => Enumerable.Range(0, size);

    public static int triangle(int value) => triangles[value];

    private static readonly ImmutableArray<int> triangles =
    [
        0,
        1,
        1 + 2,
        1 + 2 + 3,
        1 + 2 + 3 + 4,
        1 + 2 + 3 + 4 + 5,
        1 + 2 + 3 + 4 + 5 + 6,
        1 + 2 + 3 + 4 + 5 + 6 + 7,
        1 + 2 + 3 + 4 + 5 + 6 + 7 + 8,
        1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9,
    ];
}
