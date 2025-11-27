namespace StrategyBased;

public sealed record ReduceOptions()
{
    public ReduceOptions(params IReadOnlyCollection<StrategyType> strategies) : this() => Strategies = [.. strategies];

    public ImmutableArray<Strategy> Strategies { get; init; } = [];

    public bool Log { get; init; }

    public static readonly ReduceOptions All = new()
    {
        Strategies =
        [
            StrategyType.HiddenSingles,

            StrategyType.PointingDigits,
            StrategyType.HiddenPairs,

            StrategyType.NakedPairs,
            StrategyType.XWing,

            StrategyType.HiddenTriples,
            StrategyType.NakedTriples,

            StrategyType.Swordfish,

            StrategyType.HiddenQuads,
            StrategyType.NakedQuads,
        ],
    };


    public ReduceOptions Without(params IReadOnlyCollection<StrategyType> strategies)
        => this with
        {
            Strategies = [.. Strategies.Where(s => !strategies.Contains(s.Type))],
        };
}
