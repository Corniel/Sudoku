using StrategyBased;

namespace Sudoku.Reduction;

public static class Naked
{
    public static void Pairs(Nodes graph)
    {
        foreach (var set in graph.Rules.Where(r => r.IsSet))
        {
            var updates = set.Cells & graph.Todo;

            foreach (var pair in graph.NakedCells([.. updates], 2))
            {
                foreach (var update in updates ^ pair.Cells)
                    graph[update].Digits ^= pair.Digits;
            }
        }
    }

    public static void Triples(Nodes graph)
    {
        foreach (var set in graph.Rules.Where(r => r.IsSet))
        {
            var updates = set.Cells & graph.Todo;

            foreach (var triple in graph.NakedCells([.. updates], 3))
            {
                foreach (var update in updates ^ triple.Cells)
                    graph[update].Digits ^= triple.Digits;
            }
        }
    }

    public static void Quads(Nodes graph)
    {
        foreach (var set in graph.Rules.Where(r => r.IsSet))
        {
            var updates = set.Cells & graph.Todo;

            foreach (var triple in graph.NakedCells([.. updates], 4))
            {
                foreach (var update in updates ^ triple.Cells)
                    graph[update].Digits ^= triple.Digits;
            }
        }
    }
}
