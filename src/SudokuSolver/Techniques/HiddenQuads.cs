namespace SudokuSolver.Techniques;

public class HiddenQuads : HiddenMultiple
{
    protected override int Size => 4;

    protected override IReadOnlyCollection<Values> Hidden => Values.Quads;
}