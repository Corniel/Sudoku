using Sudoku.Reduction;

namespace StrategyBased;

public static partial class StrategyBasedSolver
{
    public static Cells Solve(Clues clues) => Solve(clues, Rules.Standard, ReduceOptions.All);

    public static Cells Solve(Clues clues, Rules rules, ReduceOptions? options = null)
    {
        options ??= ReduceOptions.All;

        var graph = Nodes.Empty & rules & clues;

        bool reduce;
        do
        {
            reduce = options.HiddenSingles /*.......*/ && graph & Hidden.Single;
            reduce |= options.NakedPairs /*.........*/ && graph & Naked.Pairs;
            reduce |= options.HiddenPairs /*........*/ && graph & Hidden.Pairs;
            reduce |= options.NakedTriples /*.......*/ && graph & Naked.Triples;
            reduce |= options.HiddenTriples /*......*/ && graph & Hidden.Triples;
            reduce |= options.NakedQuads /*.........*/ && graph & Naked.Quads;
            reduce |= options.HiddenQuads /*........*/ && graph & Hidden.Quads;
            reduce |= options.PointingDigits /*.*/ && graph & Pointing.Digits;

            reduce = reduce || (options.Restrictions /*.*/ && graph & Apply.Restrictions);
            reduce = reduce || (options.XWing /*........*/ && graph & Intersection.XWing);
            reduce = reduce || (options.Swordfish /*....*/ && graph & Intersection.Swordfish);
        }
        while (reduce && graph.Todo.HasAny);

#if DEBUG
        if (options.Log) Console.WriteLine(graph.Log());
#endif

        return Cells.New(graph);
    }
}
