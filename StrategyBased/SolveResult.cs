namespace StrategyBased;

public readonly record struct SolveResult(StrategyType Type, Nodes Nodes)
{
    public Cells Cells => Cells.New(Nodes);
}
