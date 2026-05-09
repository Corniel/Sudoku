namespace Sudoku.Restrictions;

public sealed class Cage(Pos appliesTo, PosArray others, Ints sum)
    : Group(appliesTo, others)
    , Summation
{
    public Ints Sum { get; } = sum;

    public override Digits Restrict(SudokuCells cells)
    {
        var total = Sum;

        foreach (var cell in Others)
            total -= cells[cell].Digits;

        return total.Digits;
    }

    public override string ToString() => $"Cage[{Sum}] {AppliesTo} => [{string.Join(',', Others)}]";
}
