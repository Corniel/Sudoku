using StrategyBased.Reductions;
using System.Collections.Frozen;

namespace StrategyBased;

public sealed record Strategy(StrategyType Type, Reduce Reduce)
{
    public static implicit operator Strategy(StrategyType type) => new(type, Lookup[type]);

    private static readonly FrozenDictionary<StrategyType, Reduce> Lookup = new Dictionary<StrategyType, Reduce>
    {
        [StrategyType.HiddenSingles] /*..*/ = Hidden.Single,
        [StrategyType.HiddenPairs] /*....*/ = Hidden.Pairs,
        [StrategyType.HiddenTriples] /*..*/ = Hidden.Triples,
        [StrategyType.HiddenQuads] /*....*/ = Hidden.Quads,

        [StrategyType.NakedPairs] /*.....*/ = Naked.Pairs,
        [StrategyType.NakedTriples] /*...*/ = Naked.Triples,
        [StrategyType.NakedQuads] /*.....*/ = Naked.Quads,

        [StrategyType.PointingDigits] /*.*/ = Pointing.Digits,

        [StrategyType.XWing] /*..........*/ = Intersection.XWing,
        [StrategyType.Swordfish] /*......*/ = Intersection.Swordfish,
    }
    .ToFrozenDictionary();
}

