using SudokuSolver.Generics;

namespace SudokuSolver;

public static class Combinations
{
    public static IEnumerable<Couple<T>> Take2<T>(this IReadOnlyList<T> source)
    {
        for (var f = 0; f < source.Count - 1; f++)
            for (var s = f + 1; s < source.Count; s++)
                yield return new(source[f], source[s]);
    }

    public static IEnumerable<Triple<T>> Take3<T>(this IReadOnlyList<T> source)
    {
        for (var f = 0; f < source.Count - 2; f++)
            for (var s = f + 1; s < source.Count - 1; s++)
                for (var t = s + 1; t < source.Count; t++)
                    yield return new(source[f], source[s], source[t]);
    }

    public static IEnumerable<Quad<T>> Take4<T>(this IReadOnlyList<T> source)
    {
        for (var f = 0; f < source.Count - 3; f++)
            for (var s = f + 1; s < source.Count - 2; s++)
                for (var t = s + 1; t < source.Count - 1; t++)
                    for (var v = t + 1; v < source.Count; v++)
                        yield return new(source[f], source[s], source[t], source[v]);
    }

    public static IReadOnlyList<int> WithMax(this PosSet[] assignments, int maxCount)
    {
        return [.. assignments
            .Select((set, val) => (set, val))
            .Where(x => Max(x.set.Count, maxCount))
            .Select(x => x.val)];

        bool Max(int count, int max) => count >= 2 && count <= max;
    }
}
