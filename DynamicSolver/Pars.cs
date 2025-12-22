using System.Collections.Immutable;

namespace DynamicSolver;

public static class Pars
{
    public static readonly ImmutableArray<double> Counts = [0, 0, 10_000, 6, 5, 4, 3, 2, 1, 0];

    public const double Inconsistency = 0.107;

    public const double Peers = 0.3;

    public const double Bits = 1;
}
