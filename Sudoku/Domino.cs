namespace Sudoku;

/// <summary>Represents a domino of two positions.</summary>
public readonly struct Domino(Pos a, Pos b) : IComparable<Domino>
{
    public readonly PosSet Set = [a, b];
    public readonly Pos A = a;
    public readonly Pos B = b;

    public bool IsHor => A.Row == B.Row;

    public bool IsVer => A.Col == B.Col;

    /// <summary>Deconstructs the domino in A and B.</summary>
    public void Deconstruct(out Pos a, out Pos b) => (a, b) = (A, B);

    /// <inheritdoc />
    public override string ToString() => $"[{A},{B}]";

    /// <inheritdoc />
    public int CompareTo(Domino other) => A.GetHashCode().CompareTo(other.A.GetHashCode()) switch
    {
        0 => B.GetHashCode().CompareTo(other.B.GetHashCode()),
        var c => c,
    };
}
