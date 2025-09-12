using System.Text.RegularExpressions;

namespace SudokuSolver.Constraints;

public static partial class KillerCages
{
    public static ImmutableArray<Constraint> Parse(string str)
    {
        if (NamedCage.Pattern().Matches(str) is { Count: > 0 } matches)
        {
            var lookup = new Dictionary<char, NamedCage>();

            foreach (var cage in matches.Select(NamedCage.FromMatch))
            {
                lookup[cage.Name] = cage;
            }

            Pos p = default;
            var singles = new List<KillerCage>();

            foreach (var ch in str.Take(matches[0].Index))
            {
                if (ch is '.')
                {
                    p++;
                }
                else if (char.IsAsciiLetter(ch))
                {
                    lookup[ch].Members.Add(p++);
                }
                else if (char.IsAsciiDigit(ch))
                {
                    singles.Add(new KillerCage(ch - '0', PosSet.New(p++)));
                }
            }
            return Process(
            [
                .. lookup.Values.Select(cage => new KillerCage(cage.Sum, [.. cage.Members])),
                .. singles,
            ]);
        }
        else if (Line().Matches(str) is { Count: > 0 } lines)
        {
            var cages = new List<KillerCage>();

            foreach (Match line in lines)
            {
                var sum = int.Parse(line.Groups["Sum"].Value);
                var cells = PosSet.Empty;

                foreach (var groups in Pos().Matches(line.Value).Select(p => p.Groups))
                {
                    cells |= (int.Parse(groups["Row"].Value), int.Parse(groups["Col"].Value));
                }
                cages.Add(new KillerCage(sum, cells));
            }

            return Process(cages);
        }
        throw new FormatException();
    }

    private static ImmutableArray<Constraint> Process(List<KillerCage> cages)
    {
        List<KillerCage> inverses = [];

        foreach (var r in Rules.Standard)
        {
            var house = new KillerCage(45, r.Cells);

            foreach (var cage in cages)
            {
                house -= cage;
            }
            if (house.Sum is > 0 and < 45)
            {
                inverses.Add(house);
            }
        }

        return [.. Rules.Standard, .. cages, .. inverses];
    }

    private sealed partial record NamedCage(char Name, int Sum)
    {
        public List<Pos> Members { get; } = [];

        public static NamedCage FromMatch(Match m) => new(
            m.Groups[nameof(Name)].Value[0],
            int.Parse(m.Groups[nameof(Sum)].Value));

        public override string ToString() => $"{Name} = {Sum,-2}, {string.Join(", ", Members)}";

        [GeneratedRegex(@"(?<Name>[A-Za-z])\s*=\s*(?<Sum>[0-9]{1,2})", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        public static partial Regex Pattern();
    }

    [GeneratedRegex(@"(?<Sum>[0-9]{1,2})\s*=(?<Pos>.*?\((?<Row>[0-8]{1,2}),\s*(?<Col>[0-8]{1,2})\))+", RegexOptions.CultureInvariant)]
    public static partial Regex Line();

    [GeneratedRegex(@"\((?<Row>[0-8]{1,2}),\s*(?<Col>[0-8]{1,2})\)", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
    public static partial Regex Pos();
}
