namespace Sudoku.Common;

public static class Arrow
{
    [DebuggerDisplay("Circle = {AppliesTo}, Shaft = {Others}")]
    public sealed class Circle(Pos circle, PosArray shaft) : Group(circle, shaft)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var total = Ints.Zero;

            foreach (var cell in Others)
                total += cells[cell].Digits;

            return total.Digits;
        }
    }

    [DebuggerDisplay("Circle = {Circle}, AppliesTo = {AppliesTo}, Others = {Others}")]
    public sealed class Shaft(Pos circle, Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public Pos Circle { get; } = circle;

        public override PosSet Cells { get; } = [circle, .. others];

        public override Digits Restrict(SudokuCells cells)
        {
            Ints total = cells[Circle].Digits;

            foreach (var cell in Others)
                total -= cells[cell].Digits;

            return total.Digits;
        }
    }
}
