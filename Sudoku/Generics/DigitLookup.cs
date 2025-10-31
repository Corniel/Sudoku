namespace Sudoku.Generics;

public sealed class DigitLookup<T>
{
    private readonly T[] lookup = new T[(Digits.Mask >> 1) + 1];

    public int Count => lookup.Length;

    public T this[Digits digits]
    {
        get => lookup[digits.Bits >> 1];
        set => lookup[digits.Bits >> 1] = value;
    }
}
