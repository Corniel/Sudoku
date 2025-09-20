namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_09 : CtcPuzzle
{
    public override string Title => "Seylla";
    public override string? Author => "Phistomefel";
    public override Uri? Url => new("https://youtu.be/DF0f15tya5Q");

    public override Clues Clues { get; } = Clues.None;

    public override Cells Solution { get; } = Cells.Parse("""
        531|498|267
        947|265|381
        286|731|495
        ---+---+---
        815|629|743
        694|317|852
        723|584|619
        ---+---+---
        469|173|528
        358|942|176
        172|856|934
        """);

     public override Rules Constraints { get; } = 
        Rules.Killer("""
        AAA|...|BB.
        ...|CC.|...
        .DD|...|EEE
        ---+---+---
        ...|.F.|...
        .G.|.F.|.H.
        IGJ|JFK|KHL
        ---+---+---
        I.J|J.K|K.L
        I.M|.N.|O.L
        I.M|NNN|O.L

        A = 9   B = 8   C = 8   D = 14  E = 18
        F = 11  G = 11  H = 6   I = 15  J = 18  K = 18  L = 27
        M = 10  N = 23  O = 10
        """);
}
