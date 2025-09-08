namespace SudokuSolver.Solvers;

public static partial class DynamicSolver
{
    /// <summary>Proccesses the given cells.</summary>
    /// <remarks>
    /// for all given cells:
    /// * remove its value from the candidates of its peers
    /// * remove itself for its peers
    /// * update its candidates to this single value
    /// * remove all its peers
    /// * add the cell to to the singles.
    /// </remarks>
    private static PosSet ProcessGivens(Clues clues, Cells cells, CellContext[] squares)
    {
        var solved = PosSet.Empty;

        foreach (var clue in clues)
        {
            solved |= ResolveSingle(squares[clue.Pos], Candidates.New(clue.Value), cells, squares);
        }
        return solved;
    }

    private static PosSet ResolveNakedSingles(Cells cells, CellContext[] contexts, PosSet unsolved)
    {
        var solved = PosSet.Empty;

        foreach (var p in unsolved)
        {
            var ctx = contexts[p];

            if (ctx.Candidates.HasSingle)
            {
                solved |= ResolveSingle(ctx, ctx.Candidates, cells, contexts);
            }
        }
        return solved;
    }

    private static Pos ResolveSingle(CellContext ctx, Candidates value, Cells cells, CellContext[] contexts)
    {
        foreach (var lin in ctx.Peers)
        {
            var peer = contexts[lin];

            if (peer.Candidates.HasSingle) continue;

            peer.Candidates ^= value;
            peer.Peers = peer.Peers.Remove(ctx.Pos);
        }
        ctx.Peers = [];
        ctx.Candidates = value;
        cells[ctx.Pos] = value.First();

        return ctx.Pos;
    }
}
