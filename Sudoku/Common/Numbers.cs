namespace Sudoku.Common;

public static class Numbers
{
    /// <summary>Two digits numbers that must be placed in two cells.</summary>
    public static Rules Two(PosArray[] cells, IEnumerable<int> numbers)
    {
        var set = true;
        var units = new Digits[_9 + 1];
        var tens = new Digits[_9 + 1];
        var umask = Digits.None;
        var tmaks = Digits.None;

        foreach (var number in numbers)
        {
            var (ten, unit) = Math.DivRem(number, 10);
            set &= ten != unit;

            if ((ten is >= 1 and <= _9) && (unit is >= 1 and <= _9))
            {
                units[unit] |= ten;
                tens[ten] |= unit;
                umask |= unit;
                tmaks |= ten;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(numbers), "Numbers be reprecentable by Sudoku digits.");
            }
        }

        var u = LookupPair.Init(units);
        var t = LookupPair.Init(tens);

        foreach (var pair in cells)
        {
            if (pair.Length is not 2) throw new ArgumentException("All cells must have a size of 2.", nameof(cells));

            if (set)
                yield return new CellSet([..pair]);
            if (tmaks.Count is not 9)
                yield return new Mask(pair[0], tmaks);
            if (umask.Count is not 9)
                yield return new Mask(pair[1], umask);

            yield return new LookupPair(pair[1], pair[0], t);
            yield return new LookupPair(pair[0], pair[1], u);
        }
    }
}
