namespace SudokuSolver.Restrictions;

[DebuggerDisplay("{AppliesTo} >= {Other} + {Delta}")]
public sealed class More(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
{
    public int Delta { get; } = delta;

    public override Candidates Restrict(Candidates other) => Candidates.AtLeast(other.First() + Delta);
}
