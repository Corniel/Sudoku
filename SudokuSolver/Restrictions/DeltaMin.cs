namespace SudokuSolver.Restrictions;

public sealed class DeltaMin(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
{
    public int Delta { get; } = delta;

    public override double Bits => Infos[Delta];

    public override Candidates Restrict(int value) => Lookup[Delta][value];

    public static readonly ImmutableArray<ImmutableArray<Candidates>> Lookup = lookup();

    public static readonly ImmutableArray<double> Infos = infos();

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

    private static ImmutableArray<double> infos()
    {
        var bits = new double[_9];
        var look = lookup();

        for (var dt = 1; dt < _9; dt++)
            bits[dt] = Info.Avg(look[dt].Sum(v => v.Count) / 10d);

        return [.. bits];
    }
}
