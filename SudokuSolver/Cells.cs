namespace SudokuSolver;

public readonly struct Cells : IEquatable<Cells>
{
    public static Cells Empty => new(new int[_9x9]);

    private readonly int[] Values;

    private Cells(int[] vals) => Values = vals;

    public int this[int row, int col]
    {
        get => this[(row, col)];
        set => this[(row, col)] = value;
    }

    public int this[Pos pos]
    {
        get => Values[pos];
        set => Values[pos] = value;
    }

    /// <summary>Indicates that all values have been resolved.</summary>
    public bool IsSolved => Values.All(v => v is not 0);

    /// <summary>Represents the Sudoku state as string.</summary>
    public override string ToString()
    {
        var sb = new StringBuilder(131);

        for (var row = 0; row < _9; row++)
        {
            for (var col = 0; col < _9; col++)
            {
                sb.Append(this[row, col] is 0 ? '.' : (char)(this[row, col] + '0'));

                if (col is 2 or 5) sb.Append('|');
            }
            if (row is not 8) sb.Append('\n');
            if (row is 2 or 5) sb.Append("---+---+---\n");
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Cells other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Cells other)
    {
        for (var i = 0; i < _9x9; i++)
        {
            if (Values[i] != other.Values[i])
            {
                return false;
            }
        }
        return true;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = Values[0];
        for (var i = 1; i < _9x9; i++)
        {
            hash = (hash * 13) ^ Values[i];
        }
        return hash;
    }

    public static bool operator ==(Cells l, Cells r) => l.Equals(r);

    public static bool operator !=(Cells l, Cells r) => !(l == r);

    public static Cells Parse(string str)
    {
        var vals = new int[_9x9];

        foreach (var cell in Clues.Parse(str))
        {
            vals[cell.Pos] = cell.Value;
        }
        return new(vals);
    }
}
