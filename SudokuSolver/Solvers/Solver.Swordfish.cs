using SudokuSolver.Houses;
using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Swordfish(Context context)
    {
        var reduce = false;

        for (var r1 = 0; r1 < context.Rows.Length - 2; r1++)
            for (var r2 = r1 + 1; r2 < context.Rows.Length - 1; r2++)
                for (var r3 = r2 + 1; r3 < context.Rows.Length; r3++)
                    for (var c1 = 0; c1 < context.Cols.Length - 2; c1++)
                    for (var c2 = c1 + 1; c2 < context.Cols.Length - 1; c2++)
                            for (var c3 = c2 + 1; c3 < context.Cols.Length; c3++)
                                reduce |= Handle(
                            context.Rows[r1],
                            context.Rows[r2],
                            context.Rows[r3],
                            context.Cols[c1],
                            context.Cols[c2],
                            context.Cols[c3]);

        return reduce;

        bool Handle(Row r1, Row r2, Row r3, Col c1, Col c2, Col c3)
        {
            var reduce = false;

            var a = r1.Cells & c1.Cells;
            var b = r1.Cells & c2.Cells;
            var c = r1.Cells & c3.Cells;
            var d = r2.Cells & c1.Cells;
            var e = r2.Cells & c2.Cells;
            var f = r2.Cells & c3.Cells;
            var g = r3.Cells & c1.Cells;
            var h = r3.Cells & c2.Cells;
            var i = r3.Cells & c3.Cells;
            var swordfish = a | b | c | d | e | f | g | h | i;

            // we can skip those.
            if ((swordfish & context.Singles).HasAny) return false;

            var skip = swordfish | context.Singles;

            var candidates = Candidates._1_to_9;
            foreach (var cell in swordfish)
                candidates &= context[cell].Candidates;

            foreach (var value in candidates)
            {
                var lockRow = context.CanNotOccur(value, r1.Cells ^ skip)
                    && context.CanNotOccur(value, r2.Cells ^ skip)
                    && context.CanNotOccur(value, r3.Cells ^ skip);

                var lockCol = context.CanNotOccur(value, c1.Cells ^ skip)
                    && context.CanNotOccur(value, c2.Cells ^ skip)
                    && context.CanNotOccur(value, c3.Cells ^ skip);

                if (lockRow && !lockCol)
                {
                    foreach (var cell in (c1.Cells | c2.Cells | c3.Cells) ^ skip)
                        context[cell].Candidates ^= value;
                    reduce = true;
                }
                else if (lockCol && !lockRow)
                {
                    foreach (var cell in (r1.Cells | r2.Cells | r3.Cells) ^ skip)
                        context[cell].Candidates ^= value;
                    reduce = true;
                }
            }

            return reduce;
        }
    }
}
