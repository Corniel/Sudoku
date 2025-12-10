using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class SameSum
{
    public static IEnumerable<Cage> Parse(string str)
    {
        var cages = NamedCage.Parse(str);
        for (var f = 0; f < cages.Length - 1; f++)
        {
            for (var s = f + 1; s < cages.Length; s++)
            {
                var fst = cages[f].Cells;
                var sec = cages[s].Cells;

                foreach (var res in Group.Select(fst, (a, o) => new Cage(a, o, sec)))
                    yield return res;

                foreach (var res in Group.Select(sec, (a, o) => new Cage(a, o, fst)))
                    yield return res;
            }
        }
    }

    public sealed class Cage(Pos appliesTo, ImmutableArray<Pos> others, ImmutableArray<Pos> sum)
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
    }
}
