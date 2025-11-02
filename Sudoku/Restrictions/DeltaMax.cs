namespace Sudoku.Restrictions;

public sealed class DeltaMax(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
{
    public int Delta { get; } = delta;

    public override Digits Restrict(int value) => Lookup[Delta][value];

    public static readonly ImmutableArray<ImmutableArray<Digits>> Lookup = lookup();

    private static ImmutableArray<ImmutableArray<Digits>> lookup()
    {
        ImmutableArray<Digits>[] look = new ImmutableArray<Digits>[_9];

        for (var dt = 1; dt < _9; dt++)
        {
            var digits = new Digits[_9 + 1];
            digits[0] = Digits._1_to_9;

            for (var val = 1; val <= _9; val++)
                digits[val] = Digits.Between(val - dt, val + dt);

            look[dt] = [.. digits];
        }

        return [.. look];
    }
}
