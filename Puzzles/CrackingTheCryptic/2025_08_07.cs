namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_08_07 : CtcPuzzle
{
    public override string Title => "Unstable Seesaws";
    public override string? Author => "Game_puzzles & Piatato";
    public override Uri? Url => new("https://youtu.be/OUlAVsaWnDQ");

    public override Clues Clues { get; } = Clues.None;

    public override ImmutableArray<Constraint> Constraints { get; } =
    [
        .. KillerCages.Parse("""
        ...|...|..1
        ...|...|...
        AAA|A..|...
        ---+---+---
        BBB|B..|...
        ...|.3.|...
        CCC|C..|...
        ---+---+---
        DDD|D..|...
        ...|...|.6.
        ...|...|...
        A = 21  B = 21  C = 20  D = 22
        """),

        .. Consecutive.Parse("""
        B..|AA.|...
        B..|...|...
        ...|.CD|...
        ---+---+---
        ...|.CD|...
        ..E|E..|...
        ...|.YZ|...
        ---+---+---
        ...|.YZ|...
        .F.|...|...
        .F.|...|...
        """),

        .. GermanWhispers.Parse("""
        87.|...|...
        .6.|...|...
        ...|...|adg
        ---+---+---
        ...|...|beh
        ...|...|...
        ...|...|ADG
        ---+---+---
        ...|...|BEH
        23.|...|...
        1..|KL.|...
        """),
    ];

    public override Cells Solution { get; } = Cells.Parse("""
        296|784|351
        341|295|876
        578|163|492
        ---+---+---
        483|652|917
        617|839|245
        952|417|683
        ---+---+---
        764|528|139
        829|371|564
        135|946|728
        """);
}
