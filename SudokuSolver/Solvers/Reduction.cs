namespace SudokuSolver.Solvers;

internal sealed class Reduction(Rules rules)
{
    private readonly Constraint[] Constraints = rules.ToArray();

    public Rules Rules { get; } = rules;

    public Constraint this[Pos cell]
    {
        get => Constraints[cell];
        set => Constraints[cell] = value;
    }
}
