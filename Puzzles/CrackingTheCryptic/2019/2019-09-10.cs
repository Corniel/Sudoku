namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_09_10 : CtcPuzzle
{
    public override string Title => "Killer Sudoku";

    public override string? Author => "Christoph Seeliger";

    public override Uri? Url => new("https://youtu.be/2v6Lf3Q5AEo");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
         827│156│349
         193│487│256
         465│923│187
         ───┼───┼───
         614│732│895
         378│549│612
         259│861│734
         ───┼───┼───
         546│218│973
         782│395│461
         931│674│528
         """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        AAA│BBB│CCC
        ADD│DEF│FGC
        AHH│IEK│FGL
        ───┼───┼───
        AHI│IJK│KKL
        MNI│IJO│OKL
        MNN│NJO│OPQ
        ───┼───┼───
        MST│NXO│PPQ
        RST│TXV│VVQ
        RRR│UUU│QQQ
        A=28 B=12 C=22 D=16 E=10 F=10 G=13 H=12 I=33 J=13 K=23 L=14 M=10
        N=31 O=31 P=19 Q=23 R=20 S=12 T=11 U=17 V=15 X=10
        """)
        + KillerCages.Extend;
}
