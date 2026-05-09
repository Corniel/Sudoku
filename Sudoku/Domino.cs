#pragma warning disable S1210 // Implementing "IComparable" just to sort.

namespace Sudoku;

/// <summary>Represents a domino of two positions.</summary>
public readonly record struct Domino(Pos A, Pos B)
    : IReadOnlyCollection<Pos>
    , IComparable<Domino>
{
    public readonly PosSet Set = [A, B];

    /// <summary>Is orthogonal.</summary>
    public bool IsOrt => IsHor || IsVer;

    /// <summary>Is horizonatal.</summary>
    public bool IsHor => A.Row == B.Row;

    /// <summary>Is vertical.</summary>
    public bool IsVer => A.Col == B.Col;

    /// <summary>Is diogonal.</summary>
    public bool IsDig
        => (A.Col - B.Col).Sqr()
         + (A.Row - B.Row).Sqr() is 2;

    /// <inheritdoc/>>
    int IReadOnlyCollection<Pos>.Count => 2;

    /// <inheritdoc />
    public override string ToString() => $"[{A},{B}]";

    /// <inheritdoc />
    public int CompareTo(Domino other) => A.GetHashCode().CompareTo(other.A.GetHashCode()) switch
    {
        0 => B.GetHashCode().CompareTo(other.B.GetHashCode()),
        var c => c,
    };

    public static implicit operator PosSet(Domino domino) => [domino.A, domino.B];

    /// <inheritdoc />
    IEnumerator<Pos> IEnumerable<Pos>.GetEnumerator()
    {
        yield return A;
        yield return B;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return A;
        yield return B;
    }
}
