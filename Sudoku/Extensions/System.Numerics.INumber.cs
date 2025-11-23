using System.Numerics;

namespace System;

public static class NumberExtensions
{
    /// <summary>Gets the square (²) of the number.</summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TNumber Sqr<TNumber>(this TNumber n) where TNumber : IMultiplyOperators<TNumber, TNumber, TNumber>
        => n * n;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this int n) => (n & 1) is 1;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this int n) => (n & 1) is 0;

    [Pure]
    public static TNumber Product<TNumber>(this IEnumerable<TNumber> numbers) where TNumber : IMultiplyOperators<TNumber, TNumber, TNumber>, INumberBase<TNumber>
    {
        var product = TNumber.One;

        foreach (var number in numbers)
            product *= number;

        return product;
    }
}
