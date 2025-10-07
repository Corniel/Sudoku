using SudokuSolver.Houses;

namespace SudokuSolver.Reduction;

public static class Intersection
{
    
    public static void XWing(Graph graph)
    {
        foreach (var rows in graph.Rows.Take2())
            foreach (var cols in graph.Cols.Take2())
                XWing(rows.One, rows.Two, cols.One, cols.Two, graph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void XWing(Row r1, Row r2, Col c1, Col c2, Graph graph)
    {
        var a = r1.Cells & c1.Cells;
        var b = r1.Cells & c2.Cells;
        var c = r2.Cells & c1.Cells;
        var d = r2.Cells & c2.Cells;
        var xwing = a | b | c | d;

        // we can skip those.
        if ((xwing & graph.Todo) != xwing) return;

        var candidates = Candidates._1_to_9;
        foreach (var cell in xwing)
            candidates &= graph[cell].Candidates;

        foreach (var value in candidates)
        {
            var lockRow = graph.DoesNotOccur(value, (r1.Cells | r2.Cells) ^ xwing);
            var lockCol = graph.DoesNotOccur(value, (c1.Cells | c2.Cells) ^ xwing);

            if (lockRow && !lockCol)
            {
                foreach (var cell in (c1.Cells | c2.Cells) ^ xwing)
                    graph[cell].Candidates ^= value;
            }
            else if (lockCol && !lockRow)
            {
                foreach (var cell in (r1.Cells | r2.Cells) ^ xwing)
                    graph[cell].Candidates ^= value;
            }
        }
    }

    public static void Swordfish(Graph graph)
    {
        for (var r1 = 0; r1 < graph.Rows.Length - 2; r1++)
            for (var r2 = r1 + 1; r2 < graph.Rows.Length - 1; r2++)
                for (var r3 = r2 + 1; r3 < graph.Rows.Length; r3++)
                    for (var c1 = 0; c1 < graph.Cols.Length - 2; c1++)
                        for (var c2 = c1 + 1; c2 < graph.Cols.Length - 1; c2++)
                            for (var c3 = c2 + 1; c3 < graph.Rows.Length; c3++)
                                Swordfish(graph.Rows[r1], graph.Rows[r2], graph.Rows[r3], graph.Cols[c1], graph.Cols[c2], graph.Cols[c3], graph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swordfish(Row r1, Row r2, Row r3, Col c1, Col c2, Col c3, Graph graph)
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
        if ((fish & graph.Todo) != fish) return;

        var candidates = Candidates._1_to_9;
        foreach (var cell in fish)
            candidates &= graph[cell].Candidates;

        foreach (var value in candidates)
        {
            var lockRow = graph.DoesNotOccur(value, (r1.Cells | r2.Cells | r3.Cells) ^ fish);
            var lockCol = graph.DoesNotOccur(value, (c1.Cells | c2.Cells | c3.Cells) ^ fish);
            if (lockRow && !lockCol)
            {
                foreach (var cell in (c1.Cells | c2.Cells) ^ fish)
                    graph[cell].Candidates ^= value;
            }
            else if (lockCol && !lockRow)
            {
                foreach (var cell in (r1.Cells | r2.Cells) ^ fish)
                    graph[cell].Candidates ^= value;
            }
        }
    }

}
