using SudokuSolver.Restrictions;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_10_17 : CtcPuzzle
{
    public override string Title => "Who’s Afraid Of 13";
    public override string? Author => "Aad van de Wetering";
    public override Uri? Url => new("https://youtu.be/z39UKC3Y8Po");
    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.Parse("""
        843│917│625
        176│285│394
        925│643│817
        ───┼───┼───
        258│439│176
        761│852│943
        439│176│258
        ───┼───┼───
        392│764│581
        617│528│439
        584│391│762
        """);

    public override Clues Clues { get; } = Clues.Parse("""
        ...│9..│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│..6│...
        ───┼───┼───
        ...│...│...
        6..│...│...
        ...│...│...
        """);

    public override Rules Constraints { get; }
        = Rules.Standard
        + Sum10s()
        + Max13s();

    private static IEnumerable<Sum10> Sum10s()
        => range(_9x9)
        .Select(p => new Sum10(new(p), new(_9x9 - p - 1)));

    private static IEnumerable<Max13> Max13s()
    {
        foreach (var p in Pos.All)
        {
            if (p.N() is { } n) yield return new Max13(p, n);
            if (p.E() is { } e) yield return new Max13(p, e);
            if (p.S() is { } s) yield return new Max13(p, s);
            if (p.W() is { } w) yield return new Max13(p, w);
        }
    }

    public sealed class Sum10(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public override Candidates Restrict(int value) => [10 - value];
    }

    public sealed class Max13(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public override Candidates Restrict(int value) => Restrictions[value];

        private static readonly ImmutableArray<Candidates> Restrictions =
        [
            /* 0 */ Candidates._1_to_9,
            /* 1 */ Candidates._1_to_9,
            /* 2 */ Candidates._1_to_9,
            /* 3 */ Candidates._1_to_9,
            /* 4 */ Candidates._1_to_9,
            /* 5 */ [1, 2, 3, 4, 5, 6, 7, 8],
            /* 6 */ [1, 2, 3, 4, 5, 6, 7],
            /* 7 */ [1, 2, 3, 4, 5, 6],
            /* 8 */ [1, 2, 3, 4, 5],
            /* 9 */ [1, 2, 3, 4],
        ];
    }
}
