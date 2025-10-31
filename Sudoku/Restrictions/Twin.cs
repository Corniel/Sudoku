namespace Sudoku.Restrictions;

public sealed class Twin(Pos appliesTo, Pos other) : Pair(appliesTo, other)
{
    public override string ToString() => $"{AppliesTo} = {Other}";

    public override Digits Restrict(Digits other) => other;
}
