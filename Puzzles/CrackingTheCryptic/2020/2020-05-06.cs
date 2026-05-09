namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_05_06 : CtcPuzzle
{
    public override string Title => "Antiknight Killer";

    public override string? Author => "Mitchell Lee";

    public override Uri? Url => new("https://youtu.be/Zk4qNEDXFSw");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        654│893│721
        798│126│354
        321│574│698
        ───┼───┼───
        815│347│269
        963│218│547
        247│965│813
        ───┼───┼───
        479│632│185
        536│481│972
        182│759│436
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│...│...
        .AA│BB.│...
        .AA│BB.│...
        ───┼───┼───
        .CC│DD.│...
        .CC│DD.│...
        ...│EE.│YYY
        ───┼───┼───
        ...│.EI│X.Z
        ...│..I│..Z
        ...│..I│...
        A = 20  B = 15  C = 15  D = 10  E = 18
        I = 12  X = 1   Y = 12  Z = 7
        """)
        + Anti.Knight;
}
