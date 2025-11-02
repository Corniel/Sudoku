namespace StrategyBased;

internal sealed class Root
{
    public readonly Node[] Nodes = new Node[_9x9];

    public int Version;

    public PosSet Todo = PosSet.All;
}
