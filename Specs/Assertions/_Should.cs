using System.Diagnostics.Contracts;

namespace AwesomeAssertions;

internal static class _Should
{
    [Pure]
    public static CellsAssertions Should(this Cells cells) => new(cells);

    [Pure]
    public static ValuesAssertions Should(this Candidates values) => new(values);

    [Pure]
    public static ConstraintsAssertions Should(this IEnumerable<Rule>sonstraints) => new(sonstraints);
}
