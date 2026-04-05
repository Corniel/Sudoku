namespace Puzzles.CrackingTheCryptic;

public sealed class _2017_08_26 : CtcPuzzle
{
    public override string Title => "Killer Sudoku No 5596 Deadly";

    public override string? Author => "The Times";

    public override Uri? Url => new("https://youtu.be/f54CZrQUxMo");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.Parse("""
        831│654│279
        957│312│486
        624│798│135
        ───┼───┼───
        712│586│943
        495│273│861
        386│149│527
        ───┼───┼───
        173│425│698
        268│931│754
        549│867│312
        """);

    public override Rules Constraints { get; }
        = Rules.Killer("""
            AAA│BBC│CCC
            AEE│FFG│HHH
            DEL│FFG│IIJ
            ───┼───┼───
            DEL│FNG│OIJ
            KKL│MNO│ORJ
            SKL│MNP│PRU
            ───┼───┼───
            SSM│MNQ│QUU
            VST│TTT│YYY
            VVV│WWW│WXX

            A = 21  B = 11  C = 22  D = 13  E = 15  F = 25  G = 16  H = 18
            I = 8   J = 9   K = 21  L = 17  M = 10  N = 21  O = 20  P = 14
            Q = 11  R = 8   S = 17  T = 21  U = 24  V = 20  W = 24  X = 3  Y = 16
            """)
        + KillerCages.Extend;
}
