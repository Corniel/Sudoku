namespace SudokuSolver.Restrictions;

public sealed class Twin(Pos appliesTo, Pos other) : Pair(appliesTo, other)
{
    public override string ToString() => $"{AppliesTo} = {Other}";

    public override double Bits => Info.Avg(0.1);

    public override Candidates Restrict(int value)
        => value is 0
        ? Candidates._1_to_9
        : Candidates.New(value);
}
