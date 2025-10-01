using SudokuSolver.Houses;
using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool XWing(Context context)
    {
        var reduce = false;

        for (var r1 = 0; r1 < context.Rows.Length - 1; r1++)
            for (var r2 = r1 + 1; r2 < context.Rows.Length; r2++)
                for (var c1 = 0; c1 < context.Cols.Length - 1; c1++)
                    for (var c2 = c1 + 1; c2 < context.Cols.Length; c2++)
                        reduce |= Handle(
                            context.Rows[r1],
                            context.Rows[r2],
                            context.Cols[c1],
                            context.Cols[c2]);

        return reduce;

        bool Handle(Row r1, Row r2, Col c1, Col c2)
        {
            var reduce = false;

            var a = r1.Cells & c1.Cells;
            var b = r1.Cells & c2.Cells;
            var c = r2.Cells & c1.Cells;
            var d = r2.Cells & c2.Cells;
            var xwing = a | b | c | d;

            // we can skip those.
            if ((xwing & context.Singles).HasAny) return false;

            var skip = xwing | context.Singles;

            var candidates = Candidates._1_to_9;
            foreach (var cell in xwing)
                candidates &= context[cell].Candidates;

            foreach (var value in candidates)
            {
                var lockRow = context.CanNotOccur(value, r1.Cells ^ skip)
                    && context.CanNotOccur(value, r2.Cells ^ skip);

                var lockCol = context.CanNotOccur(value, c1.Cells ^ skip)
                    && context.CanNotOccur(value, c2.Cells ^ skip);

                if (lockRow && !lockCol)
                {
                    foreach (var cell in (c1.Cells | c2.Cells) ^ skip)
                        context[cell].Candidates ^= value;
                    reduce = true;
                }
                else if (lockCol && !lockRow)
                {
                    foreach (var cell in (r1.Cells | r2.Cells) ^ skip)
                        context[cell].Candidates ^= value;
                    reduce = true;
                }
            }

            return reduce;
        }
    }
}
