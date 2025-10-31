namespace Sudoku.Restrictions;

[DebuggerDisplay("{AppliesTo} >= {Other} + {Delta}")]
public sealed class More(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
{
    public int Delta { get; } = delta;

    public override Digits Restrict(Digits other) => Digits.AtLeast(other.First() + Delta);
}
