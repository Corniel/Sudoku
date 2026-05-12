namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_04_21 : CtcPuzzle
{
    public override string Title => "Partial Killer";

    public override string? Author => "Phistomefel";

    public override Uri? Url => new("https://youtu.be/ZLcey7qiXv8");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        147│296│583
        963│185│472
        528│743│691
        ───┼───┼───
        831│562│947
        792│834│156
        456│917│328
        ───┼───┼───
        619│428│735
        274│351│869
        385│679│214
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        AA.│...│..B
        ...│.X.│..B
        ..a│aX.│bZZ
        ───┼───┼───
        ...│IKK│b.J
        .Y.│IMM│.JJ
        .Yd│IOO│...
        ───┼───┼───
        ..d│..c│c..
        D.L│...│...
        D.L│L..│.CC
        A = 5   B = 5   C = 5   D = 5
        a = 15  b = 15  c = 15  d = 15
        I = 22  J = 18  L = 15  K = 8   M = 7  O = 8 
        X = 12  Y = 14  Z = 10
        """)
        + KillerCages.Extend;
}
