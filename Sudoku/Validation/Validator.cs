namespace Sudoku.Validation;

public static class Validator
{
    public static IEnumerable<Violation> Validate(this RuleSet rules, Cells cells)
       => rules.Validate(new CellsWrapper(cells));

    public static IEnumerable<Violation> Validate(this RuleSet rules, SudokuCells cells) =>
    [
        .. rules.OfType<Set>().SelectMany(set => set.Validate(cells)),
        .. rules.Restrictions.SelectMany(res => res.Validate(cells)),
        .. rules.Constraints.SelectMany(con => con.Validate(cells)),
    ];

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<SetViolation> Validate(this Set rule, SudokuCells cells)
    {
        var digits = Digits.None;
        var violations = PosSet.Empty;

        foreach (var cell in rule.Cells)
        {
            var digit = cells[cell].Digit;

            if (digit is not 0 && digits.Contains(digit))
            {
                violations |= cell;
            }
            else digits |= digit;
        }

        if (violations.HasAny)
        {
            yield return new SetViolation(violations, rule);
        }
    }

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<RestrictionViolation> Validate(this Restriction restriction, SudokuCells cells)
    {
        var digits = cells[restriction.AppliesTo].Digits;
        var allowed = restriction.Restrict(cells);

        if ((digits & allowed).HasNone)
        {
            yield return new RestrictionViolation(digits, allowed, restriction.AppliesTo, restriction);
        }
    }

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public static IEnumerable<ConstraintViolation> Validate(this Constraint constraint, SudokuCells cells)
    {
        if (!constraint.IsSatisfied(cells))
        {
            yield return new ConstraintViolation(constraint);
        }
    }
}
