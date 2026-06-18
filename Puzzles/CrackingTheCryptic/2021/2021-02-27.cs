namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_02_27 : CtcPuzzle
{
    public override string Title => "Mounted Archery 3";

    public override string? Author => "AFrayedKnot";

    public override Uri? Url => new("https://youtu.be/5D0byP3psnQ");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
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

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Lines.Arrow("""
        ...│...│...
        .AB│.C.│ee.
        aaa│bcc│D.E
        ───┼───┼───
        ...│...│.d.
        ffh│ggg│...
        F.G│H..│..n
        ───┼───┼───
        iij│jll│.N.
        IJ.│k.L│...
        ..K│k.M│mm.
        """);
}
