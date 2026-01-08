namespace Sudoku.Restrictions;

// TODO: Rename to Set and drop old Set.
public sealed class Unique(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others), Peers
{
    public override Digits Restrict(SudokuCells cells) => Digits._1_to_9;
}
