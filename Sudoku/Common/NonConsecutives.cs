namespace Sudoku.Common;

public static class NonConsecutives
{
    public static Rules Orthogonally()
        => New(Dominos.Ort);

    public static Rules Diagonally()
        => New(Dominos.Dig);

    private static Rules New(IEnumerable<Domino> dominos)
        => dominos.SelectMany(NonConsecutive.New);
}
