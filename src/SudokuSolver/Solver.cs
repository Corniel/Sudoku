using SudokuSolver.Techniques;

namespace SudokuSolver;

public static class Solver
{
    public static IEnumerable<Reduction> Solve(Puzzle puzzle, IEnumerable<Technique>? techniques = null)
        => Solve(puzzle, Regions.Default, techniques);

    public static IEnumerable<Reduction> Solve(Puzzle puzzle, Regions regions, IEnumerable<Technique>? techniques = null)
    {
        techniques ??= Techniques;
        var running = true;
        
        while(running)
        {
            running = false;

            foreach(var technique in techniques)
            {
                var reduced = technique.Reduce(puzzle, regions);

                if(reduced != puzzle)
                {
                    running = true;
                    puzzle = reduced;
                    yield return new Reduction(reduced, technique.GetType());
                    break;
                }
            }
        }
    }

    private static readonly Technique[] Techniques = new Technique[] 
    {
        new NakedSingles(),
        new HiddenSingles(),
        new NakedPairs(),
        new PointingPair(),
        new HiddenPairs(),
        new NakedTriples(),
        new PointingTriple(),
        new NakedQuads(),
        new XWing(),
        new SteeringWheel(),
    };
}
