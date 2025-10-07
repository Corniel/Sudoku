namespace SudokuSolver.Reduction;

public static class Hidden
{
    public static void Single(Graph graph)
    {
        foreach (var house in graph.Houses)
        {
            var count = graph.Assignments(house.Cells);

            for (var val = 1; val <= _9; val++)
                if (count[val] is { HasSingle: true } single)
                    graph[single.First()].Candidates = [val];
        }
    }

    public static void Pairs(Graph graph)
    {
        foreach (var house in graph.Houses)
            Pair(house, graph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Pair(Rule house, Graph graph)
    {
        var assignments = graph.Assignments(house.Cells);

        foreach (var candidates in Combinations.Take2(assignments.WithMax(2))
            .Select(pair => Candidates.New(pair.One, pair.Two)))
        {
            var pair = PosSet.Empty;

            foreach (var value in candidates)
                pair |= assignments[value];

            if (pair.Count is 2)
            {
                var others = (house.Cells & graph.Todo) ^ pair;

                foreach (var update in pair)
                    graph[update].Candidates &= candidates;

                foreach (var update in others)
                    graph[update].Candidates ^= candidates;
            }
        }
    }

    public static void Triples(Graph graph)
    {
        foreach (var house in graph.Houses)
            Triple(house, graph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Triple(Rule house, Graph graph)
    {
        var assignments = graph.Assignments(house.Cells);

        foreach (var candidates in Combinations.Take3(assignments.WithMax(3))
            .Select(triple => Candidates.New(triple.One, triple.Two, triple.Thr)))
        {
            var triple = PosSet.Empty;

            foreach (var value in candidates)
                triple |= assignments[value];

            if (triple.Count is 3)
            {
                var others = (house.Cells & graph.Todo) ^ triple;

                foreach (var update in triple)
                    graph[update].Candidates &= candidates;

                foreach (var update in others)
                    graph[update].Candidates ^= candidates;
            }
        }
    }

    public static void Quads(Graph graph)
    {
        foreach (var house in graph.Houses)
            Quad(house, graph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Quad(Rule house, Graph graph)
    {
        var assignments = graph.Assignments(house.Cells);

        foreach (var candidates in Combinations.Take4(assignments.WithMax(4))
            .Select(quad => Candidates.New(quad.One, quad.Two, quad.Thr, quad.For)))
        {
            var quad = PosSet.Empty;

            foreach (var value in candidates)
                quad |= assignments[value];

            if (quad.Count is 4)
            {
                var others = (house.Cells & graph.Todo) ^ quad;

                foreach (var update in quad)
                    graph[update].Candidates &= candidates;

                foreach (var update in others)
                    graph[update].Candidates ^= candidates;
            }
        }
    }
}
