namespace DynamicSolver;

#if DEBUG
public readonly struct Step(Pos cell, Digits prev, Digits curr, Digits mask)
#else
public readonly struct Step(Pos cell, Digits prev)
#endif
{
    public readonly Pos Cell = cell;
    public readonly Digits Prev = prev;
#if DEBUG
    public readonly Digits Curr = curr;
    public readonly Digits Mask = mask;
#endif

#if DEBUG
    public override string ToString() => $"{Cell}: {Curr} = {Prev} & {Mask}";
#else
    public override string ToString() => $"{Cell} = {Prev}";
#endif
}
