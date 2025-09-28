namespace SudokuSolver.Solvers;

public sealed record ReduceOptions
{
    public bool NakedSingles { get; init; }

    public bool Hidden { get; init; }

    public bool NakedPairs { get; init; }

    public bool Intersection { get; init; }

    public bool Restrictions { get; init; }

    public bool Backtracker { get; init; }

    public static readonly ReduceOptions Backtracking = new() { Backtracker = true };

    public static readonly ReduceOptions Default = new() { NakedSingles = true, Backtracker = true, };

    public static readonly ReduceOptions All = new()
    {
        NakedSingles = true,
        NakedPairs = true,
        Hidden = true,
        Intersection = true,
        Restrictions = true,
        Backtracker = true,
    };
}
