namespace SudokuSolver.Solvers;

public sealed record ReduceOptions
{
    public bool NakedSingles { get; init; }

    public bool HiddenSingles { get; init; }

    public bool Restrictions { get; init; }

    public static readonly ReduceOptions None = new();
    public static readonly ReduceOptions Default = new() { NakedSingles = true };
    public static readonly ReduceOptions All = Default with { Restrictions = true, HiddenSingles = true };
}
