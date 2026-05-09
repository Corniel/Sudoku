namespace Sudoku.Restrictions;

public static class DoubleArrow
{
    [DebuggerDisplay("Ends = [{AppliesTo}, {Other}], Shaft = {Others}")]
    public sealed class End(Pos appliesTo, Pos other, PosArray shaft) : Group(appliesTo, shaft)
    {
        public Pos Other { get; } = other;

        public override PosSet Cells { get; } = [other, .. shaft];

        public override Digits Restrict(SudokuCells cells)
        {
            Ints total = Ints.Zero;

            foreach (var cell in Others)
                total += cells[cell].Digits;

            total -= cells[Other].Digits;

            return total.Digits;
        }
    }

    [DebuggerDisplay("Ends = [{First}, {Second}], AppliesTo = {AppliesTo}, Others = {Others}")]
    public sealed class Shaft(Pos first, Pos second, Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public Pos First { get; } = first;

        public Pos Second { get; } = second;

        public override PosSet Cells { get; } = [first, second, .. others];

        public override Digits Restrict(SudokuCells cells)
        {
            Ints total = cells[First].Digits;
            total += cells[Second].Digits;

            foreach (var cell in Others)
                total -= cells[cell].Digits;

            return total.Digits;
        }
    }
}
