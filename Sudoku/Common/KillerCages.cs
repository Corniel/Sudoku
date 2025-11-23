using Sudoku.Restrictions;
using System.Text.RegularExpressions;

namespace Sudoku.Common;

public static partial class KillerCages
{
    [Pure]
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

    /// <summary>Extend the rules with extra cages.</summary>
    [Pure]
    public static Rules Extend(Rules rules)
    {
        var houses = rules.Sets.Where(s => s.Count is _9).ToArray();
        var cages = rules.OfType<KillerCage>().ToArray();
        var found = new List<KillerCage>();

        // Make use of the fact that houses sum up to 45
        foreach (var house in houses)
        {
            var cage = house;
            var sum = _45;

            foreach (var c in cages.Where(c => c.Cells.IsSubsetOf(cage)))
            {
                cage ^= c.Cells;
                sum -= c.Sum;
            }
            if (sum is > 0 and < _45)
                found.Add(new KillerCage(sum, cage));
        }

        // Check if we can make use some cages we just found
        var count = found.Count;
        for (var i = 0; i < count; i++)
        {
            var cage = found[i];
            foreach (var c in cages)
            {
                if ((cage.Cells & c.Cells) is { HasAny: true } overlay)
                {
                    // c is a proper subset of cage
                    if (c.Cells == overlay)
                        found.Add(new(cage.Sum - c.Sum, cage.Cells ^ c.Cells));
                    else if (cage.Cells == overlay)
                        found.Add(new(c.Sum - cage.Sum, c.Cells ^ cage.Cells));
                }
            }
        }

        return rules + found + Masks([.. cages, .. found], rules.Sets);
    }

    private static IEnumerable<Mask> Masks(IEnumerable<KillerCage> cages, IEnumerable<PosSet> sets) => cages
        .Where(c
            => c.Count is 2
            && c.Sum.IsEven()
            && sets.Any(s => c.Cells.IsSubsetOf(s)))
        .SelectMany(Masks);

    private static IEnumerable<Mask> Masks(KillerCage c) =>
    [
        new Mask(c.Cells.First(), ~Digits.New(c.Sum / 2)),
        new Mask(c.Cells.Last(), ~Digits.New(c.Sum / 2)),
    ];

    [GeneratedRegex(@"(?<Sum>[0-9]{1,2})\s*=(?<Pos>.*?\((?<Row>[0-8]{1,2}),\s*(?<Col>[0-8]{1,2})\))+", RegexOptions.CultureInvariant)]
    private static partial Regex Line();

    [GeneratedRegex(@"\((?<Row>[0-8]{1,2}),\s*(?<Col>[0-8]{1,2})\)", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
    private static partial Regex Pos();
}
