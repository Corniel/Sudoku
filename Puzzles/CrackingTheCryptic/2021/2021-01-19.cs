namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_01_19 : CtcPuzzle
{
    public override string Title => "German Whispers";

    public override string? Author => "Philipp Blume";

    public override Uri? Url => new("https://youtu.be/nH3vat8z9uM");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│.1.│...
        .5.│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        6..│...│..9
        ...│...│...
        ───┼───┼───
        ..3│...│...
        ...│...│.3.
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        796│413│852
        352│689│417
        184│275│693
        ───┼───┼───
        247│591│386
        615│348│279
        839│762│541
        ───┼───┼───
        923│857│164
        478│126│935
        561│934│728
        """);

    protected override Rules GetConstraints() =>
        Rules.Standard
        + GermanWhispers.Parse("""
        ...│..F│G..
        ...│.E.│.H.
        ...│D..│cI.
        ───┼───┼───
        ..C│.ab│J..
        .B.│..K│.q.
        ..A│..L│..p
        ───┼───┼───
        fg.│O.M│no.
        e.h│.N.│m..
        .ji│..l│...
        """);
}
