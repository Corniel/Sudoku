namespace AwesomeAssertions;

internal sealed class ConstraintsAssertions(IEnumerable<Rule> subject)
{
    private readonly AssertionChain Chain = AssertionChain.GetOrCreate();

    public IEnumerable<Rule> Subject { get; } = subject;

    public void BeValidFor(Cells cells)
    {
        var violations = Subject.Validate(cells).ToArray();

        Chain
           .ForCondition(violations.Length is 0)
           .WithDefaultIdentifier("Constraints")
           .FailWith($"Expected no violations:\n- {string.Join("\n- ", violations)}");
    }
}
