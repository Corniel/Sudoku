namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_07_10 : CtcPuzzle
{
    public override string Title => "White Room";

    public override string? Author => "Philip Newman";

    public override Uri? Url => new("https://youtu.be/ejhtYYvUs5M");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        693│154│872
        815│672│394
        742│398│516
        ───┼───┼───
        927│485│163
        381│267│945
        456│913│728
        ───┼───┼───
        139│826│457
        278│549│631
        564│731│289
        """);

    public override Rules Constraints { get; }
        = Rules.Killer("""
        ...│...│...
        .A.│..B│B..
        .A.│...│CC.
        ───┼───┼───
        .A.│..D│D..
        ...│...│...
        ..G│G..│..E
        ───┼───┼───
        ..H│G..│..E
        ..H│...│...
        ...│..F│F..

        A = 7  B = 5  C = 6  D = 6  E = 15  F = 3  G = 23  H = 17
        """);
}
