namespace Sudoku.Validation;

public sealed class CellsWrapper(Cells cells) : SudokuCells
{
    private readonly Cells Cells = cells;

    public SudokuCell this[Pos pos] => new CellWrapper(pos, Cells[pos]);

    public static CellsWrapper Parse(string s) => new(Cells.New(s));

    private readonly record struct CellWrapper(Pos Pos, int Digit) : SudokuCell
    {
        public Digits Digits => Digit is 0 ? _1_to_9 : [Digit];
    }
}
