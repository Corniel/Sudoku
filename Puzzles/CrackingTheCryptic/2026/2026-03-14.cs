namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_03_14 : CtcPuzzle
{
    public override string Title => "Wingspan";

    public override string? Author => "Kaktuslav";

    public override Uri? Url => new("https://youtu.be/k7MMOd13AdQ");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        562│943│178
        138│725│649
        749│816│253
        ───┼───┼───
        487│392│516
        915│684│732
        326│157│894
        ───┼───┼───
        271│438│965
        654│279│381
        893│561│427
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        .CC│DDD│EEE
        CFF│G.M│Nne
        CF.│GMm│n.e
        ───┼───┼───
        BII│.H.│..d
        B.L│H..│..d
        BLk│...│hhd
        ───┼───┼───
        AKl│..f│.gc
        Al.│..f│ggc
        Aaa│bbb│cc.
        A=B=C=D=E a=b=c=d=e
        F=G=H=I f=g=h K=L k=l
        M=N m=n
        """)
        + Groups.Cages("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│..A│...
        ...│.A.│...
        ...│..A│...
        ───┼───┼───
        ...│...│B..
        ...│...│.B.
        ...│...│...
        A=B
        """);
}
