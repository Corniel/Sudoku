namespace Puzzles.CrackingTheCryptic;

public sealed class _2023_10_02 : CtcPuzzle
{
    public override string Title => "Escaltion";

    public override string? Author => "Celery";

    public override Uri? Url => new("https://youtu.be/S-eer1pRVM0");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        395│421│687
        726│983│415
        418│756│329
        ───┼───┼───
        573│148│962
        842│369│571
        961│275│843
        ───┼───┼───
        239│814│756
        154│637│298
        687│592│134
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Diagonal.NW_SE
        + Houses.Disjoints
        + Groups.Cages("""
        ...│.aa│b..
        A..│..a│...
        AA.│...│..c
        ───┼───┼───
        .BB│...│.dd
        ..B│C.e│f.d
        ...│CC.│.f.
        ───┼───┼───
        .g.│hDD│EE6
        g.g│..D│...
        ...│...│...
        A=B=C=D=E
        a=b c=d e=f g=h
        """);
}
