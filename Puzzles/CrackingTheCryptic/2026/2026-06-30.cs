namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_30 : CtcPuzzle
{
    public override string Title => "Stishovite";

    public override string? Author => "bellsita and Wisteria Fall";

    public override Uri? Url => new("https://youtu.be/a79ewtzPrrY");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        365│921│874
        814│657│329
        792│384│651
        ───┼───┼───
        481│573│296
        237│169│548
        956│248│137
        ───┼───┼───
        129│436│785
        548│712│963
        673│895│412
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + RuleSet.Jigsaw("""
        AAB│...│CAA
        AAB│B.C│CAA
        BBB│B.C│CCC
        ───┼───┼───
        .BB│...│CC.
        ...│...│...
        .DD│...│EE.
        ───┼───┼───
        DDD│D.E│EEE
        ..D│D.E│E..
        ..D│...│E..
        """)
        + Lines.Renban("""
        ..A│...│...
        ..A│A..│...
        CCB│B..│...
        ───┼───┼───
        .CB│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
        + Lines.GermanWhisper("""
        AD.│...│.ad
        BC.│...│.bc
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
        + Grid.NamedGroups("""
        ...│...│A..
        ...│..A│A..
        ...│..B│BCC
        ───┼───┼───
        ...│...│BC.
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """).SelectMany(Mod3s)
        + Lines.Parity("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        .cx│...│...
        ───┼───┼───
        aby│z..│...
        ..l│m..│...
        ..k│...│...
        """)
        + Groups.EvenOdd("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│.O.│...
        ...│...│...
        ...│E.E│...
        ───┼───┼───
        ...│...│...
        .E.│...│.E.
        ...│...│...
        """)
        + Couples.Ratio1_2((4, 3), (5, 3))
        + Couples.Consecutive((5, 4), (6, 4));

    private static Rules Mod3s(NamedGroup group)
    {
        PosArray cells = [.. group.Cells];
        return
        [
            .. new LookupPair(cells[0], cells[1], Mod3).Couple(),
            .. new LookupPair(cells[0], cells[2], Mod3).Couple(),
            .. new LookupPair(cells[1], cells[2], Mod3).Couple(),
        ];
    }

    private static readonly LookupDigits Mod3 = LookupPair.Init([
        Digits.None,

        [_, 2, 3, _, 5, 6, _, 8, 9],
        [1, _, 3, 4, _, 6, 7, _, 9],
        [1, 2, _, 4, 5, _, 7, 8, _],

        [_, 2, 3, _, 5, 6, _, 8, 9],
        [1, _, 3, 4, _, 6, 7, _, 9],
        [1, 2, _, 4, 5, _, 7, 8, _],

        [_, 2, 3, _, 5, 6, _, 8, 9],
        [1, _, 3, 4, _, 6, 7, _, 9],
        [1, 2, _, 4, 5, _, 7, 8, _],
    ]);

    private const int _ = 0;
}
