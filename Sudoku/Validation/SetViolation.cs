namespace Sudoku.Validation;

public sealed class SetViolation(PosSet violations, Set rule) : Violation(rule)
{
    public PosSet Violations { get; } = violations;

    public override string ToString() => $"Set = {Rule}, Violations = {Violations}";
}
