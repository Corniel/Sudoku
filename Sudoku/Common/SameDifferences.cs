using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class SameDifferences
{
    public static IEnumerable<Line> Parse(string str)
        => Lines.Parse(str).SelectMany(l => Group.Select(l, (a, _) => new Line(a, l)));

    public sealed class Line : Restriction
    {
        public Line(Pos appliesTo, ImmutableArray<Pos> cells)
        {
            AppliesTo = appliesTo;
            Cells = cells;
            Links = [.. cells];
            var index = cells.IndexOf(appliesTo);
            Neigbors = [.. cells.Index().Where(x => int.Abs(x.Index - index) is 1).Select(x => x.Item)];
        }

        public Pos AppliesTo { get; }

        public ImmutableArray<Pos> Cells { get; }

        public ImmutableArray<Pos> Neigbors { get; }

        public PosSet Links { get; }

        public Digits Restrict(SudokuCells cells)
        {
            var deltas = Deltas(cells[Cells[0]].Digits, cells[Cells[1]].Digits);

            for (var i = 2; i < Cells.Length; i++)
            {
                var add = Deltas(cells[Cells[i]].Digits, cells[Cells[i - 1]].Digits);

                // No shared delta.
                if ((deltas &= add) is 0) return Digits.None;
            }

            var dt = new Ints(deltas);

            Ints allowed = cells[Neigbors[0]].Digits;
            allowed = (allowed + dt) | (allowed - dt);

            if (Neigbors.Length >= 2)
            {
                Ints second = cells[Neigbors[1]].Digits;
                second = (second + dt) | (second - dt);
                allowed &= second;
            }

            return allowed.Digits;
        }

        private static int Deltas(Digits ls, Digits rs)
        {
            var deltas = 0;

            foreach (var l in ls)
                foreach (var r in rs)
                    deltas |= 1 << int.Abs(l - r);

            return deltas;
        }
    }
}
