using Sudoku.Generics;

namespace Sudoku.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Pair(Pos appliesTo, Pos other) : Restriction
{
    /// <inheritdoc />
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public Pos Other { get; } = other;

    /// <inheritdoc />
    public PosSet Links { get; } = [other];

    /// <inheritdoc />
    public Digits Restrict(SudokuCells graph) => Restrict(graph[Other].Digits);

    /// <inheritdoc cref="Restriction.Restrict(SudokuCells)" />
    public virtual Digits Restrict(Digits other)
    {
        var digits = Digits.None;

        foreach (var digit in other)
            digits |= Restrict(digit);

        return digits;
    }

    /// <inheritdoc cref="Restriction.Restrict(SudokuCells)" />
    public virtual Digits Restrict(int digit) => Restrict([digit]);

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {Other}";

    public static DigitLookup<Digits> Init(Digits[] byValue)
    {
        var lookup = new DigitLookup<Digits>();

        foreach (var digits in Digits.All)
            foreach (var value in digits)
                lookup[digits] |= byValue[value];

        return lookup;
    }
}
