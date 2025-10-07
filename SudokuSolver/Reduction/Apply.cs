namespace SudokuSolver.Reduction;

public static class Apply
{
    public static void Restrictions(Graph graph)
    {
        foreach (var cell in graph.Todo & graph.Restricted)
        {
            var node = graph[cell];

            var candidates = node.Candidates;

            foreach (var restriction in node.Restrictions)
                candidates &= restriction.Restrict(graph);

            foreach (var (other, restrictions) in node.PairedRestrictions)
            {
                var paired = Candidates.None;

                foreach (var val in graph[other].Candidates)
                {
                    var allowed = Candidates._1_to_9;

                    foreach (var restriction in restrictions)
                        allowed &= restriction.Restrict(val);

                    paired |= allowed;
                }
                candidates &= paired;
            }

            node.Candidates &= candidates;
        }
    }
}
