using System.Diagnostics.CodeAnalysis;

namespace AwesomeAssertions;

internal sealed class ValuesAssertions(Candidates subject)
{
    public Candidates Subject { get; } = subject;

    public void Be(Candidates expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ((object)Subject).Should().Be(expected, because, becauseArgs);
    }
}
