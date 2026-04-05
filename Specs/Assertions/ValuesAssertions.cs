using System.Diagnostics.CodeAnalysis;

namespace AwesomeAssertions;

internal sealed class ValuesAssertions(Digits subject)
{
    public Digits Subject { get; } = subject;

    public void Be(Digits expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        => ((object)Subject).Should().Be(expected, because, becauseArgs);
}
