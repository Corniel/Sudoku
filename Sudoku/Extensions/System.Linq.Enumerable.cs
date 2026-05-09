using Sudoku;

namespace System.Linq;

public static class EnumerableExtensions
{
    [Pure]
    public static bool NotAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        => !source.Any(predicate);

    [Pure]
    public static TSource? FirstOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        where TSource : struct
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var curr = enumerator.Current;
            if (predicate(curr))
                return curr;
        }
        return null;
    }

    public static IEnumerable<Pair> Couples(this IEnumerable<LookupPair> pairs) => pairs.SelectMany(p => p.Couple());

    public static IEnumerable<Domino> RoundRoubin(this IReadOnlyList<Pos> cells)
    {
        for (var i = 0; i < cells.Count - 1; i++)
        {
            for (var j = i + 1; j < cells.Count; j++)
            {
                yield return new(cells[i], cells[j]);
            }
        }
    }
}
