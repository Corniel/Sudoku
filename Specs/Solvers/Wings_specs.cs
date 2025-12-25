using StrategyBased;

namespace Specs.Solvers.Wings_specs;

public class Detects
{
    [Test]
    public void Ws()
    {
        var nodes = Nodes.Empty;
        var clues = Clues.Parse("""
            .78│.26│3..
            63.│...│...
            .5.│4..│.68
            ───┼───┼───
            89.│...│4.2
            ...│...│51.
            ...│...│89.
            ───┼───┼───
            ..6│8..│...
            5..│.49│...
            ..3│5.2│...
            """);

        var solver = new StrategyBasedSolver(nodes & Rules.Standard & clues, new(StrategyType.WWing));
        nodes[(3, 2)].Digits = [1, 7];
        nodes[(7, 8)].Digits = [1, 7];

        _ = solver.First();

        nodes[(7, 2)].Digits.Should().Be([2, 7]);
    }
}
