using SudokuSolver.Common;
using SudokuSolver.Restrictions;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    /// <summary>Adds cages based on existing cages.</summary>
    private static void AddCages(Cells cells, Context context)
    {
        List<KillerCage> inverses = [];

        foreach (var house in context.Houses.Select(h => h.Cells))
        {
            var inverse = house;
            var sum = _45;
            var any = false;

            foreach (var cage in context.Rules)
            {
                if (cage is FixedSum fix && cage.Cells.IsSubsetOf(inverse))
                {
                    sum -= fix.Sum;
                    inverse ^= cage.Cells;
                    any = true;
                }
            }

            if (!any) continue;

            foreach (var cell in inverse)
            {
                var val = cells[cell];
                if (val is not 0)
                {
                    sum -= cell;
                    inverse ^= cell;
                }
            }

            // If any left add the killer cage.
            if (sum is > 0)
                inverses.Add(new KillerCage(sum, inverse));
        }

        ExtendContext();

        void ExtendContext()
        {
            context.Rules.AddRange(inverses);

            foreach (var inverse in inverses)
                foreach (var res in inverse.Restrictions)
                    context[res.AppliesTo].Restrictions.Add(res);
        }
    }
}
