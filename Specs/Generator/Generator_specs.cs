using Generator;
using StrategyBased;

namespace Specs.Generator_specs;

public class Generates
{
    [Test]
    public void Puzzles()
    {
        var random = new Random(17);
        var puzzles = new PuzzleGenerator(ReduceOptions.All, random).Take(10).ToArray();

        puzzles.Should().AllSatisfy(generated =>
        {
            Rules.Standard.Should().BeValidFor(generated.Solution);

            var solved = TestSolver.Solve(generated.Clues, Rules.Standard);
            solved.Should().Be(generated.Solution);

            Console.WriteLine();
            Console.WriteLine(generated.Solution);
        });
    }
}
