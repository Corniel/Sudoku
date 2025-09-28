using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Remove(Pos cell, int value, Cells cells, Context context)
    {
        if (cells[cell] is not 0) return false;

        foreach (var peer in context[cell].Peers ^ context.Singles)
        {
            var ctx = context[peer];
            ctx.Peers ^= cell;
            ctx.Candidates ^= value;
        }

        cells[cell] = value;
        context.Singles |= cell;
        context[cell].Candidates = Candidates.New(value);
        return true;
    }

    /// <summary>Removes values from peers.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Remove(PosSet cells, Candidates values, PosSet peers, Context context)
    {
        var reduce = false;

        peers ^= cells;
        peers ^= context.Singles;

        // trim value and remove peers.
        foreach (var cell in cells)
        {
            var ctx = context[cell];
            ctx.Candidates &= values;
            ctx.Peers ^= peers;
        }

        // trim value and remove trimmed cells.
        foreach (var peer in peers)
        {
            var ctx = context[peer];
            var befor = ctx.Candidates;
            var after = befor ^ values;

            if (befor != after)
            {
                reduce = true;
                ctx.Candidates = after;
                ctx.Peers ^= cells;
            }
        }
        return reduce;
    }
}
