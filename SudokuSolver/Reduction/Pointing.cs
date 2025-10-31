namespace Sudoku.Reduction;

public static class Pointing
{
    public static void Digits(Graph graph)
    {
        foreach (var houses in graph.Houses.Take2())
            Digits(houses.One, houses.Two, graph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Digits(Rule r1, Rule r2, Graph graph)
    {
        var inter = r1.Cells & r2.Cells & graph.Todo;

        // we can skip those.
        if (!inter.HasMultiple) return;

        var digits = new int[_9 + 1];
        foreach (var cell in inter)
            foreach (var val in graph[cell].Digits)
                digits[val]++;

        for (var value = 1; value <= _9; value++)
        {
            if (digits[value] is 0) continue;

            var lockRow = graph.DoesNotOccur(value, r1.Cells ^ inter);
            var lockCol = graph.DoesNotOccur(value, r2.Cells ^ inter);

            if (lockRow && !lockCol)
            {
                foreach (var cell in r2.Cells ^ inter)
                    graph[cell].Digits ^= value;
            }
            else if (lockCol && !lockRow)
            {
                foreach (var cell in r1.Cells ^ inter)
                    graph[cell].Digits ^= value;
            }
        }
    }
}
