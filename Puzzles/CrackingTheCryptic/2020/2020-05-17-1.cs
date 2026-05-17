namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_05_17_1 : CtcPuzzle
{
    public override string Title => "Thermo Miracle Sudoku";

    public override string? Author => "Wei-Hwa Huang and Bram Cohen";

    public override Uri? Url => new("https://youtu.be/Tv-48b-KuxI");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        369│714│258
        825│369│714
        471│825│369
        ───┼───┼───
        936│471│825
        582│936│471
        147│582│936
        ───┼───┼───
        693│147│582
        258│693│147
        714│258│693
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Anti.King
        + NonConsecutives.Orthogonally()
        + Lines.Thermometer("""
        ...│...│...
        ...│...│b..
        ...│...│a..
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        .CB│A..│...
        ...│...│...
        ...│...│...
        """);
}
