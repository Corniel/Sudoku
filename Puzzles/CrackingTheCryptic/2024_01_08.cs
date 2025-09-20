
namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_01_08 : CtcPuzzle
{
    public override string Title => "Tulpenblüte";
    public override string? Author => "Myxo";
    public override Uri? Url => new("https://youtu.be/l2IAP57GIxU");

    public override Clues Clues { get; } = Clues.Parse("""
        ...|.2.|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        7..|...|..8
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        678|921|354
        524|378|196
        913|546|782
        ---+---+---
        849|632|571
        735|194|628
        261|857|943
        ---+---+---
        187|265|439
        496|783|215
        352|419|867
        """);

    public override Rules Constraints { get; } =
        Rules.Standard
        
        + RenbanLines.Parse("""
        AAA|...|BBB
        A..|A.B|..B
        A.F|...|F.B
        ---+---+---
        .A.|F.F|.B.
        ...|F.F|...
        .C.|.F.|.D.
        ---+---+---
        C..|...|..D
        C..|C.D|..D
        CCC|...|DDD
        """)

        + GermanWhispers.Parse("""
        ..A|...|D..
        ...|B.E|...
        a..|...|h.d
        ---+---+---
        .b.|G.g|.e.
        ...|H..|...
        .K.|...|.O.
        ---+---+---
        J..|...|..P
        ...|k.o|...
        ..j|...|p..
        """);
}
