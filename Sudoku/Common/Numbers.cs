namespace Sudoku.Common;

/// <summary>Represents multi digit numbers on a Sudoku grid.</summary>
public sealed class Numbers(int index, PosArray cells, ImmutableArray<ImmutableArray<int>> allowed)
    : Group(cells[index], cells.Remove(cells[index]))
{
    private readonly int Index = index;
    private readonly PosArray Positions = cells;
    private readonly ImmutableArray<ImmutableArray<int>> Allowed = allowed;

    /// <inheritdoc />
    public override Digits Restrict(SudokuCells cells)
    {
        IEnumerable<ImmutableArray<int>> options = Allowed;

        for (var idx = 0; idx < Positions.Length; idx++)
        {
            if (idx == Index) continue;
            var d = cells[Positions[idx]].Digits;
            var i = idx;
            options = options.Where(o => d.Contains(o[i]));
        }

        var digits = Digits.None;

        foreach (var option in options)
            digits |= option[Index];

        return digits;
    }

    public static Rules New(IEnumerable<PosArray> arrays, IEnumerable<int> numbers)
    {
        var ns = Split(numbers).ToArray();

        return arrays
            .Select(c => c.Length).Distinct()
            .SelectMany(size => New(arrays, ns, size));
    }

    private static Rules New(IEnumerable<PosArray> arrays, IEnumerable<ImmutableArray<int>> numbers, int size)
    {
        var ns = numbers.Where(n => n.Length == size).ToImmutableArray();
        var cells = arrays.Where(c => c.Length == size);
        return
        [
            .. Masks(cells, ns),
            .. size is 2
                ? Two(cells, ns)
                : ThreePlus(cells, ns),
        ];
    }

    private static Rules Two(IEnumerable<PosArray> arrays, ImmutableArray<ImmutableArray<int>> numbers)
    {
        var us = new Digits[_9 + 1];
        var ts = new Digits[_9 + 1];

        foreach (var n in numbers)
        {
            var (u, t) = (n[0], n[1]);
            us[u] |= t;
            ts[t] |= u;
        }

        var ul = LookupPair.Init(us);
        var tl = LookupPair.Init(ts);

        foreach (var cells in arrays)
        {
            yield return new LookupPair(cells[1], cells[0], ul);
            yield return new LookupPair(cells[0], cells[1], tl);
        }
    }

    private static Rules ThreePlus(IEnumerable<PosArray> arrays, ImmutableArray<ImmutableArray<int>> numbers)
    {
        var size = numbers[0].Length;

        foreach (var cells in arrays)
        {
            for (var i = 0; i < size; i++)
                yield return new Numbers(i, cells, numbers);
        }
    }

    private static Rules Masks(IEnumerable<PosArray> arrays, ImmutableArray<ImmutableArray<int>> numbers)
    {
        var size = numbers[0].Length;
        var set = true;
        var masks = new Digits[size];

        foreach (var n in numbers)
        {
            for (var i = 0; i < size; i++)
                masks[i] |= n[i];

            set &= n.Distinct().Count() == size;
        }

        foreach (var cells in arrays)
        {
            if (set)
                yield return new CellSet([.. cells]);

            for (var i = 0; i < size; i++)
                if (masks[i] != _1_to_9)
                    yield return new Mask(cells[i], masks[i]);
        }
    }

    private static IEnumerable<ImmutableArray<int>> Split(IEnumerable<int> numbers)
    {
        var buffer = new List<int>();

        foreach (var number in numbers)
        {
            buffer.Clear();
            var n = number;

            while (n > 0)
            {
                var (n_, d) = Math.DivRem(n, 10);
                if (d is < 1 or > _9) throw new ArgumentOutOfRangeException(nameof(numbers), "Numbers be reprecentable by Sudoku digits.");
                buffer.Insert(0, d);
                n = n_;
            }
            yield return [.. buffer];
        }
    }
}
