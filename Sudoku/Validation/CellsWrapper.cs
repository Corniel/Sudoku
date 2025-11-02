namespace Sudoku.Validation;

public sealed class CellsWrapper(Cells cells) : SudokuCells
{
    private readonly Cells Cells = cells;

    public SudokuCell this[Pos pos] => new CellWrapper(pos, Cells[pos]);

    private readonly record struct CellWrapper(Pos Pos, int Digit) : SudokuCell
    {
        public Digits Digits => [Digit];
    }
}
