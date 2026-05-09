namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_20 : CtcPuzzle
{
    public override string Title => "Williwaw";

    public override string? Author => "Nicolas Duhail";

    public override Uri? Url => new("https://youtu.be/0cvA-XDiQNQ");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        584│312│976
        629│758│431
        137│964│258
        ───┼───┼───
        715│436│892
        863│291│547
        492│875│163
        ───┼───┼───
        276│149│385
        958│623│714
        341│587│629
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Groups.Cages("""
        ...│...│...
        ACY│XX.│xca
        ACY│..X│xca
        ───┼───┼───
        B.D│ZZy│db.
        ...│.Z.│...
        EE.│Ggg│.ee
        ───┼───┼───
        F.F│H.h│f.f
        .F.│.Hh│.fi
        III│H..│ii.
        A=B C=D E=F G=H=I X=Y=Z
        a=b c=d e=f g=h=i x=y
        """);
}
