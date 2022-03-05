namespace SudokuSolver.Techniques;

/// <summary>Reduces naked pairs.</summary>
/// <remarks>
/// Two puzzle in a row, a column or a block having only the same pair of
/// candidates are called a Naked Pair.
/// 
/// All other appearances of the two candidates in the same row, column,
/// or block can be eliminated.
/// </remarks>
public class NakedPairs : NakedMultiple
{
    protected override int Size => 2;
}
