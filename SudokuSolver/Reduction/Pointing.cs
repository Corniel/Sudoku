namespace SudokuSolver.Reduction;

public static class Pointing
{
    public static void Candidates(Graph graph)
    {
        foreach (var houses in graph.Houses.Take2())
            Candidates(houses.One, houses.Two, graph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Candidates(Rule r1, Rule r2, Graph graph)
    {
        var inter = r1.Cells & r2.Cells & graph.Todo;

        // we can skip those.
        if (!inter.HasMultiple) return;

        var candidates = new int[_9 + 1];
        foreach (var cell in inter)
            foreach (var val in graph[cell].Candidates)
                candidates[val]++;

        for (var value = 1; value <= _9; value++)
        {
            if (candidates[value] is 0) continue;

            var lockRow = graph.DoesNotOccur(value, r1.Cells ^ inter);
            var lockCol = graph.DoesNotOccur(value, r2.Cells ^ inter);

            if (lockRow && !lockCol)
            {
                foreach (var cell in r2.Cells ^ inter)
                    graph[cell].Candidates ^= value;
            }
            else if (lockCol && !lockRow)
            {
                foreach (var cell in r1.Cells ^ inter)
                    graph[cell].Candidates ^= value;
            }
        }
    }

}
