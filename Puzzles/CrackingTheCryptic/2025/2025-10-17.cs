namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_10_17 : CtcPuzzle
{
    public override string Title => "Who’s Afraid Of 13";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/z39UKC3Y8Po");

    public override O Duration => O.μs10;

    public override Cells Solution { get; } = Cells.New("""
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

    public override Clues Clues { get; } = Clues.New("""
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

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Sum10s()
        + Max13s();

    private static Rules Sum10s()
        => range(_9x9)
        .Select(p => new LookupPair(new(p), new(_9x9 - p - 1), Sum10));

    private static Rules Max13s()
        => Dominos.Ort.Select(dom => new LookupPair(dom.A, dom.B, Max13)).Couples();

    private static readonly LookupDigits Sum10 = LookupPair.Init(d => [10 - d]);

    private static readonly LookupDigits Max13 = LookupPair.Init(d => Digits.AtMost(13 - d));
}
