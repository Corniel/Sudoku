namespace SudokuSolver.Generics;

public readonly struct Triple<T>(T one, T two, T thr)
{
    public readonly T One = one;
    public readonly T Two = two;
    public readonly T Thr = thr;
}
