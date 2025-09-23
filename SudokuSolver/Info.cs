using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace SudokuSolver;

public static class Info
{
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Cell(int candidates) => cell[candidates];

    public static double Avg(double candidates) => Bits((candidates / _9) + ((9d - candidates) / _9x9));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Bits(double p) => -Math.Log2(p);

    public static double Peer(int candidates) => peer[candidates];

    private static readonly ImmutableArray<double> cell =
    [
       /* ? */ 0,
       /* 1 */ Bits(1d / _9),
       /* 2 */ Bits(2d / _9),
       /* 3 */ Bits(3d / _9),
       /* 4 */ Bits(4d / _9),
       /* 5 */ Bits(5d / _9),
       /* 6 */ Bits(6d / _9),
       /* 7 */ Bits(7d / _9),
       /* 8 */ Bits(8d / _9),
       /* 9 */ 0,
    ];

    private static readonly double[] peer =
    [
       /* ? */ 0,
       /* 1 */ Bits((0d / _9) + (9d / _9 * 8d / _9)),
       /* 2 */ Bits((1d / _9) + (8d / _9 * 8d / _9)),
       /* 3 */ Bits((2d / _9) + (7d / _9 * 8d / _9)),
       /* 4 */ Bits((3d / _9) + (6d / _9 * 8d / _9)),
       /* 5 */ Bits((4d / _9) + (5d / _9 * 8d / _9)),
       /* 6 */ Bits((5d / _9) + (4d / _9 * 8d / _9)),
       /* 7 */ Bits((6d / _9) + (3d / _9 * 8d / _9)),
       /* 8 */ Bits((7d / _9) + (2d / _9 * 8d / _9)),
       /* 9 */ Bits((8d / _9) + (1d / _9 * 8d / _9)),
    ];
}
