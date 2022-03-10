namespace SudokuSolver.Techniques;

public class HiddenPairs : HiddenMultiple
{
    protected override int Size => 2;

    protected override IReadOnlyCollection<Values> Hidden => Values.Pairs;
}