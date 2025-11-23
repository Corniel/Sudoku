namespace Sudoku.Validation;

public static class Validator
{
    public static IEnumerable<Violation> Validate(this Rules rules, Cells cells)
       => rules.Validate(new CellsWrapper(cells));

    public static IEnumerable<Violation> Validate(this Rules rules, SudokuCells cells) =>
    [
        .. rules.SelectMany(rule => rule.Validate(cells)),
        .. rules.Restrictions.SelectMany(res => res.Validate(cells)),
    ];

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<Violation> Validate(this Rule rule, SudokuCells cells)
    {
        if (rule.IsSet)
        {
            var values = Digits.None;

            foreach (var cell in rule.Cells)
            {
                var digits = cells[cell].Digits;

                if (digits.HasSingle && (values & digits).HasAny)
                {
                    yield return new Violation(digits, Digits._1_to_9 ^ digits, cell, rule);
                }
                values |= cells[cell].Digit;
            }
        }
    }

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<Violation> Validate(this Restriction restriction, SudokuCells cells)
    {
        var digits = cells[restriction.AppliesTo].Digits;
        var allowed = restriction.Restrict(cells);

        if ((digits & allowed).HasNone)
        {
            yield return new Violation(digits, allowed, restriction.AppliesTo, null, restriction);
        }
    }

    public static bool IsValid(this Rules rules, SudokuCells cells)
        => !rules.Validate(cells).Any();
}
