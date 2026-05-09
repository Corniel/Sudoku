namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_03_14 : CtcPuzzle
{
    public override string Title => "Pi";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/N41yZsxIsK8");

    public override O Duration => O.μs10;

    public override Clues Clues { get; } = Clues.New("""
        ...│431│...
        ..8│...│4..
        .3.│...│.1.
        ───┼───┼───
        2..│...│..5
        3..│.6.│..9
        9..│...│..2
        ───┼───┼───
        .7.│...│.6.
        ..9│...│5..
        ...│853│...
        """);

    public override Cells Solution { get; } = Cells.New("""
        762│431│958
        198│675│423
        435│928│716
        ───┼───┼───
        287│319│645
        354│267│189
        916│584│372
        ───┼───┼───
        573│192│864
        829│746│531
        641│853│297
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Anti.King;
}
