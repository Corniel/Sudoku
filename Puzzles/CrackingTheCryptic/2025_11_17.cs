namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_11_17 : CtcPuzzle
{
    public override string Title => "wicked";

    public override string? Author => "aretan";

    public override Uri? Url => new("https://youtu.be/6o7caUPFY_s");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.Parse("""
        358│916│724
        672│354│819
        419│827│536
        ───┼───┼───
        246│185│973
        837│469│152
        195│732│648
        ───┼───┼───
        523│678│491
        961│543│287
        784│291│365
        """);

    public override Rules Constraints { get; }
        = Rules.Killer("""
            ..A│BBC│...
            .7A│..C│...
            aa.│...│...
            ───┼───┼───
            H..│b..│.DD
            H..│b..│..E
            ...│.cc│..E
            ───┼───┼───
            ...│...│.FF
            ...│...│d8.
            ...│.GG│d..

            A = 10  B = 10  C = 10  D = 10  E = 10  F = 10  G = 10  H = 10
            a = 5   b = 5   c = 5   d = 5
            """)
        + KillerCages.Extend
        + RenbanLines.Parse("""
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ..A│...│...
            ..A│...│...
            ..A│...│...
            ───┼───┼───
            CCC│BBB│...
            ..C│...│...
            ..C│...│...
            """)
        + GermanWhispers.Parse("""
            ...│...│.E.
            ...│...│F.H
            ...│ABC│.G.
            ───┼───┼───
            ...│...│a..
            ...│...│b..
            ...│...│c..
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """);
}
