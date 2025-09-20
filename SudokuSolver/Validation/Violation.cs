using SudokuSolver.Restrictions;

namespace SudokuSolver.Validation;

public sealed class Violation(int value, Candidates allowed, Pos cell, Rule constraint, Restriction? restriction = null)
{
    public int Value { get; } = value;

    public Candidates Allowed { get; } = allowed;

    public Pos Cell { get; } = cell;

    public Rule Constraint { get; } = constraint;

    public Restriction Restriction { get; } = restriction ?? new Peers(cell, [.. constraint.Cells]);

    public override string ToString() => $"{Cell} = {Value}, Allowed = {Allowed}, Constraint = {Constraint}, Restrction = {Restriction}";
}
