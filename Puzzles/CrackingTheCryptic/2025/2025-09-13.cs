namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_13 : CtcPuzzle
{
    public override string Title => "Royalty";

    public override string? Author => "zetamath";

    public override Uri? Url => new("https://youtu.be/uyTSKJ1DB6c");

    public override O Duration => O.ms100;

    public override Clues Clues { get; } = Clues.New("""
        ...|...|.8.
        ..6|...|...
        1..|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Lines.Renban("""
        AAA|BCC|C..
        A..|B.D|C..
        .EE|DDD|FFF
        ---+---+---
        .EG|GG.|..F
        JEG|..H|IIF
        J.G|HHH|I..
        ---+---+---
        JJ.|...|I.K
        .LM|MMN|.KK
        LLM|NNN|.K.
        """);

    public override Cells Solution { get; } = Cells.New("""
        324|956|781
        596|871|432
        178|324|956
        ---+---+---
        861|543|297
        753|219|648
        942|768|513
        ---+---+---
        685|197|324
        439|682|175
        217|435|869
        """);
}
