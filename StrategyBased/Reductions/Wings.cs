using System.Net.NetworkInformation;

namespace StrategyBased.Reductions;

public static class Wings
{
    /// <summary>Spots XY Wings (also known as Y Wings).</summary>
    /// <remarks>
    /// See: https://sudoku.coach/en/learn/y-wing.
    /// </remarks>
    public static void XY(Nodes nodes)
    {
        Pairs.Clear();
        Pairs.AddRange(nodes.Where(c => c.Digits.Count is 2));

        foreach(var (one, two) in Pairs.Take2())
            XY(one, two, nodes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void XY(Node one, Node two, Nodes nodes)
    {
        // Find to Pivot and wing.
        if (!(one.Digits & two.Digits).HasSingle || one.Peers.Contains(two.Pos)) return;

        foreach(var pivot in Pairs)
        {
            // Skip doubles
            if (pivot.Digits == one.Digits
                || pivot.Digits == two.Digits
                || (one.Digits | two.Digits | pivot.Digits).Count != 3) continue;

            // We have an XY Wing
            if (pivot.Peers.Contains(one.Pos) && pivot.Peers.Contains(two.Pos))
            {
                PosSet shared = [pivot.Pos, one.Pos, two.Pos];
                var others = one.Peers & two.Peers;
                var digit = one.Digits & two.Digits;

                foreach (var share in (others ^ shared) & nodes.Todo)
                    nodes[share].Digits ^= digit;

                return;
            }
        }
    }

    private static readonly List<Node> Pairs = [];
}
