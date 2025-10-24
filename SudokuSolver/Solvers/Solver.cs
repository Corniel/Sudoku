using SudokuSolver.Reduction;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    public static Cells Solve(Clues clues) => Solve(clues, Rules.Standard, ReduceOptions.Default);

    public static Cells Solve(Clues clues, Rules rules, ReduceOptions? options = null)
    {
        options ??= ReduceOptions.Default;

        var graph = Graph.Empty & rules & clues;

        _ = options.AddCages && graph & Add.Cages;

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
            reduce |= options.PointingCandidates /*.*/ && graph & Pointing.Candidates;

            reduce = reduce || (options.Restrictions /*.*/ && graph & Apply.Restrictions);
            reduce = reduce || (options.XWing /*........*/ && graph & Intersection.XWing);
            reduce = reduce || (options.Swordfish /*....*/ && graph & Intersection.Swordfish);
        }
        while (reduce && graph.Todo.HasAny);

#if DEBUG
        if (options.Log) Console.WriteLine(graph.Log());
#endif

        if (options.Backtracker && graph.Todo.HasAny)
            _ = Backtracker.New(graph, options.Log).Solve();

        return graph.Cells;
    }
}
