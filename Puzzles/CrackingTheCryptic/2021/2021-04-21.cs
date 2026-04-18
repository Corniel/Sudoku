namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_04_21 : CtcPuzzle
{
    public override string Title => "Ten Knights";

    public override string? Author => "FryTheGuy";

    public override Uri? Url => new("https://youtu.be/EV5blCSEzrk");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.Parse("""
        549│361│278
        863│279│541
        172│548│369
        ───┼───┼───
        925│684│137
        684│137│925
        731│925│684
        ───┼───┼───
        456│792│813
        297│813│456
        318│456│792
        """);

    protected override Rules GetConstraints() =>
        Rules.Killer("""
        ...│AAA│...
        ..C│...│BBB
        DDC│C..│...
        ───┼───┼───
        .DE│...│G..
        ..E│EFF│G..
        .HH│..I│...
        ───┼───┼───
        ..H│..I│...
        ...│..I│...
        ...│...│...
        A=10 B=10 C=10 D=10 E=10 F=10 G=10 H=10 I=10
        """)
        + Rules.AntiKnight
        + KillerCages.Extend;
}
