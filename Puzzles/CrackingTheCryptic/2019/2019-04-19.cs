namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_04_19 : CtcPuzzle
{
    public override string Title => "Archers And Arrows";

    public override string? Author => "SudokuExplorer";

    public override Uri? Url => new("https://youtu.be/yy5Lo6O99CE");

    public override O Duration => O.μs;

    public override Cells Solution { get; } = Cells.New("""
        935│864│721
        641│237│895
        782│195│643
        ───┼───┼───
        593│486│172
        264│371│958
        178│952│436
        ───┼───┼───
        419│528│367
        826│743│519
        357│619│284
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Lines.Arrow("""
            ...│...│...
            ...│...│...
            .Fd│Df.│...
            ───┼───┼───
            G.c│C.g│.uw
            H.b│B.h│.v.
            I.A│a.i│...
            ───┼───┼───
            ...│W..│...
            ...│.V.│...
            ...│..U│...
            """);
}
