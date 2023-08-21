namespace SudokuSolver;

public readonly struct Values : IEquatable<Values>
{
    /// <summary>Represents a cell with no valid options.</summary>
    public static readonly Values Invalid = 0;
    
    /// <summary>Gets the unknown value.</summary>
    public static readonly Values Unknown = 0x1FF;

    /// <summary>Represents a cell with value 1.</summary>
    public static readonly Values Value1 = 0x001;
    /// <summary>Represents a cell with value 2.</summary>
    public static readonly Values Value2 = 0x002;
    /// <summary>Represents a cell with value 3.</summary>
    public static readonly Values Value3 = 0x004;
    /// <summary>Represents a cell with value 4.</summary>
    public static readonly Values Value4 = 0x008;
    /// <summary>Represents a cell with value 5.</summary>
    public static readonly Values Value5 = 0x010;
    /// <summary>Represents a cell with value 6.</summary>
    public static readonly Values Value6 = 0x020;
    /// <summary>Represents a cell with value 7.</summary>
    public static readonly Values Value7 = 0x040;
    /// <summary>Represents a cell with value 8.</summary>
    public static readonly Values Value8 = 0x080;
    /// <summary>Represents a cell with value 9.</summary>
    public static readonly Values Value9 = 0x100;

    public static readonly IReadOnlyCollection<Values> Singles = new[] { Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9 };
    public static readonly IReadOnlyCollection<Values> Pairs;
    public static readonly IReadOnlyCollection<Values> Triples;
    public static readonly IReadOnlyCollection<Values> Quads;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly uint values;

    public Values(uint vs) => values = vs;

    public int Count => Counts[values];

    public bool SingleValue() => Count == 1;

    public bool IsUndecided() => (values & (values - 1)) != 0;

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < 9; i++)
        {
            if ((values & (1 << i)) != 0) { sb.Append(i + 1); }
        }
        return sb.ToString();
    }

    public override bool Equals(object? obj)=> obj is Values other && Equals(other);
    public bool Equals(Values other) => values == other.values;
    public override int GetHashCode() => values.GetHashCode();

    public static bool operator ==(Values l, Values r)=> l.Equals(r);
    public static bool operator !=(Values l, Values r) => !(l == r);

    public static explicit operator uint(Values cell) => cell.values;
    public static implicit operator Values(uint cell) => new(cell);
    public static implicit operator bool(Values cell) => cell.values != default;

    public static Values operator ~(Values values) => new(~values.values);
    public static Values operator |(Values l, Values r) => new(l.values | r.values);
    public static Values operator &(Values l, Values r) => new(l.values & r.values);

    /// <summary>A lookup to get the number of options of a value.</summary>
    private static readonly byte[] Counts;

#pragma warning disable S3963 // "static" fields should be initialized inline
    static Values()
    {
        var counts = new byte[Unknown.values + 1];
        var pairs = new List<Values>();
        var triples = new List<Values>();
        var quads = new List<Values>();

        for (ushort val = 1; val < counts.Length; val++)
        {
            byte count = 0;
            for (var i = 0; i < 9; i++)
            {
                if ((val & (1 << i)) != 0)
                {
                    count++;
                }
            }
            counts[val] = count;
            if (count == 2) { pairs.Add(val); }
            else if (count == 3) { triples.Add(val); }
            else if (count == 4) { quads.Add(val); }
        }
        Pairs = pairs.ToArray();
        Triples = triples.ToArray();
        Quads = quads.ToArray();
        Counts = counts;
    }
#pragma warning restore S3963 // "static" fields should be initialized inline
}
