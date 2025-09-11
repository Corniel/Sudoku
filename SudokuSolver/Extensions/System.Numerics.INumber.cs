namespace System.Numerics;

public static class NumberExtensions
{
    /// <summary>Gets the square (²) of the number.</summary>
    public static TNumber Sqr<TNumber>(this TNumber n) where TNumber : IMultiplyOperators<TNumber, TNumber, TNumber>
        => n * n;
}
