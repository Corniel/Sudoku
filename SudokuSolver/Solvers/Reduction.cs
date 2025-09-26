namespace SudokuSolver.Solvers;

public sealed class Reduction(Rules rules)
{
    private readonly Constraint[] Constraints = rules.ToArray();

    public Rules Rules { get; } = rules;

    /// <summary>Gets all houses (e.a. sets with size 9).</summary>
    public IEnumerable<Rule> Houses => Rules.Where(r => r.IsSet && r.Count == _9);

    public Constraint this[Pos cell]
    {
        get => Constraints[cell];
        set => Constraints[cell] = value;
    }
}
