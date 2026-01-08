using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class SameSum(Pos appliesTo, ImmutableArray<Pos> others, ImmutableArray<Pos> sum)
        : Group(appliesTo, others)
{
    public ImmutableArray<Pos> Sum { get; } = sum;

    public override PosSet Links { get; } = [.. others, .. sum];

    public override Digits Restrict(SudokuCells cells)
    {
        var total = Ints.Zero;

        foreach (var cell in Sum)
            total += cells[cell].Digits;

        foreach (var cell in Others)
            total -= cells[cell].Digits;

        return total.Digits;
    }

    [Pure]
    public static IEnumerable<SameSum> Create(params ImmutableArray<ImmutableArray<Pos>> cages)
    {
        for (var f = 0; f < cages.Length - 1; f++)
        {
            for (var s = f + 1; s < cages.Length; s++)
            {
                var fst = cages[f];
                var sec = cages[s];

                foreach (var res in Select(fst, (a, o) => new SameSum(a, o, sec)))
                    yield return res;

                foreach (var res in Select(sec, (a, o) => new SameSum(a, o, fst)))
                    yield return res;
            }
        }
    }
}
