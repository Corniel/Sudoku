namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_03 : CtcPuzzle
{
    public override string Title => "24 Squares!";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/XmtIOoVaMzY");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.New("""
        297│315│486
        456│897│231
        138│642│957
        ───┼───┼───
        673│184│592
        581│729│364
        924│563│178
        ───┼───┼───
        742│958│613
        319│276│845
        865│431│729
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + pos(0, 6).Clue(4)
        + Numbers.New(
        Grid.NamedGroups("""
        Aa.│...│.Bb
        aAC│...│cbB
        ICD│d.E│ec.
        ───┼───┼───
        I.d│D.e│E..
        .FF│ZZZ│ff.
        ..G│g.H│h..
        ───┼───┼───
        .Jg│G.h│Hj.
        KkJ│...│jLl
        kK.│...│.lL
        """).Select(group => group.OrderBy(c => c.Col).ToImmutableArray()),
        Powers);

    private static readonly ImmutableArray<int> Powers = [16, 25, 36, 49, 64, 81, 169, 196, 256, 289, 324, 361, 529, 576, 625, 729, 841, 961];
}
