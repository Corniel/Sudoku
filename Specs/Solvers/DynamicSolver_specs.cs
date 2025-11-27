namespace Specs.Solvers.DynamicSolver_specs;

public class Solves
{
    [Test]
    public void Without_Clues()
    {
        var cells = DynamicSolver.Solver.Solve(Clues.None, Rules.Standard);
        cells.Should().Be("""
            123│456│789
            456│789│123
            789│123│456
            ───┼───┼───
            231│674│895
            875│912│364
            694│538│217
            ───┼───┼───
            317│265│948
            542│897│631
            968│341│572
            """);
    }
}
