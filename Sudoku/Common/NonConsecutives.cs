namespace Sudoku.Common;

public static class NonConsecutives
{
    public static IEnumerable<Restriction> Orthogonally()
        => New(Dominos.Ort);

    public static IEnumerable<Restriction> New(IEnumerable<Domino> dominos)
        => dominos.SelectMany(d => NonConsecutive.New(d));
}
