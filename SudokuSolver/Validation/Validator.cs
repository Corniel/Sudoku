namespace SudokuSolver.Validation;

public static class Validator
{
    public static IEnumerable<Violation> Validate(this IEnumerable<Constraint> constraints, Cells cells)
        => constraints.SelectMany(c => c.Validate(cells));

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<Violation> Validate(this Constraint constraint, Cells cells)
    {
        if (constraint.IsSet)
        {
            var values = Candidates.None;
            foreach (var cell in constraint.Cells)
            {
                var value = cells[cell];

                if (value is not 0 && values.Contains(value))
                {
                    yield return new Violation(value, Candidates._1_to_9 ^ value, cell, constraint);
                }
                values |= cells[cell];
            }
        }

        foreach (var res in constraint.Restrictions)
        {
            var value = cells[res.AppliesTo];

            if (value is 0) continue;

            var allowed = res.Restrict(cells);

            if (!allowed.Contains(value))
            {
                yield return new Violation(value, allowed, res.AppliesTo, constraint, res);
            }
        }
    }

    public static bool IsValid(this IEnumerable<Constraint> constraints, Cells cells)
        => !constraints.Validate(cells).Any();
}
