using Generator;

namespace Specs.Generator.Candidates_specs;

public class Generates
{
    [Test]
    public void Puzzles()
    {
        var random = new Random(17);
        new Grids( random).Take(100)
            .Should().AllSatisfy(cells =>
            {
                Rules.Standard.Should().BeValidFor(cells);

                Console.WriteLine();
                Console.WriteLine(cells);
            });
    }
}
