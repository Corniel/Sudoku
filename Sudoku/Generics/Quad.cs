namespace Sudoku.Generics;

public readonly struct Quad<T>(T one, T two, T thr, T four)
{
    public readonly T One = one;
    public readonly T Two = two;
    public readonly T Thr = thr;
    public readonly T For = four;

    /// <summary>Deconstructs the quad.</summary>
    public void Deconstruct(out T one, out T two, out T thr, out T fur) => (one, two, thr, fur) = (One, Two, Thr, For);
}
