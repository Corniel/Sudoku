namespace SudokuSolver.Generics;

public sealed class CandidateLookup<T>
{
    private readonly T[] lookup = new T[(Candidates.Mask >> 1) + 1];

    public int Count => lookup.Length;

    public T this[Candidates candidates]
    {
        get => lookup[candidates.Bits >> 1];
        set => lookup[candidates.Bits >> 1] = value;
    }
}
