using SudokuSolver.Restrictions;
using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    private static void Reduce(Clues clues, Cells cells, Context context, ReduceOptions options)
    {
        foreach (var (pos, val) in clues)
        {
            Remove(pos, val, cells, context);
        }
        foreach (var pos in context.Todos)
        {
            var ctx = context[pos];
            var any = false;
            foreach (var mask in ctx.Restrictions.OfType<Mask>())
            {
                ctx.Candidates &= mask.Restrict(cells);
                any = true;
            }
            if (any)
            {
                for (var i = ctx.Restrictions.Count - 1; i >= 0; i--)
                {
                    if (ctx.Restrictions[i] is Mask) ctx.Restrictions.RemoveAt(i);
                }
            }
        }

        if (options.AddCages) AddCages(cells, context);

        bool reduce;
        do
        {
            reduce = options.NakedSingles && NakedSingles(cells, context);
            reduce |= options.NakedPairs && NakedPairs(cells, context);
            reduce |= options.Hidden && Hidden(cells, context);
            reduce |= options.Intersection && Intersection(context);
            reduce |= options.XWing && XWing(context);
            reduce |= options.Swordfish && Swordfish(context);
            reduce |= options.Restrictions && Restrict(cells, context);
        }
        while (reduce);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Restrict(Cells cells, Context context)
    {
        var reduce = false;

        foreach (var pos in context.Todos)
        {
            var ctx = context[pos];

            var befor = ctx.Candidates;
            var after = befor;

            foreach (var res in ctx.Restrictions)
            {
               after &= res.Restrict(cells);
            }
            foreach (var (other, restrictions) in ctx.PairRestrictions)
            {
                var alloweds = Candidates.None;

                foreach (var val in context[other].Candidates)
                {
                    var allowed = Candidates._1_to_9;

                    foreach (var res in restrictions)
                        allowed &= res.Restrict(val);

                    alloweds |= allowed;
                }

                after &= alloweds;
            }

            if (befor != after)
            {
                ctx.Candidates = after;
                reduce = true;
            }
        }
        return reduce;
    }
}
