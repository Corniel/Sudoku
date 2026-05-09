namespace AwesomeAssertions;

public sealed class CellsAssertions(Cells subject)
{
    private readonly AssertionChain Chain = AssertionChain.GetOrCreate();

    public Cells Subject { get; } = subject;

    public void Be(string expected, RuleSet? rules = null)
        => Be(Cells.New(expected), rules);

    public void Be(Cells expected, RuleSet? rules = null)
    {
        rules ??= RuleSet.Standard;

        ((RuleSet)rules).Should().BeValidFor(Subject);

        Chain
            .ForCondition(Subject.Equals(expected))
            .WithDefaultIdentifier("Puzzle")
            .FailWith($"Expected:\n{expected}\n\nAcutal:\n{Subject}");
    }
}
