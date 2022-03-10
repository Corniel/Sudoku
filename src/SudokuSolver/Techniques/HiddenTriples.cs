namespace SudokuSolver.Techniques;

public class HiddenTriples : HiddenMultiple
{
    protected override int Size => 3;

    protected override IReadOnlyCollection<Values> Hidden => Values.Triples;
}