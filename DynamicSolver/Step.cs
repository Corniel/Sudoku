namespace DynamicSolver;

public readonly struct Step(Pos cell, Digits prev)
{
    public readonly Pos Cell = cell;
    public readonly Digits Prev = prev;

    public override string ToString() => $"{Cell} = {Prev}";
}
