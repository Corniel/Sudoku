namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_04_19 : CtcPuzzle
{
    public override string Title => "Archers And Arrows";

    public override string? Author => "SudokuExplorer";

    public override Uri? Url => new("https://youtu.be/yy5Lo6O99CE");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
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

    protected override Rules GetConstraints() =>
        Rules.AntiKnight
        + Arrows.Parse("""
        ...│...│...
        ...│...│...
        .Ai│dF.│...
        ───┼───┼───
        B.h│c.G│.KM
        C.g│b.H│.L.
        D.a│f.I│...
        ───┼───┼───
        ...│Q..│...
        ...│.P.│...
        ...│..O│...
        """);
}
