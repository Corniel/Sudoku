namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>Parses the grid to find lines.</summary>
    public static IEnumerable<Line> Parse(string grid)
    {
        var nodes = new List<Node>();
        var p = Pos.O;
        foreach (var ch in grid)
        {
            if (ch is '.' or '?') p++;
            else if (char.IsAsciiLetter(ch))
                nodes.Add(new(ch, p++));
        }

        nodes.Sort();

        for (var i = nodes.Count - 1; i > 0; i--)
        {
            if (nodes[i].Order - nodes[i - 1].Order > 1)
            {
                var first = nodes[0];
                var line = nodes[i..];
                yield return new([.. line.Select(n => n.Cell)], first.Ch, line[0].Ch);
                nodes.RemoveRange(i, line.Count);
            }
        }
        yield return new([.. nodes.Select(n => n.Cell)], nodes[0].Ch, nodes[^1].Ch);
    }

    private readonly record struct Node(char Ch, Pos Cell) : IComparable<Node>
    {
        public int Order => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".IndexOf(Ch);

        public int CompareTo(Node other) => Order.CompareTo(other.Order);

        public override string ToString() => $"{Ch} = {Cell}, Order = {Order}";
    }
}
