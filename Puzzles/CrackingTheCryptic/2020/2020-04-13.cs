namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_04_13 : CtcPuzzle
{
    public override string Title => "Killer Sudoku";

    public override string? Author => "William Andrews";

    public override Uri? Url => new("https://youtu.be/jTA6nPhxfPI");

    public override O Duration => O.ms;

    public override Cells Solution => Cells.Parse("""
        463│915│287
        528│673│194
        971│824│653
        ───┼───┼───
        346│597│821
        812│346│975
        795│182│346
        ───┼───┼───
        287│431│569
        634│259│718
        159│768│432
        """);

    protected override Rules GetConstraints()
        => Rules.Killer("""
        AAE│JKK│KNO
        AAE│JKG│GNO
        AAD│DBB│GOO
        ───┼───┼───
        APP│PPQ│QQQ
        RRR│SSS│SSQ
        RLL│LII│IMM
        ───┼───┼───
        CTT│TVV│WWM
        CTU│UVV│WHH
        TTU│UVF│FHH

        A = 36  B = 6   C = 8   D = 9   E = 11  F = 12  G = 10  H = 14  I = 13
        J = 15  K = 15  L = 15  M = 19  N = 17  O = 19  P = 24  Q = 23  R = 18
        S = 29  T = 28  U = 22  V = 24  W = 18
        """)
        + KillerCages.Extend;
}
