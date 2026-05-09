namespace Sudoku.Validation;

public abstract class Violation(Rule rule)
{
    public Rule Rule { get; } = rule;
}
