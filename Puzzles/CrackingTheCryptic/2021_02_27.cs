namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_02_27 : CtcPuzzle
{
    public override string Title => "Mounted Archery 3";

    public override string? Author => "AFrayedKnot";

    public override Uri? Url => new("https://youtu.be/5D0byP3psnQ");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        835│674│291
        679│281│435
        142│935│687
        ───┼───┼───
        514│397│862
        368│412│759
        927│856│314
        ───┼───┼───
        251│763│948
        783│149│526
        496│528│173
        """);

    public override Rules Constraints { get; }
        = Rules.AntiKnight
        + Twins.Parse("""
        ...│...│...
        ..D│...│...
        ...│D..│A..
        ───┼───┼───
        ...│...│.A.
        ..C│...│...
        ...│C..│..B
        ───┼───┼───
        ...│...│.B.
        ...│...│...
        ...│...│...
        """)
        + Arrows.Parse("""
        ...│...│...
        .A.│.F.│LK.
        BCD│.GH│..J
        ───┼───┼───
        ...│...│...
        OP.│STU│...
        N.R│...│...
        ───┼───┼───
        YZc│dlk│...
        Xb.│h.j│...
        ..f│g.n│op.
        """);
}
