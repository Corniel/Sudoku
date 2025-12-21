namespace StrategyBased;

public readonly record struct HiddenCells
{
    public required int Digit { get; init; }

    /// <summary>Index of the linked house.</summary>
    public required int Index { get; init; }

    public required Indexes Indexes { get; init; }

    public required PosSet Cells { get; init; }

    public required PosSet Peers { get; init; }
}
