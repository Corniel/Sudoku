namespace Sudoku.Restrictions;

public sealed class Cage2(Pos appliesTo, Pos other, Ints sum)
    : Pair(appliesTo, other)
    , Summation
{
    public Ints Sum { get; } = sum;

    public override Digits Restrict(Digits other) => (Sum - other).Digits;

    public override string ToString() => $"Cage[{Sum}] {AppliesTo} => {Other}";
}
