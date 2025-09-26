using SudokuSolver.Restrictions;
using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    private static PosSet Reduce(Clues clues, Cells cells, Reduction rules, ReduceOptions options)
    {
        var singles = PosSet.Empty;

        foreach (var (pos, val) in clues)
        {
            cells[pos] = val;
            rules[pos] = rules[pos].Solve(val);
            singles |= pos;
        }
        foreach (var pos in ~singles)
        {
            var ctx = rules[pos];
            var any = false;
            var candidates = ctx.Candidates;

            foreach (var mask in ctx.Restrictions.OfType<Mask>())
            {
                any = true;
                candidates &= mask.Restrict(cells);
            }
            if (any)
            {
                rules[pos] = ctx with { Candidates = candidates, Restrictions = ctx.Restrictions.RemoveRange(ctx.Restrictions.OfType<Mask>()) };
            }
        }

        bool reduce;
        do
        {
            reduce = options.NakedSingles && NakedSingle(cells, rules, ref singles);
            reduce |= options.HiddenSingles && HiddenSingles(cells, rules, ref singles);
            reduce |= options.Restrictions && Restrict(cells, rules, ref singles);
        }
        while (reduce);

        return singles;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Restrict(Cells cells, Reduction rules, ref PosSet singles)
    {
        var reduce = false;

        foreach (var pos in ~singles)
        {
            var ctx = rules[pos];

            var befor = ctx.Candidates;
            var after = befor;

            foreach (var res in ctx.Restrictions)
            {
                after &= res.Restrict(cells);
            }
            if (befor != after)
            {
                rules[ctx.Cell] = ctx with { Candidates = after };
                reduce = true;
            }
        }
        return reduce;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool NakedSingle(Cells cells, Reduction rules, ref PosSet singles)
    {
        var reduce = false;

        foreach (var pos in ~singles)
        {
            var ctx = rules[pos];
            var candidates = ctx.Candidates;
            var peers = ctx.Peers;

            foreach (var pr in ctx.Peers)
            {
                var val = cells[pr];

                if (val is not 0)
                {
                    peers = peers.Remove(pr);
                    ctx = ctx with { Candidates = ctx.Candidates ^ val };
                }
            }

            if (ctx.Candidates.HasSingle)
            {
                cells[pos] = ctx.Candidates.First();
                singles |= pos;
                reduce = true;
            }
            else
            {
                ctx = ctx with { Peers = peers };
                reduce |= candidates != ctx.Candidates;
            }
            rules[ctx.Cell] = ctx;
        }
        return reduce;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool HiddenSingles(Cells cells, Reduction rules, ref PosSet singles)
    {
        var reduce = false;
        var hidden = new PosSet[_9 + 1];

        foreach (var house in rules.Houses)
        {
            foreach (var p in house)
            {
                foreach (var v in rules[p].Candidates)
                {
                    hidden[v] |= p;
                }
            }
            for (var val = 1; val <= _9; val++)
            {
                if (hidden[val].HasSingle)
                {
                    var pos = hidden[val].First();

                    if (cells[pos] is 0)
                    {
                        rules[pos] = rules[pos] with { Candidates = Candidates.New(val) };
                        cells[pos] = val;
                        singles |= pos;
                        reduce = true;
                    }
                }
            }
        }
        return reduce;
    }
}
