namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_12_30 : CtcPuzzle
{
    public override string Title => "Dotless Kropki Sudoku X";

    public override string? Author => "Phistomefel";

    public override Uri? Url => new("https://youtu.be/1QP7yviZYTU");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        .1.│...│.2.
        ...│.4.│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.New("""
        268│174│935
        593│826│417
        741│359│682
        ───┼───┼───
        417│593│826
        682│741│359
        935│268│174
        ───┼───┼───
        359│682│741
        174│935│268
        826│417│593
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.XSudoku
        + Dominos.Ort.SelectMany(d => new LookupPair(d, Kropki).Couple());

    private static readonly int _ = 0;
    private static readonly LookupDigits Kropki = LookupPair.Init(
    [
        Digits._1_to_9,
        [_, _, 3, 4, 5, 6, 7, 8, 9],
        [_, _, _, _, 5, 6, 7, 8, 9],
        [1, _, _, _, 5, _, 7, 8, 9],
        [1, _, _, _, _, 6, 7, 8, 9],
        [1, 2, 3, _, _, _, 7, 8, 9],
        [1, 2, _, 4, _, _, _, 8, 9],
        [1, 2, 3, 4, 5, _, _, _, 9],
        [1, 2, 3, _, 5, 6, _, _, _],
        [1, 2, 3, 4, 5, 6, 7, _, _],
    ]);
}
