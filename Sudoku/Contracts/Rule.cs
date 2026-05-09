namespace Sudoku.Contracts;

/// <summary>Represents a Sudoku rule.</summary>
/// <remarks>
/// Rules can be:
/// * <see cref="Constraint"/>
/// * <see cref="Restriction"/>
/// * <see cref="Set"/>.
/// </remarks>
public interface Rule
{
    /// <summary>The cells linked to the rule.</summary>
    PosSet Cells { get; }
}
