namespace Sudoku.Restrictions;

public sealed class SameSum(Pos appliesTo, PosArray others, PosArray sum)
        : Group(appliesTo, others)
{
    public PosArray Sum { get; } = sum;

    public override PosSet Cells { get; } = [.. others, .. sum];

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
    public static Rules New(params ImmutableArray<PosArray> cages)
    {
        for (var f = 0; f < cages.Length - 1; f++)
        {
            for (var s = f + 1; s < cages.Length; s++)
            {
                var fst = cages[f];
                var sec = cages[s];

                if (fst.Length is 1 && sec.Length is 1)
                {
                    yield return new Twin(fst[0], sec[0]);
                    yield return new Twin(sec[0], fst[0]);
                }
                else
                {
                    foreach (var res in Select(fst, (a, o) => new SameSum(a, o, sec)))
                        yield return res;

                    foreach (var res in Select(sec, (a, o) => new SameSum(a, o, fst)))
                        yield return res;
                }
            }
        }
    }
}
