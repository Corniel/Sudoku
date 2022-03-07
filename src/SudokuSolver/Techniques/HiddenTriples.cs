namespace SudokuSolver.Techniques;

public class HiddenTriples : HiddenMultiple
{
    public static readonly IReadOnlyCollection<Values> Triples = Combinations()
        .Where(p => p.Count == 3)
        .ToArray();

    private static IEnumerable<Values> Combinations()
    {
        foreach(var m in Values.Singles)
        {
            foreach (var n in Values.Singles)
            {
                foreach (var o in Values.Singles)
                {
                    yield return m | n | o;
                }
            }
        }
    }

    protected override int Size => 3;

    protected override IReadOnlyCollection<Values> Hidden => Triples;
}