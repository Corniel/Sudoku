namespace SudokuSolver;

/// <summary>Describes a restriction.</summary>
public interface Restriction
{
    /// <summary>The cell that is restricted.</summary>
    Pos AppliesTo { get; }

    /// <summary>The remaining candidates based on the restriction.</summary>
    Candidates Restrict(Cells cells);
}
