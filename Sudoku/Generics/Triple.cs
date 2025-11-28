namespace Sudoku.Generics;

public readonly struct Triple<T>(T one, T two, T thr)
{
    public readonly T One = one;
    public readonly T Two = two;
    public readonly T Thr = thr;

    /// <summary>Deconstructs the triple.</summary>
    public void Deconstruct(out T one, out T two, out T thr) => (one, two, thr) = (One, Two, Thr);
}
