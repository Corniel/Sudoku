using Sudoku.Constraints;

namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>Lines that represent an arrow.</summary>
    public static RulesExtender Arrow(string grid) => rules =>
    {
        return rules
            + Parse(grid).SelectMany(line => Arrow(line, rules.Sets));

        static Rules Arrow(Line line, IEnumerable<PosSet> sets)
        {
            var min = sets.Any(line.Set.IsSubsetOf)
                ? triangle(line.Length - 1)
                : line.Length - 1;

            return
            [
                new SumGroup([.. line], (min * 2)..18),
                new Mask(line[0], Digits.AtLeast(min)),
                new Arrow.Circle(line[0], line[1..]),
                .. Group.Select(line[1..], (a, o) => new Arrow.Shaft(line[0], a, o)),
            ];
        }
    };
}
