namespace SudokuSolver.Techniques;

public class HiddenQuads : HiddenMultiple
{
    public static readonly IReadOnlyCollection<Values> Quads = Combinations()
        .Where(p => p.Count == 4)
        .ToArray();

    private static IEnumerable<Values> Combinations()
    {
        foreach(var m in Values.Singles)
        {
            foreach (var n in Values.Singles)
            {
                foreach (var o in Values.Singles)
                {
                    foreach (var p in Values.Singles)
                    {
                        yield return m | n | o | p;
                    }
                }
            }
        }
    }

    protected override int Size => 4;

    protected override IReadOnlyCollection<Values> Hidden => Quads;
}