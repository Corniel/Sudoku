namespace Sudoku.Validation;

public sealed class ConstraintViolation(Constraint rule) : Violation(rule)
{
    public override string ToString() => $"Constraint = {Rule}, Cells = {Rule.Cells}";
}
