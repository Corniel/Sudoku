namespace SudokuSolver.Constraints;

public static class GermanWhispers
{
    public static IEnumerable<GermanWhisper> Parse(string str)
    {
        var nodes = new List<Node>();
        var p = Pos.O;
        foreach (var ch in str)
        {
            if (ch is '.' or '?') p++;
            else if (char.IsAsciiLetterOrDigit(ch))
                nodes.Add(new(ch, p++));
        }

        nodes.Sort();

        for (var i = nodes.Count - 1; i > 0; i--)
        {
            if (nodes[i].Order - nodes[i - 1].Order > 1)
            {
                var line = nodes[i..];
                yield return new GermanWhisper([.. line.Select(n => n.Cell)]);
                nodes.RemoveRange(i, line.Count);
            }
        }
        yield return new GermanWhisper([.. nodes.Select(n => n.Cell)]);
    }

    private readonly record struct Node(char Ch, Pos Cell) : IComparable<Node>
    {
        public int Order => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".IndexOf(Ch);

        public int CompareTo(Node other) => Order.CompareTo(other.Order);

        public override string ToString() => $"{Ch} = {Cell}, Order = {Order}";
    }
}
