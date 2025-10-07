namespace SudokuSolver.Restrictions;

public sealed class DeltaMin(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
{
    public int Delta { get; } = delta;

    public override Candidates Restrict(int value) => Lookup[Delta][value];

    public static readonly ImmutableArray<ImmutableArray<Candidates>> Lookup = lookup();

    private static ImmutableArray<ImmutableArray<Candidates>> lookup()
    {
        ImmutableArray<Candidates>[] look = new ImmutableArray<Candidates>[_9];

        for (var dt = 1; dt < _9; dt++)
        {
            var candidates = new Candidates[_9 + 1];
            candidates[0] = Candidates._1_to_9;

            for (var val = 1; val <= _9; val++)
                candidates[val] = ~Candidates.Between(val - dt + 1, val + dt - 1);

            look[dt] = [.. candidates];
        }

        return [..look];
    }
}
