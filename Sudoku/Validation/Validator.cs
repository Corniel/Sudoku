namespace Sudoku.Validation;

public static class Validator
{
    public static IEnumerable<Violation> Validate(this IEnumerable<Rule> rules, Cells cells)
       => rules.Validate(new CellsWrapper(cells));

    public static IEnumerable<Violation> Validate(this IEnumerable<Rule> rules, SudokuCells cells)
        => rules.SelectMany(rule => rule.Validate(cells));

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<Violation> Validate(this Rule rule, SudokuCells cells)
    {
        if (rule.IsSet)
        {
            var values = Digits.None;
            foreach (var cell in rule.Cells)
            {
                var value = cells[cell].Digit;

                if (value is not 0 && values.Contains(value))
                {
                    yield return new Violation(value, Digits._1_to_9 ^ value, cell, rule);
                }
                values |= cells[cell].Digit;
            }
        }

        foreach (var res in rule.Restrictions)
        {
            var value = cells[res.AppliesTo].Digit;

            if (value is 0) continue;

            var allowed = res.Restrict(cells);

            if (!allowed.Contains(value))
            {
                yield return new Violation(value, allowed, res.AppliesTo, rule, res);
            }
        }
    }

    public static bool IsValid(this IEnumerable<Rule> rules, SudokuCells cells)
        => !rules.Validate(cells).Any();
}
