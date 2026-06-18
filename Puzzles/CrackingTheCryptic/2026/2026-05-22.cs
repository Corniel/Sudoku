namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_22 : CtcPuzzle
{
    public override string Title => "Epitome";

    public override string? Author => "Duhail";

    public override Uri? Url => new("https://youtu.be/htJRav3X9Ik");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        581│692│743
        762│834│951
        349│715│628
        ───┼───┼───
        958│461│372
        634│287│519
        127│359│486
        ───┼───┼───
        295│173│864
        476│928│135
        813│546│297
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        AA.│BCC│...
        A..│B.C│..D
        ..F│F..│.DD
        ───┼───┼───
        EEF│FF.│.GG
        H..│F..│I.G
        HH.│..I│IJJ
        ───┼───┼───
        ...│.II│IX.
        ..x│L.K│XX.
        .xx│LLK│...
        A = 20  B = 14  C = 15  D = 11
        E = 14  F = 36  G = 18  H = 9
        I = 36  J = 14  X = 10  x = 10
        K = 14  L = 18
        """)
        + KillerCages.Extend;
}
