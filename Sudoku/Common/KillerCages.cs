using Sudoku.Constraints;
using System.Text.RegularExpressions;

namespace Sudoku.Common;

public static partial class KillerCages
{
    /// <summary>Extend the rules with extra cages.</summary>
    [Pure]
    public static RuleSet Extend(RuleSet rules)
    {
        HashSet<SumGroup> existing = [.. rules.OfType<Summation>().Select(r => new SumGroup(r.Cells, r.Sum))];

        HashSet<PosSet> sets = [.. rules.Sets];

        HashSet<SumGroup> combos = [];

        Dictionary<Pos, PosSet> links = [];
        foreach (var p in Pos.All)
            links[p] = default;

        foreach (var set in sets)
            foreach (var cell in set)
                links[cell] |= set ^ cell;

        var adding = true;

        while (adding)
        {
            adding = false;
            foreach (var rule in existing)
            {
                var inverse = PosSet.All;
                foreach (var c in rule.Cells)
                    inverse &= links[c];

                var full = rule.Cells | inverse;

                if (inverse.HasAny && sets.Add(full))
                {
                    adding = true;

                    if (full.Count is _9)
                    {
                        combos.Add(new(inverse, Ints.New(_45) - rule.Sum));
                    }
                }

                foreach (var other in existing.Where(o => o.Cells.IsProperSubsetOf(rule.Cells)))
                {
                    var cage = new SumGroup(rule.Cells ^ other.Cells, rule.Sum - other.Sum);

                    if (!existing.Contains(cage))
                    {
                        adding |= combos.Add(cage);
                    }
                }
            }
        }

        // We ignore big cages.
        SumGroup[] small = [.. combos.Where(c => c.Size <= 7)];

        return rules
            + small.SelectMany(c => Groups.SumCage(c.Cells, c.Sum))
            + Masks([..existing, .. combos], sets);
    }

    private static Rules Masks(IEnumerable<SumGroup> cages, HashSet<PosSet> sets) => cages
        .Where(cage => sets.Any(cage.Cells.IsSubsetOf))
        .SelectMany(Masks);

    private static Rules Masks(SumGroup cage)
    {
        if (cage.Size is _9) return [];

        var digits = Digits.None;

        foreach (var combo in Digits.All.Where(comb => comb.Count == cage.Size && cage.Sum.Contains(comb.Sum())))
            digits |= combo;

        return digits == _1_to_9
            ? []
            : cage.Cells.Select(c => new Mask(c, digits));
    }
}
