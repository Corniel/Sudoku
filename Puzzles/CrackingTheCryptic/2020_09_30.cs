
namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_09_30 : CtcPuzzle
{
    public override string Title => "Tatooine Sunset";
    public override string? Author => "Philip Newman";
    public override Uri? Url => new("https://youtu.be/V38qsL1cmFs");

    public override Clues Clues { get; } = Clues.Parse("""
        ...|...|...
        ..9|8..|..7
        .8.|.6.|.5.
        ---+---+---
        .5.|.4.|.3.
        ..7|9..|..2
        ...|...|...
        ---+---+---
        ..2|7..|..9
        .4.|.5.|.6.
        3..|..6|2..
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        124|573|896
        569|814|327
        783|269|154
        ---+---+---
        251|647|938
        437|985|612
        896|321|475
        ---+---+---
        612|738|549
        948|152|763
        375|496|281
        """);
}
