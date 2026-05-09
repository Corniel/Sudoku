namespace StrategyBased.Reductions;

public static class Pointing
{
    public static void Digits(Nodes cells)
    {
        foreach (var houses in cells.Houses.Take2())
            Digits(houses.One, houses.Two, cells);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Digits(House r1, House r2, Nodes cells)
    {
        var inter = r1.Cells & r2.Cells & cells.Todo;

        // we can skip those.
        if (!inter.HasMultiple) return;

        var digits = new int[_9 + 1];
        foreach (var cell in inter)
            foreach (var val in cells[cell].Digits)
                digits[val]++;

        for (var value = 1; value <= _9; value++)
        {
            if (digits[value] is 0) continue;

            var lockRow = cells.DoesNotOccur(value, r1 ^ inter);
            var lockCol = cells.DoesNotOccur(value, r2 ^ inter);

            if (lockRow && !lockCol)
            {
                foreach (var cell in r2 ^ inter)
                    cells[cell].Digits ^= value;
            }
            else if (lockCol && !lockRow)
            {
                foreach (var cell in r1 ^ inter)
                    cells[cell].Digits ^= value;
            }
        }
    }
}
