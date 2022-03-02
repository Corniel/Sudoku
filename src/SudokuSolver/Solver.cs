﻿using SudokuSolver.Techniques;

namespace SudokuSolver;

public static class Solver
{
    public static IEnumerable<Reduction> Solve(Cells cells, IEnumerable<Technique>? techniques = null)
        => Solve(cells, Regions.Default, techniques);

    public static IEnumerable<Reduction> Solve(Cells cells, Regions regions, IEnumerable<Technique>? techniques = null)
    {
        techniques ??= Techniques;
        var running = true;
        
        while(running)
        {
            running = false;

            foreach(var technique in techniques)
            {
                if (technique.Reduce(cells, regions) is { } reduced)
                {
                    running = true;
                    cells = reduced;
                    yield return new Reduction(reduced, technique.GetType());
                    break;
                }
            }
        }
    }

    private static readonly Technique[] Techniques = new Technique[] 
    {
        new NakedSingles(),
        new Singles(),
        new NakedPairs(),
        new HiddenPairs(),
        new PointingPair(),
        new NakedTriples(),
        new NakedQuads(),
        new SteeringWheel(),
    };
}