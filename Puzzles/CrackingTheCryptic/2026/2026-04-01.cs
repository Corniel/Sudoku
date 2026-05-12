namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_01 : CtcPuzzle
{
    public override string Title => "Wilkommen im Palindrom";

    public override string? Author => "Billybeth";

    public override Uri? Url => new("https://youtu.be/PcJNQCckiKs");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        745│286│913
        926│713│845
        318│549│627
        ───┼───┼───
        167│935│482
        834│172│596
        259│468│371
        ───┼───┼───
        572│394│168
        691│857│234
        483│621│759
        """);

    private const string GreenLine = """
        ...│...│...
        ..A│..N│M..
        .B.│.PO│.LK
        ───┼───┼───
        ..C│Q..│..J
        UTR│DEF│.I.
        V.S│..G│Hef
        ───┼───┼───
        .WX│...│dg.
        .Y.│a.c│...
        ..Z│.b.│...
        """;

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.GermanWhisper(GreenLine)
        + Lines.Palindrome(GreenLine)
        + new Pos(8, 8).GT((8, 7))
        + new Pos(8, 8).GT((7, 8));
}
