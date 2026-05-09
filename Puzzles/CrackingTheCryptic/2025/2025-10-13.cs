namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_10_13 : CtcPuzzle
{
    public override string Title => "Wanddeko";

    public override string? Author => "Myxo";

    public override Uri? Url => new("https://youtu.be/ite7WigjGGI");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        417│586│923
        328│914│567
        596│732│148
        ───┼───┼───
        785│641│239
        264│379│851
        931│258│476
        ───┼───┼───
        842│167│395
        173│495│682
        659│823│714
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Couples.BlackDots("""
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│..A│A..
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """)
        + Couples.WhiteDots("""
            ...│...│...
            ..A│A..│...
            ...│..B│B..
            ───┼───┼───
            ..C│C..│...
            ...│..D│D..
            ..E│E..│...
            ───┼───┼───
            ...│...│...
            ..F│F..│...
            ...│...│...
            """)
        + Couples.WhiteDots("""
            ...│...│...
            ...│...│...
            .A.│B.C│.J.
            ───┼───┼───
            .A.│B.C│.J.
            ...│...│...
            D..│F.G│H.I
            ───┼───┼───
            D..│F.G│H.I
            ...│...│...
            ...│...│...
            """)
        + LessThen("""
            ..B│A.D│E..
            ...│..G│H..
            ...│...│...
            ───┼───┼───
            ...│...│...
            ..K│J..│...
            ...│...│...
            ───┼───┼───
            ..N│M..│...
            ...│..V│W..
            ..Q│P.S│T..
            """)

        + LessThen("""
            ...│...│...
            ...│...│...
            A..│.D.│G.J
            ───┼───┼───
            B..│.E.│H.K
            ...│...│...
            .M.│.P.│.S.
            ───┼───┼───
            .N.│.Q.│.T.
            ...│...│...
            ...│...│...
            """);

    private static Rules LessThen(string str) => Lines.Parse(str).SelectMany(line =>
    {
        var (a, b) = (line[0], line[1]);
        return
        new LookupPair[]
        {
            new(a, b, Less),
            new(b, a, More),
        };
    });

    private static readonly LookupDigits Less = LookupPair.Init(d => Digits.AtMost(d - 1));
    private static readonly LookupDigits More = LookupPair.Init(d => Digits.AtLeast(d + 1));
}
