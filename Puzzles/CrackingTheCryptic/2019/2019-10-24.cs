namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_10_24 : CtcPuzzle
{
    public override string Title => "Square Killer";

    public override string? Author => "Christoph Seeliger";

    public override Uri? Url => new("https://youtu.be/myGqOF6blPI");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.Parse("""
        634│591│872
        821│347│596
        579│826│431
        ───┼───┼───
        367│218│945
        192│754│683
        458│963│127
        ───┼───┼───
        745│682│319
        913│475│268
        286│139│754
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + NamedCage.Parse("""
        AAB│BCD│DEF
        GHH│CCD│EEF
        GHH│III│EJF
        ───┼───┼───
        GH.│...│JJK
        LLL│...│MKK
        L..│...│MMN
        ───┼───┼───
        OPP│QQR│SSN
        OTT│QQR│UUU
        VVV│WWR│XXX
        """).SelectMany(Cages);

    private static IEnumerable<Restriction> Cages(NamedCage cage) =>
    [
        .. Group.Select(cage.Cells, (a, o) => new Unique(a, o)),
        .. Group.Select(cage.Cells, (a, o) => new Cage(a, o, Ints.SquareNumbers & Ints.Triangles[cage.Cells.Length])),
    ];
}
