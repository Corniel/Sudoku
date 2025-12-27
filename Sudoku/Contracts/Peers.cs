namespace Sudoku.Contracts;

public interface Peers
{
    /// <summary>The cell that has peers.</summary>
    Pos AppliesTo { get; }

    /// <summary>The linked cells.</summary>
    PosSet Links { get; }
}
