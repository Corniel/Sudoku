namespace DynamicSolver;

[Mutable]
public sealed class Link(Pos pos) : SudokuCell
{
    /// <inheritdoc />
    public Pos Pos { get; } = pos;

    /// <inheritdoc />
    public Digits Digits { get; set; } = Digits._1_to_9;

    /// <inheritdoc />
    public int Digit
    {
        get => Digits.HasSingle ? Digits.First() : 0;
        set => Digits = Digits.New(value);
    }

    public PosSet Peers { get; set; }

    public List<Restriction> Restrictions { get; } = [];

    public double Bits { get; set; }

    /// <inheritdoc />
    public override string ToString() => $"{Pos} = {Digits}";
}
