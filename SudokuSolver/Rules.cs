using SudokuSolver.Constraints;

namespace SudokuSolver;

/// <summary>A set of rules that apply when solving a set of <see cref="Clues"/>.</summary>
public static class Rules
{
    /// <summary>The standard set of houses.</summary>
    public static readonly ImmutableArray<Constraint> Standard = [.. Row.All, .. Col.All, .. Box.All];

    /// <summary>The standard set of houses extended with the four windows.</summary>
    public static readonly ImmutableArray<Constraint> Hyper = [.. Standard, ..Window.All];

    /// <summary>The standard set of houses extended with both diagonals.</summary>
    public static readonly ImmutableArray<Constraint> XSudoku = [.. Standard, Diagonal.NE_SW, Diagonal.NW_SE];

    /// <summary>The rows, columsn and jigsaw shaped houses.</summary>
    public static ImmutableArray<Constraint> Jigsaw(string jigsaws) => [.. Row.All, .. Col.All, .. Constraints.Jigsaw.Parse(jigsaws)];
}
