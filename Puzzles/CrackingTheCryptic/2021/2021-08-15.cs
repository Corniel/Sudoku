namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_08_15 : CtcPuzzle
{
    public override string Title => "Steering Wheel";

    public override string? Author => "SudokuExplorer";

    public override Uri? Url => new("https://youtu.be/Ui1hrp7rovw");

    public override O Duration => O.μs100;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│1.2│...
        .6.│...│.7.
        ..8│...│9..
        ───┼───┼───
        4..│...│..3
        .5.│..7│...
        2..│.8.│..1
        ───┼───┼───
        ..9│...│8.5
        .7.│...│.6.
        ...│3.4│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
         934│172│658
         561│948│372
         728│635│914
         ───┼───┼───
         417│269│583
         853│417│296
         296│583│741
         ───┼───┼───
         149│726│835
         372│851│469
         685│394│127
         """);
}
