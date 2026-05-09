namespace Sudoku.Common;

public static class NonConsecutives
{
    public static Rules Orthogonally()
        => New(Dominos.Ort);

    public static Rules New(IEnumerable<Domino> dominos)
        => dominos.SelectMany(d => NonConsecutive.New(d));
}
