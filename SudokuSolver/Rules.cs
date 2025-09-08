using SudokuSolver.Constraints;

namespace SudokuSolver;

/// <summary>A set of rules that apply when solving a set of <see cref="Clues"/>.</summary>
public static class Rules
{
    /// <summary>The standard set of rules.</summary>
    public static readonly ImmutableArray<Constraint> Standard = [.. Row.All, .. Col.All, .. Box.All];
}
