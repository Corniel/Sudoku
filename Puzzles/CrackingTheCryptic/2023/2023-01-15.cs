namespace Puzzles.CrackingTheCryptic;

public sealed class _2023_01_15 : CtcPuzzle
{
    public override string Title => "Arbitrary Code Execution";

    public override string? Author => "jovi_all";

    public override Uri? Url => new("https://youtu.be/ihggWfW5wqM");

    public override O Duration => O.μs10;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│..7│...
        .73│2.1│.56
        ───┼───┼───
        .35│.24│61.
        1.2│6.3│5.4
        64.│51.│.32
        ───┼───┼───
        35.│1.2│46.
        ...│8..│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.New("""
         521│469│873
         896│357│241
         473│281│956
         ───┼───┼───
         735│924│618
         182│673│594
         649│518│732
         ───┼───┼───
         358│192│467
         917│846│325
         264│735│189
         """);
}
