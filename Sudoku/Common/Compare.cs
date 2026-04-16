using Sudoku.Restrictions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Common;

public static class Compare
{
    extension(Pos pos)
    {
        public Couple<LookupPair> LT(Pos other) => other.GT(pos);

        public Couple<LookupPair> GT(Pos other) => new(
            new LookupPair(pos, other, More),
            new LookupPair(other, pos, Less));
    }

    private static readonly LookupDigits Less = LookupPair.Init(d => Digits.AtMost(d - 1));
    private static readonly LookupDigits More = LookupPair.Init(d => Digits.AtLeast(d + 1));
}
