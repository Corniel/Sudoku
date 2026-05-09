namespace Sudoku.Common;

public static class Couples
{
    /// <summary>Black dots describe pairs that have a 1:2 ratio.</summary>
    public static Rules BlackDots(string grid)
        => Grid.NamedGroups(grid)
            .Where(group => group.Size is 2)
            .SelectMany(group => Ratio1_2(group.First(), group.Last()));

    /// <summary>White dots describe pairs that are consecutive.</summary>
    public static Rules WhiteDots(string grid)
        => Grid.NamedGroups(grid)
            .Where(group => group.Size is 2)
            .SelectMany(group => Consecutive(group.First(), group.Last()));

    public static Rules Twins(string grid)
        => Grid.NamedGroups(grid)
         .Where(group => group.Size is 2)
        .SelectMany(group => Dominos.RoundRobin([.. group.Cells]))
        .SelectMany(domino => Twin(domino.A, domino.B));

    public static Rules Consecutive(Pos a, Pos b) =>
    [
        new CellSet([a, b], nameof(Consecutive)),
        .. new LookupPair(a, b, Consecutives).Couple(),
    ];

    public static Rules Ratio1_2(Pos a, Pos b) =>
    [
        new CellSet([a, b], "Ratio 1:2"),
        .. new LookupPair(a, b, Ratios1_2).Couple(),
    ];

    public static IEnumerable<Twin> Twin(Pos a, Pos b) => [new Twin(a, b), new Twin(b, a)];

    private static readonly LookupDigits Consecutives = LookupPair.Init(d => [d - 1, d + 1]);

    private static readonly LookupDigits Ratios1_2 = LookupPair.Init(
    [
        /* 0 */ Digits._1_to_9,
        /* 1 */ [2],
        /* 2 */ [1, 4],
        /* 3 */ [6],
        /* 4 */ [2, 8],
        /* 5 */ default,
        /* 6 */ [3],
        /* 7 */ default,
        /* 8 */ [4],
        /* 9 */ default,
    ]);
}
