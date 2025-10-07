
namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_05_09 : CtcPuzzle
{
    public override string Title => "Jigsaw Sudoku";
    public override string? Author => "?";
    public override Uri? Url => new("https://youtu.be/wuduuLVGKDQ");
    public override O Duration => O.oo;

    public override Clues Clues { get; } = Clues.Parse("""
        3..|...|..7
        1..|...|..5
        ...|.68|...
        ---+---+---
        ..5|.19|...
        ...|9..|...
        ...|...|..2
        ---+---+---
        8..|...|...
        ...|235|..1
        ...|...|.9.
        """);

    public override Rules Constraints { get; } = Rules.Jigsaw("""
        AAA|AAA|ACH
        BDQ|QAQ|ACH
        BDQ|QQQ|CCH
        ---+---+---
        BDQ|QCC|CCH
        BDD|DGG|HCH
        BBD|GGG|HHH
        ---+---+---
        BBD|DGG|GGF
        BEE|EEF|FFF
        EEE|EEF|FFF
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        364|891|527
        189|374|265
        542|168|739
        ---+---+---
        625|719|843
        217|983|456
        973|456|182
        ---+---+---
        836|527|914
        498|235|671
        751|642|398
        """);
}
