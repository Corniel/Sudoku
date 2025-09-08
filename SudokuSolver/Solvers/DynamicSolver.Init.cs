namespace SudokuSolver.Solvers;

public static partial class DynamicSolver
{
    public static Cells Solve(Clues clues, Rules rules)
    {
        var peers = range(_9x9, _ => PosSet.Empty);
        var restrictions = range(_9x9, _ => new List<Restriction>());

        foreach (var @set in rules.Sets)
            foreach (var peer in @set)
                peers[peer] |= set;

        foreach (var restriction in rules.Restrictions)
            restrictions[restriction.AppliesTo].Add(restriction);

        return Solve(
            clues,
            [
                .. Pos.All.Select(p => new CellContext(p)
                {
                    Peers = [.. peers[p] ^ p],
                    Restrictions = [.. restrictions[p]],
                })
            ],
            rules);
    }

    public static Cells Solve(Clues clues)
        => Solve(clues, [.. Pos.All.Select(InitStandard)], Rules.Standard);

    private static CellContext InitStandard(Pos p) => new(p) { Peers = Standard[p] };

    public static readonly ImmutableArray<ImmutableArray<Pos>> Standard = [.. InitStandard()];

    private static IEnumerable<ImmutableArray<Pos>> InitStandard()
    {
        var set = new PosSet[_9x9];

        for (Pos p = Pos.First; p <= Pos.Last; p++)
            foreach (var dis in Houses.Standard.Where(d => d.Contains(p)))
                set[p] |= dis;

        return set.Select((s, p) => (s ^ new Pos(p)).ToImmutableArray());
    }

    private static T[] range<T>(int size, Func<int, T> select) => [.. Enumerable.Range(0, size).Select(select)];
}
