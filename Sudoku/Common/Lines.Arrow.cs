using Sudoku.Constraints;

namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>Lines that represent an arrow.</summary>
    /// <remarks>
    /// The uppercase letter is the circle, the lowercase letters the shaft.
    /// </remarks>
    public static RulesExtender Arrow(string grid) => rules =>
    {
        var groups = Grid.NamedGroups(grid).ToDictionary(g => g.Name, g => g);

        return rules
            + groups.Keys.Where(char.IsUpper)
            .SelectMany(n => Arrow(groups[n].Single(), [.. groups[char.ToLower(n)]], rules.Sets));

        static Rules Arrow(Pos circle, PosArray shaft, IEnumerable<PosSet> sets)
        {
            PosSet line = [circle, .. shaft];

            var min = sets.Any(line.IsSubsetOf)
                ? triangle(line.Count - 1)
                : line.Count - 1;

            return
            [
                new SumGroup([.. line], (min * 2)..18),
                new Mask(circle, Digits.AtLeast(min)),
                new Arrow.Circle(circle, shaft),
                .. Group.Select(shaft, (a, o) => new Arrow.Shaft(circle, a, o)),
            ];
        }
    };
}
