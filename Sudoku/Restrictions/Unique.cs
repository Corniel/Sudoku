namespace Sudoku.Restrictions;

public sealed class Unique(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others), Peers
{
    public override Digits Restrict(SudokuCells cells) => Digits._1_to_9;
}
