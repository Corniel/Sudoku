using Sudoku.Restrictions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Common;

public static class Compare
{
    extension(Pos p)
    {
        public Couple<LookupPair> LT(int row, int col) => pos(row, col).GT(p);

        public Couple<LookupPair> LT(Pos other) => other.GT(p);

        public Couple<LookupPair> GT(int row, int col) => p.GT((row, col));

        public Couple<LookupPair> GT(Pos other) => new(
            new LookupPair(p, other, More),
            new LookupPair(other, p, Less));
    }

    private static readonly LookupDigits Less = LookupPair.Init(d => Digits.AtMost(d - 1));
    private static readonly LookupDigits More = LookupPair.Init(d => Digits.AtLeast(d + 1));
}
