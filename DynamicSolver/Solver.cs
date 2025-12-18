namespace DynamicSolver;

public static class Solver
{
    private static readonly Iterator Iterator = new();

    public static Iterator Iterate(Clues clues, Rules rules)
        => Iterator.Set(clues, rules);

    public static IEnumerable<Cells> FindAll(Clues clues, Rules rules)
        => Iterator.Set(clues, rules).Select(Cells.New);

    public static Links Raw(Clues clues, Rules rules)
    {
        Iterator.Set(clues, rules);
        Iterator.MoveNext();
        return Iterator.Current;
    }

    public static Cells Solve(Clues clues, Rules rules) => Cells.New(Raw(clues, rules));
}
