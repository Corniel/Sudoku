namespace Sudoku.Parsing;

public readonly record struct GridClue(Pos Pos, int Digit) : GridItem
{
    /// <inheritdoc />
    public override string ToString() => $"{Pos} = {Digit}";
}
