namespace Puzzles.CrackingTheCryptic;

public sealed class _2018_06_07 : CtcPuzzle
{
    public override string Title => "Hard 2018-06-07";

    public override string? Author => "The New York Times";

    public override Uri? Url => new("https://youtu.be/mma8xJcMfTQ");

    public override O Duration => O.μs100;

    public override bool IsStandard => true;

    public override Clues Clues { get; } = Clues.Parse("""
        ..6│.9.│...
        17.│..3│.9.
        ...│7..│..5
        ───┼───┼───
        ...│5..│6..
        .9.│.3.│2..
        ..4│..2│1..
        ───┼───┼───
        ...│978│...
        .4.│..5│.8.
        ...│..6│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
         356│294│871
         178│653│492
         429│781│365
         ───┼───┼───
         812│549│637
         695│137│248
         734│862│159
         ───┼───┼───
         263│978│514
         941│325│786
         587│416│923
         """);
}
