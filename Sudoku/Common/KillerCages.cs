using System.Text.RegularExpressions;

namespace Sudoku.Common;

public static partial class KillerCages
{
    public static ImmutableArray<Rule> Parse(string str, bool isSet = true)
    {
        if (NamedCage.Parse(str) is { Length: > 0 } cs)
        {
            return
            [
                .. cs.Select(c => isSet
                ? new KillerCage(c.Sum, [.. c.Cells])
                : (Rule)new SumCage(c.Sum, [.. c.Cells]))
            ];
        }
        else if (Line().Matches(str) is { Count: > 0 } lines)
        {
            var cages = new List<Rule>();

            foreach (Match line in lines)
            {
                var sum = int.Parse(line.Groups["Sum"].Value);
                var cells = PosSet.Empty;

                foreach (var groups in Pos().Matches(line.Value).Select(p => p.Groups))
                    cells |= (int.Parse(groups["Row"].Value), int.Parse(groups["Col"].Value));

                cages.Add(isSet ? new KillerCage(sum, cells) : new SumCage(sum, cells));
            }

            return [.. cages];
        }
        throw new FormatException();
    }

    [GeneratedRegex(@"(?<Sum>[0-9]{1,2})\s*=(?<Pos>.*?\((?<Row>[0-8]{1,2}),\s*(?<Col>[0-8]{1,2})\))+", RegexOptions.CultureInvariant)]
    private static partial Regex Line();

    [GeneratedRegex(@"\((?<Row>[0-8]{1,2}),\s*(?<Col>[0-8]{1,2})\)", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
    private static partial Regex Pos();
}
