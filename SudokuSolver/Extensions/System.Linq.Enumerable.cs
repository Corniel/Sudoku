namespace System.Linq;

public static class EnumerableExtensions
{
    public static bool NotAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        => !source.Any(predicate);
}
