namespace StrategyBased.Reductions;

public static partial class Hidden
{
    public static void Single(Nodes nodes)
    {
        foreach(var house in nodes.Houses)
            foreach (var hidden in House(nodes, house, 1))
                nodes[hidden.Peers.First()].Digit = hidden.Digit;
    }

    public static void Pairs(Nodes nodes)
    {
        foreach (var house in nodes.Houses)
            Pair(nodes, house);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Pair(Nodes nodes, House house)
    {
        Cache.Clear();
        Cache.AddRange(House(nodes, house, 2));

        foreach (var (one, two) in Cache.Take2())
        {
            if (one.Peers == two.Peers)
            {
                Digits digits = [one.Digit, two.Digit];

                foreach (var hidden in one.Peers)
                    nodes[hidden].Digits &= digits;

                foreach (var other in (one.Cells & nodes.Todo) ^ one.Peers)
                    nodes[other].Digits ^= digits;
            }
        }
    }

    public static void Triples(Nodes nodes)
    {
        foreach (var house in nodes.Houses)
            Triple(nodes, house);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Triple(Nodes nodes, House house)
    {
        Cache.Clear();
        Cache.AddRange(House(nodes, house, 2, 3));

        foreach (var (one, two, thr) in Cache.Take3())
        {
            if ((one.Peers | two.Peers | thr.Peers) is { Count: 3 } hidden)
            {
                Digits digits = [one.Digit, two.Digit, thr.Digit];

                foreach (var self in hidden)
                    nodes[self].Digits &= digits;

                foreach (var other in (one.Cells & nodes.Todo) ^ hidden)
                    nodes[other].Digits ^= digits;
            }
        }
    }

    public static void Quads(Nodes nodes)
    {
        foreach (var house in nodes.Houses)
            Quad(nodes, house);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Quad(Nodes nodes, House house)
    {
        Cache.Clear();
        Cache.AddRange(House(nodes, house, 2, 4));

        foreach (var (one, two, thr, fur) in Cache.Take4())
        {
            if ((one.Peers | two.Peers | thr.Peers | fur.Peers) is { Count: 4 } hidden)
            {
                Digits digits = [one.Digit, two.Digit, thr.Digit, fur.Digit];

                foreach (var self in hidden)
                    nodes[self].Digits &= digits;

                foreach (var other in (one.Cells & nodes.Todo) ^ hidden)
                    nodes[other].Digits ^= digits;
            }
        }
    }

    private static readonly List<HiddenCells> Cache = [];
}
