namespace Puzzles.CrackingTheCryptic;

public sealed class _2022_04_27 : CtcPuzzle
{
    public override string Title => "The Aquarium";

    public override string? Author => "rubenscube";

    public override Uri? Url => new("https://youtu.be/DUlfr6jmaNA");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.Parse("""
        483│261│579
        617│395│824
        952│748│136
        ───┼───┼───
        261│579│483
        395│824│617
        748│136│952
        ───┼───┼───
        579│483│261
        824│617│395
        136│952│748
        """);

    protected override Rules GetConstraints() =>
        Rules.Standard
        + Box(0, 0) + Box(0, 3) + Box(0, 6)
        + Box(3, 0) + Box(3, 3) + Box(3, 6)
        + Box(6, 0) + Box(6, 3) + Box(6, 6)
        + GermanWhispers.Parse("""
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│..A│...
            ...│...│B..
            ───┼───┼───
            ...│...│C..
            ...│...│...
            ...│...│...
            """);

    public static IEnumerable<Restriction> Box(int r, int c) =>
    [
        .. new Thermometer([(r + 0, c + 0), (r + 1, c + 0)]).Restrictions,
        .. new Thermometer([(r + 2, c + 1), (r + 1, c + 2), (r + 0, c + 1)]).Restrictions,
        new Mask((r + 2, c + 2), Digits.Even),
    ];
}
