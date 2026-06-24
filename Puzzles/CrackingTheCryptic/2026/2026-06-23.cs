namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_23 : CtcPuzzle
{
    public override string Title => "Good and Plenty";

    public override string? Author => "Blobz";

    public override Uri? Url => new("https://youtu.be/t7cZjAyU50s");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        126│745│839
        547│938│621
        839│216│745
        ───┼───┼───
        692│157│483
        451│683│297
        783│492│156
        ───┼───┼───
        974│321│568
        315│864│972
        268│579│314
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Groups.Cages("""
        AA.│...│.BB
        AA.│...│.BB
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        CC.│...│.DD
        CC.│...│.DD
        A:124 B:123 C:123 D:124
        """)
        + Couples.WhiteDots("""
        ...│...│.A.
        BB.│.C.│XA.
        ...│C..│X..
        ───┼───┼───
        ..D│.E.│.G.
        ..D│E.F│FG.
        YY.│...│...
        ───┼───┼───
        ...│...│JJ.
        H..│.I.│...
        H..│.I.│...
        """)
        + Quadruple.Extend;
}
