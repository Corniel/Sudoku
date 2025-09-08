namespace SudokuSolver.Restrictions;

[DebuggerDisplay("{AppliesTo} + {Involved[0]} <= {Sum}")]
public sealed record AtMost : Restriction
{
    public AtMost(Pos appliesTo, Pos involved, int sum)
    {
        AppliesTo = appliesTo;
        Involved = [involved];
        Sum = sum;
    }

    public int Sum { get; }

    /// <inheritdoc />
    public override Candidates Restrict(Cells cells) => Candidates.AtMost(Sum - cells[Involved[0]]);

    public static IEnumerable<AtMost> New(Pos one, Pos two, int sum) =>
    [
        new(one, two , sum),
        new(two, one , sum),
    ];
}
