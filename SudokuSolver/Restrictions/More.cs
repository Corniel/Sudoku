namespace SudokuSolver.Restrictions;

[DebuggerDisplay("{AppliesTo} >= {Other} + {Delta}")]
public sealed class More(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
{
    public override double Bits => Info.Avg(9 - Delta);

    public int Delta { get; } = delta;

    public override Candidates Restrict(int value) => Candidates.AtLeast((value is 0 ? 1 : value) + Delta);
}
