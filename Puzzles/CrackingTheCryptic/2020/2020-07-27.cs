namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_07_27 : CtcPuzzle
{
    public override string Title => "The Mirical Sudoku 2";

    public override string? Author => "Ri Sa";

    public override Uri? Url => new("https://youtu.be/LwkNChSO2yE");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        926│473│851
        835│912│476
        417│865│293
        ───┼───┼───
        641│287│935
        592│341│687
        783│596│142
        ───┼───┼───
        164│758│329
        359│124│768
        278│639│514
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│9..│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Lines.Thermometer("""
        ...│B..│...
        ..C│GA.│I..
        ...│DFJ│M..
        ───┼───┼───
        ...│.KP│.N.
        ...│...│O..
        ...│.Vd│R..
        ───┼───┼───
        ...│.cU│aS.
        ...│..b│T..
        ...│...│...
        """);
}
