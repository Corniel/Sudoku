using System.Runtime.CompilerServices;

namespace System.Numerics;

public static class NumberExtensions
{
    /// <summary>Gets the square (²) of the number.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TNumber Sqr<TNumber>(this TNumber n) where TNumber : IMultiplyOperators<TNumber, TNumber, TNumber>
        => n * n;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this int n) => (n & 1) is 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this int n) => (n & 1) is 0;
}
