using SudokuSolver.Common;

namespace Specs.Rules_specs;

public class Adds
{
    [Test]
    public void sets_when_not_overlapping()
    {
        var rules = Rules.Standard + AntiKnight.All;
        rules.Count.Should().Be(179)
            .And.BeLessThan(Rules.Standard.Count + AntiKnight.All.Length);
    }
}
