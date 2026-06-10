namespace Sudoku.Common;

public static class Compare
{
    extension(Pos p)
    {
        public Mask IsEven => Mask.Even(p);

        public Rules LT(int row, int col) => pos(row, col).GT(p);

        public Rules LT(Pos other) => other.GT(p);

        public Rules GT(int row, int col) => p.GT((row, col));

        public Rules GT(Pos other) =>
        [
            new CellSet([p, other], $"{p} > {other}"),
            new LookupPair(p, other, More),
            new LookupPair(other, p, Less),
        ];
    }

    private static readonly LookupDigits Less = LookupPair.Init(d => Digits.AtMost(d - 1));
    private static readonly LookupDigits More = LookupPair.Init(d => Digits.AtLeast(d + 1));
}
