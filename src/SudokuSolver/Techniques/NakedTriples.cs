namespace SudokuSolver.Techniques;

/// <summary>Reduces naked triples.</summary>
/// <remarks>
/// Three puzzle in a row, a column or a block having only the same triple of
/// candidates are called a Naked Triple.
/// 
/// All other appearances of the three candidates in the same row, column,
/// or block can be eliminated.
/// </remarks>
public class NakedTriples : NakedMultiple
{
    protected override int Size => 3;
    protected override IReadOnlyCollection<Values> Naked => Values.Triples;
}
