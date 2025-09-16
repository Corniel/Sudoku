namespace SudokuSolver.Constraints;

public static class GermanWhispers
{
    public static IEnumerable<GermanWhisper> Parse(string str)
    {
        var lookup = new Dictionary<int, Pos>();
        var p = Pos.O;
        foreach (var ch in str)
        {
            if (ch is '.' or '?') p++;
            else if (char.IsAsciiLetterOrDigit(ch))
                lookup.Add(Order.IndexOf(ch), p++);
        }

        KeyValuePair<int, Pos>[] sorted = [.. lookup.OrderBy(kvp => kvp.Key)];

        for (var i = 1; i < sorted.Length; i++)
        {
            var prev = sorted[i - 1];
            var curr = sorted[i];

            if (curr.Key - prev.Key is 1)
            {
                yield return new GermanWhisper(prev.Value, curr.Value);
            }
        }
    }

    private const string Order = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
}
