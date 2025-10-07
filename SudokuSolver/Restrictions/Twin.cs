namespace SudokuSolver.Restrictions;

public sealed class Twin(Pos appliesTo, Pos other) : Pair(appliesTo, other)
{
    public override string ToString() => $"{AppliesTo} = {Other}";

    public override Candidates Restrict(Candidates other) => other;
}
