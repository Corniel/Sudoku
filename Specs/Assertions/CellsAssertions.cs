namespace AwesomeAssertions;

public sealed class CellsAssertions(Cells subject)
{
    private readonly AssertionChain Chain = AssertionChain.GetOrCreate();

    public Cells Subject { get; } = subject;

    public void BeSolved(Rules? rules = null)
    {
        var reference = Backtracker.Solve(Clues.Parse(Subject.ToString()));

        Be(reference, rules);
    }

    public void Be(string expected, Rules? rules = null)
        => Be(Cells.Parse(expected), rules);

    public void Be(Cells expected, Rules? rules = null)
    {
        rules ??= Rules.Standard;

        rules.Should().BeValidFor(expected);

        Chain
            .ForCondition(Subject.Equals(expected))
            .WithDefaultIdentifier("Puzzle")
            .FailWith($"Expected:\n{expected}\n\nAcutal:\n{Subject}");
    }
}
