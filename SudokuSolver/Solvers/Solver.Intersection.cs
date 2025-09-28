using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Intersection(Cells cells, Context context)
    {
        var reduce = false;

        for (var f = 0; f < context.Houses.Length - 1; f++)
        {
            for (var s = f + 1; s < context.Houses.Length; s++)
            {
                var fs = context.Rules[f].Cells;
                var ss = context.Houses[s].Cells;

                if ((fs & ss) is not { HasMultiple: true } intersection) continue;

                fs ^= intersection;
                ss ^= intersection;

                var counts = Counts(intersection);

                if (!counts.Any(c => c >= 1)) continue;

                var firsts = Counts(fs);
                var second = Counts(ss);

                for (var val = 1; val <= _9; val++)
                {
                    if (counts[val] < 2) continue;

                    reduce |= (firsts[val] is 0, second[val] is 0) switch
                    {
                        (false, true) => Remove(val, fs),
                        (true, false) => Remove(val, ss),
                        _ => false,
                    };
                }
            }
        }

        return reduce;

        int[] Counts(PosSet set)
        {
            var counts = new int[_9 + 1];

            foreach (var p in set)
                foreach (var v in context[p].Candidates)
                    counts[v]++;

            return counts;
        }

        bool Remove(int val, PosSet intersection)
        {
            foreach (var p in intersection)
                context[p].Candidates ^= val;

            return true;
        }
    }
}
