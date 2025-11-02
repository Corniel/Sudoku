namespace DynamicSolver;

#if DEBUG
public readonly struct Step(Pos cell, Digits prev, Digits curr)
#else
public readonly struct Step(Pos cell, Digits prev)
#endif
{
    public readonly Pos Cell = cell;
    public readonly Digits Prev = prev;
#if DEBUG
    public readonly Digits Curr = curr;
#endif

#if DEBUG
    public override string ToString() => $"{Cell} = {Prev} ({Curr})";
#else
    public override string ToString() => $"{Cell} = {Prev}";
#endif
}
