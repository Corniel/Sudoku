namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_05_09 : CtcPuzzle
{
    public override string Title => "Jigsaw Sudoku";

    public override string? Author => "?";

    public override Uri? Url => new("https://youtu.be/wuduuLVGKDQ");

    public override O Duration => O.ms;

    public override Clues Clues { get; } = Clues.New("""
        3 . . . . . .|.|7
        -+-+---+ +-+ | |
        1|.|. .|.|.|.|.|5
         | |   +-+ +-+ |
        .|.|. . 6 8|. .|.
         | |   +---+   |
        .|.|5 .|1 9 . .|.
         | +---+---+-+ |
        .|. . 9|. .|.|.|.
         +-+ +-+   | +-+
        . .|.|. . .|. . 2
           | +-+   +---+-
        8 .|. .|. 3 . .|.
         +-+---+-+-----+
        .|. . 2 3|5 . . 1
        -+       |
        . . . . .|. . 9 .
        """);

    public override Cells Solution { get; } = Cells.New("""
        364891527
        189374265
        542168739
        625719843
        213987456
        937456182
        876523914
        498235671
        751642398
        """);

    protected override RuleSet GetConstraints() => RuleSet.Jigsaw("""
        A A A A A A A|C|H
        -+-+---+ +-+ | |
        B|D|Q Q|A|Q|A|C|H
         | |   +-+ +-+ |
        B|D|Q Q Q Q|C C|H
         | |   +---+   |
        B|D|Q Q|C C C C|H
         | +---+---+-+ |
        B|D D D|G G|H|C|H
         +-+ +-+   | +-+
        B B|D|G G G|H H H
           | +-+   +---+-
        B B|D D|G G G G|F
         +-+---+-+-----+
        B|E E E E|F F F F
        -+       |
        E E E E E|F F F F
        """);
}
