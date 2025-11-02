namespace Puzzles;

public static class OrderExntensions
{
    public static O O(this TimeSpan time) => (O)(int)Math.Log10(time.TotalNanoseconds);
}
