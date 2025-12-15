namespace Sudoku.Validation;

public sealed class Violation(Digits value, Digits allowed, Pos cell, Rule? constraint, Restriction? restriction = null)
{
    public Digits Digits { get; } = value;

    public Digits Allowed { get; } = allowed;

    public Pos Cell { get; } = cell;

    public Rule? Constraint { get; } = constraint;

    public Restriction? Restriction { get; } = restriction;

    public override string ToString() => $"{Cell} = {Digits}, Allowed = {Allowed}, Constraint = {Constraint}, Restriction = {Restriction}";
}
