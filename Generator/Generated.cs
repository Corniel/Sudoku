using StrategyBased;
using System.Diagnostics;

namespace Generator;

[DebuggerDisplay("Hardest = {Hardest}, Strategies = {Strategies.Length}, Clues = {Clues.Count}")]
public sealed record Generated : IComparable<Generated>
{
    public required Clues Clues { get; init; }

    public required Cells Solution { get; init; }

    public required Rules Rules { get; init; }

    public required ImmutableArray<StrategyType> Strategies { get; init; }

    public StrategyType Hardest => Strategies.Max();

    public int CompareTo(Generated? other)
        => Hardest.CompareTo(other!.Hardest) switch
        {
            0 => Strategies.Length.CompareTo(other.Strategies.Length) switch
            {
                0 => -Clues.Count.CompareTo(other.Clues.Count),
                var len => len,
            },
            var max => max,
        };
}
