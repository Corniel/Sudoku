namespace SudokuSolver.Techniques;

public class HiddenPairs : HiddenMultiple
{
    public static readonly IReadOnlyCollection<Values> Pairs = Values.Singles
        .SelectMany(single => Values.Singles.Select(other => single | other))
        .Where(p => p.Count == 2)
        .ToArray();

    protected override int Size => 2;

    protected override IReadOnlyCollection<Values> Hidden => Pairs;
}