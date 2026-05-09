namespace Sudoku.Validation;

public sealed class RestrictionViolation(Digits value, Digits allowed, Pos cell, Restriction rule) : Violation(rule)
{
    public Digits Digits { get; } = value;

    public Digits Allowed { get; } = allowed;

    public Pos Cell { get; } = cell;

    public override string ToString() => $"{Cell} = {Digits}, Allowed = {Allowed}, Restriction = {Rule}";
}
