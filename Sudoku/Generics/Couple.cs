namespace Sudoku.Generics;

public readonly struct Couple<T>(T one, T two)
{
    public readonly T One = one;
    public readonly T Two = two;
}
