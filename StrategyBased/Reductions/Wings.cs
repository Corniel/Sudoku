namespace StrategyBased.Reductions;

public static class Wings
{
    /// <summary>Spots XY-Wings (also known as Y-Wings).</summary>
    /// <remarks>
    /// See: https://sudoku.coach/en/learn/y-wing.
    /// </remarks>
    public static void XY(Nodes nodes)
    {
        Pairs.Clear();
        Pairs.AddRange(nodes.Where(c => c.Digits.Count is 2));

        foreach (var (one, two) in Pairs.Take2())
            XY(one, two, nodes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void XY(Node one, Node two, Nodes nodes)
    {
        // Find to Pivot and wing.
        if (!(one.Digits & two.Digits).HasSingle || one.Peers.Contains(two.Pos)) return;

        foreach (var pivot in Pairs)
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

    /// <summary>Spots W-Wings.</summary>
    /// <remarks>
    /// See: https://sudoku.coach/en/learn/w-wing.
    /// </remarks>
    public static void W(Nodes nodes)
    {
        Pairs.Clear();
        Pairs.AddRange(nodes.Where(c => c.Digits.Count is 2));

        foreach (var (one, two) in Pairs.Take2())
            W(one, two, nodes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void W(Node one, Node two, Nodes nodes)
    {
        // A pair that does not see each other but sees at least some cells
        if (one.Digits != two.Digits ||
            one.Peers.Contains(two.Pos) ||
            (one.Links & two.Links) is not { HasAny: true } shared) return;

        var combined = one.Peers | two.Peers;

        foreach (var digit in one.Digits)
        {
            if (nodes.Houses.Any(h => IsInconsistent(h.Cells, digit)))
            {
                foreach (var share in shared)
                    nodes[share].Digits ^= one.Digits ^ digit;
                return;
            }
        }

        // Setting the digit will lead to an inconsistency
        // if the part of the box that is not shared does not conain
        // the tested digit.
        bool IsInconsistent(PosSet house, int digit)
        {
            // Only consider houses (boxes) that can be seen by both nodes.
            return (one.Links & house).HasAny 
                && (two.Links & house).HasAny
                && (house ^ combined).NotAny(cell => nodes[cell].Digits.Contains(digit));
        }
    }

    private static readonly List<Node> Pairs = [];
}
