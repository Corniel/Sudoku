namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_01_13 : CtcPuzzle
{
    public override string Title => "Quality Street";

    public override string? Author => "Jay Dyer";

    public override Uri? Url => new("https://youtu.be/oSG0c7PZ7ME");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        879│634│521
        561│289│734
        423│715│698
        ───┼───┼───
        634│178│952
        792│453│186
        158│962│347
        ───┼───┼───
        987│526│413
        346│891│275
        215│347│869
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.XSudoku
        + Groups.Cages("""
        ...│Gk.│l..
        ..F│..k│l..
        .FF│AA.│...
        ───┼───┼───
        Eaa│..B│...
        K.a│...│C..
        K..│b..│CCg
        ───┼───┼───
        .L.│.cc│ff.
        ...│..c│f..
        ...│..e│...
        
        A=B=C E=F=G K=L
        a=b=c e=f=g k=l
        """);
}
