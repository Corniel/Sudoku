namespace Sudoku.Common;

public static class Compare
{
    extension(Pos p)
    {
        public Mask IsEven => Mask.Even(p);

        public Mask Clue(int digit) => new(p, [digit]);

        public Rules LT(int row, int col) => pos(row, col).GT(p);

        public Rules LT(params Pos[] others) => others.SelectMany(o => o.GreaterThan(p));

        public Rules GT(int row, int col) => p.GT((row, col));

        public Rules GT(params Pos[] others) => others.SelectMany(o => p.GreaterThan(o));

        private Rules GreaterThan(Pos other) =>
        [
            new CellSet([p, other], $"{p} > {other}"),
            new LookupPair(p, other, More),
            new LookupPair(other, p, Less),
        ];
    }

    private static readonly LookupDigits Less = LookupPair.Init(d => Digits.AtMost(d - 1));
    private static readonly LookupDigits More = LookupPair.Init(d => Digits.AtLeast(d + 1));
}
