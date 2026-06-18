namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_25 : CtcPuzzle
{
    public override string Title => "Blue Caterpillars";

    public override string? Author => "Arachno";

    public override Uri? Url => new("https://youtu.be/GIVSl6pfT5Q");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.New("""
        598│734│612
        147│926│385
        362│185│974
        ───┼───┼───
        956│372│841
        821│469│753
        473│851│269
        ───┼───┼───
        635│218│497
        719│643│528
        284│597│136
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│B.E│...
        AA.│B.E│F..
        AAA│CC.│.F.
        ───┼───┼───
        .G.│.DD│...
        .GG│...│...
        J..│H..│.LL
        ───┼───┼───
        .K.│.I.│M..
        .K.│.II│MM.
        ...│...│MM.
        A=B C=D E=F G=H=I J=K L=M
        """)
        + KillerCages.Extend;
}
