namespace SudokuSolver;

/// <summary>A set of rules that apply when solving a set of <see cref="Clues"/>.</summary>
public sealed record Rules
{
    public static readonly Rules Standard = new();

    /// <summary>
    /// All sets of cells (such as <see cref="Houses"/>) that must have different values.
    /// </summary>
    public ImmutableArray<PosSet> Sets { get; init; } = Houses.Standard;

    /// <summary>Additional (cell based) restrctions.</summary>
    /// <remarks>
    /// In a standard Sudoku puzzle, there are no additional restrictions.
    /// </remarks>
    public ImmutableArray<Restriction> Restrictions { get; init; } = [];
}
