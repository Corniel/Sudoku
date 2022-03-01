namespace SudokuSolver.Techniques;

/// <summary>Reduces naked triples.</summary>
/// <remarks>
/// Four cells in a row, a column or a block having only the same four of
/// candidates are called a Naked Quad.
/// 
/// All other appearances of the four candidates in the same row, column,
/// or block can be eliminated.
/// </remarks>
public class NakedQuads : NakedMultiple
{
    protected override int Size => 4;
}
