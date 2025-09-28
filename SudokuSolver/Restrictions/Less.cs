namespace SudokuSolver.Restrictions;

[DebuggerDisplay("{AppliesTo} <= {Other} - {Delta}")]
public sealed class Less(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
{
    public int Delta { get; } = delta;

    public override double Bits => Info.Avg(9 - Delta);

    public override Candidates Restrict(int value) => Candidates.AtMost((value is 0 ? _9 : value) - Delta);
}
