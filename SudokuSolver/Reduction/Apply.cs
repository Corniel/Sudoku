namespace Sudoku.Reduction;

public static class Apply
{
    public static void Restrictions(Graph graph)
    {
        foreach (var cell in graph.Todo & graph.Restricted)
        {
            var node = graph[cell];

            var digits = node.Digits;

            foreach (var restriction in node.Restrictions)
                digits &= restriction.Restrict(graph);

            foreach (var (other, restrictions) in node.PairedRestrictions)
            {
                var paired = Digits.None;

                foreach (var val in graph[other].Digits)
                {
                    var allowed = Digits._1_to_9;

                    foreach (var restriction in restrictions)
                        allowed &= restriction.Restrict(val);

                    paired |= allowed;
                }
                digits &= paired;
            }

            node.Digits &= digits;
        }
    }
}
