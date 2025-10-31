namespace SudokuSolver.Graphs;

public readonly struct CandidatesCells(Digits digits, PosSet cells)
{
    public readonly Digits Digits = digits;
    public readonly PosSet Cells = cells;

    public override string ToString() => $"{string.Join(", ", Cells)} = {Digits}";
}
