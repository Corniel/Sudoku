using Sudoku.Houses;

namespace StrategyBased.Reductions;

public static class Intersection
{
    public static void XWing(Nodes cells)
    {
        foreach (var rows in cells.Rows.Take2())
            foreach (var cols in cells.Cols.Take2())
                XWing(rows.One, rows.Two, cols.One, cols.Two, cells);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void XWing(Row r1, Row r2, Col c1, Col c2, Nodes cells)
    {
        var a = r1.Cells & c1.Cells;
        var b = r1.Cells & c2.Cells;
        var c = r2.Cells & c1.Cells;
        var d = r2.Cells & c2.Cells;
        var xwing = a | b | c | d;

        // we can skip those.
        if ((xwing & cells.Todo) != xwing) return;

        var digits = Digits._1_to_9;
        foreach (var cell in xwing)
            digits &= cells[cell].Digits;

        foreach (var value in digits)
        {
            var lockRow = cells.DoesNotOccur(value, (r1.Cells | r2.Cells) ^ xwing);
            var lockCol = cells.DoesNotOccur(value, (c1.Cells | c2.Cells) ^ xwing);

            if (lockRow && !lockCol)
            {
                foreach (var cell in (c1.Cells | c2.Cells) ^ xwing)
                    cells[cell].Digits ^= value;
            }
            else if (lockCol && !lockRow)
            {
                foreach (var cell in (r1.Cells | r2.Cells) ^ xwing)
                    cells[cell].Digits ^= value;
            }
        }
    }

    public static void Swordfish(Nodes cells)
    {
        for (var r1 = 0; r1 < cells.Rows.Length - 2; r1++)
            for (var r2 = r1 + 1; r2 < cells.Rows.Length - 1; r2++)
                for (var r3 = r2 + 1; r3 < cells.Rows.Length; r3++)
                    for (var c1 = 0; c1 < cells.Cols.Length - 2; c1++)
                        for (var c2 = c1 + 1; c2 < cells.Cols.Length - 1; c2++)
                            for (var c3 = c2 + 1; c3 < cells.Rows.Length; c3++)
                                Swordfish(cells.Rows[r1], cells.Rows[r2], cells.Rows[r3], cells.Cols[c1], cells.Cols[c2], cells.Cols[c3], cells);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swordfish(Row r1, Row r2, Row r3, Col c1, Col c2, Col c3, Nodes cells)
    {
        var a = r1.Cells & c1.Cells;
        var b = r1.Cells & c2.Cells;
        var c = r1.Cells & c3.Cells;
        var d = r2.Cells & c1.Cells;
        var e = r2.Cells & c2.Cells;
        var f = r2.Cells & c3.Cells;
        var g = r3.Cells & c1.Cells;
        var h = r3.Cells & c2.Cells;
        var i = r3.Cells & c3.Cells;
        var fish = a | b | c | d | e | f | g | h | i;

        // we can skip those.
        if ((fish & cells.Todo) != fish) return;

        var digits = Digits._1_to_9;
        foreach (var cell in fish)
            digits &= cells[cell].Digits;

        foreach (var value in digits)
        {
            var lockRow = cells.DoesNotOccur(value, (r1.Cells | r2.Cells | r3.Cells) ^ fish);
            var lockCol = cells.DoesNotOccur(value, (c1.Cells | c2.Cells | c3.Cells) ^ fish);
            if (lockRow && !lockCol)
            {
                foreach (var cell in (c1.Cells | c2.Cells) ^ fish)
                    cells[cell].Digits ^= value;
            }
            else if (lockCol && !lockRow)
            {
                foreach (var cell in (r1.Cells | r2.Cells) ^ fish)
                    cells[cell].Digits ^= value;
            }
        }
    }
}
