namespace SudokuSolver.Graphs;

public readonly struct CandidatesCells(Candidates candidates, PosSet cells)
{
    public readonly Candidates Candidates = candidates;
    public readonly PosSet Cells = cells;

    public override string ToString() => $"{string.Join(", ", Cells)} = {Candidates}";
}
