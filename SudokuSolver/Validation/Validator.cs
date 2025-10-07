namespace SudokuSolver.Validation;

public static class Validator
{
    public static IEnumerable<Violation> Validate(this IEnumerable<Rule> rules, Cells cells)
    {
        var graph = Graph.Empty;

        foreach (var cell in Pos.All)
            graph[cell].Test(cells[cell]);

        return rules.SelectMany(rule => rule.Validate(graph));
    }

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<Violation> Validate(this Rule rule, Graph graph)
    {
        if (rule.IsSet)
        {
            var values = Candidates.None;
            foreach (var cell in rule.Cells)
            {
                var value = graph[cell].Value;

                if (value is not 0 && values.Contains(value))
                {
                    yield return new Violation(value, Candidates._1_to_9 ^ value, cell, rule);
                }
                values |= graph[cell].Value;
            }
        }

        foreach (var res in rule.Restrictions)
        {
            var value = graph[res.AppliesTo].Value;

            if (value is 0) continue;

            var allowed = res.Restrict(graph);

            if (!allowed.Contains(value))
            {
                yield return new Violation(value, allowed, res.AppliesTo, rule, res);
            }
        }
    }

    public static bool IsValid(this IEnumerable<Rule> rules, Cells cells)
        => !rules.Validate(cells).Any();
}
