namespace SudokuSolver.Reduction;

public static class Naked
{
    public static void Pairs(Graph graph)
    {
        foreach (var set in graph.Rules.Where(r => r.IsSet))
        {
            var updates = set.Cells & graph.Todo;

            foreach (var pair in graph.NakedCells([.. updates], 2))
            {
                foreach (var update in updates ^ pair.Cells)
                    graph[update].Candidates ^= pair.Candidates;
            }
        }
    }

    public static void Triples(Graph graph)
    {
        foreach (var set in graph.Rules.Where(r => r.IsSet))
        {
            var updates = set.Cells & graph.Todo;

            foreach (var triple in graph.NakedCells([.. updates], 3))
            {
                foreach (var update in updates ^ triple.Cells)
                    graph[update].Candidates ^= triple.Candidates;
            }
        }
    }

    public static void Quads(Graph graph)
    {
        foreach (var set in graph.Rules.Where(r => r.IsSet))
        {
            var updates = set.Cells & graph.Todo;

            foreach (var triple in graph.NakedCells([.. updates], 4))
            {
                foreach (var update in updates ^ triple.Cells)
                    graph[update].Candidates ^= triple.Candidates;
            }
        }
    }
}
