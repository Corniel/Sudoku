namespace Sudoku;

[CollectionBuilder(typeof(Ints), nameof(New))]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Ints(Int128 bits) : IReadOnlyCollection<int>
{
    /// <summary>Only contains a zero.</summary>
    public static readonly Ints Zero = new(Int128.One);

    /// <summary>Only contains 1-9.</summary>
    public static readonly Ints _1_9 = new(Digits._1_to_9.Bits);

    /// <summary>Only contains 45.</summary>
    public static readonly Ints _45 = [45];

    /// <summary>Numbers between 1 and 128.</summary>
    public static readonly Ints All = new(Int128.MaxValue);

    /// <summary>Square numbers: [1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121].</summary>
    public static readonly Ints SquareNumbers = [1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121];

    /// <summary>Multiples of 10: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120].</summary>
    public static readonly Ints Mutlple10 = [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120];

    public static readonly ImmutableArray<Ints> Triangles =
    [
        [],
        [.. range(1, 9)],
        [.. range(1 + 2, /*...........................................*/ 8 + 9)],
        [.. range(1 + 2 + 3, /*...................................*/ 7 + 8 + 9)],
        [.. range(1 + 2 + 3 + 4,  /*..........................*/ 6 + 7 + 8 + 9)],
        [.. range(1 + 2 + 3 + 4 + 5, /*...................*/ 5 + 6 + 7 + 8 + 9)],
        [.. range(1 + 2 + 3 + 4 + 5 + 6, /*...........*/ 4 + 5 + 6 + 7 + 8 + 9)],
        [.. range(1 + 2 + 3 + 4 + 5 + 6 + 7, /*...*/ 3 + 4 + 5 + 6 + 7 + 8 + 9)],
        [.. range(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8, 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9)],
        [45],
    ];

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int128 Bits = bits;

    /// <inheritdoc />
    public int Count => (int)Int128.PopCount(Bits);

    public bool HasNone => Bits == 0;

    public bool HasSingle => (Bits & (Bits - 1)) == 0 && Bits != 0;

    public bool HasAny => Bits != 0;

    public bool HasMultiple => (Bits & (Bits - 1)) != 0;

    /// <summary>Gets all ints that are valid digits.</summary>
    public Digits Digits => new((uint)Bits);

    /// <summary>Returns true if the value is in the collection.</summary>
    public bool Contains(int value) => (Bits & (Int128.One << value)) != 0;

    /// <inheritdoc />
    public override string ToString() => string.Join(", ", this);

    /// <inheritdoc cref="IEnumerable{TagList}.GetEnumerator()" />
    public Iterator GetEnumerator() => new(Bits);

    /// <inheritdoc />
    IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Ints operator &(Ints left, Ints right) => new(left.Bits & right.Bits);

    public static Ints operator |(Ints left, Ints right) => new(left.Bits | right.Bits);

    public static Ints operator -(Ints left, Ints right)
    {
        var bits = Int128.Zero;
        foreach (var digit in right)
            bits |= left.Bits >> digit;
        return new(bits);
    }

    public static Ints operator -(Ints left, Digits right)
    {
        var bits = Int128.Zero;
        foreach (var digit in right)
            bits |= left.Bits >> digit;
        return new(bits);
    }

    public static Ints operator +(Ints left, int right) => new(left.Bits << right);

    public static Ints operator +(Ints left, Digits right)
    {
        var bits = Int128.Zero;
        foreach (var digit in right)
            bits |= left.Bits << digit;
        return new(bits);
    }

    public static Ints operator +(Ints left, Ints right)
    {
        var bits = Int128.Zero;
        foreach (var digit in right)
            bits |= left.Bits << digit;
        return new(bits);
    }

    public static Ints operator /(Ints ints, Digits digits)
    {
        var bits = Int128.Zero;

        foreach (var @int in ints.GetEnumerator())
        {
            foreach (var digit in digits)
            {
                if (Math.DivRem(@int, digit) is { Remainder: 0 } factor)
                    bits |= Int128.One << factor.Quotient;
            }
        }
        return new(bits);
    }

    public static Ints operator %(Ints ints, int modulo)
    {
        var bits = Int128.Zero;
        foreach (var i in ints)
            bits |= Int128.One << (i % modulo);
        return new(bits);
    }

    public static implicit operator Ints(Digits digits) => New(digits);

    public static implicit operator Ints(Range range)
    {
        var ints = Int128.Zero;
        for (var i = range.Start.Value; i <= range.End.Value; i++)
            ints |= Int128.One << i;
        return new(ints);
    }

    public static Ints New(Digits digits) => new(digits.Bits);

    [Pure]
    [OverloadResolutionPriority(1)]
    public static Ints New(params ReadOnlySpan<int> ints)
    {
        Int128 bits = 0;

        foreach (var @int in ints)
            bits |= Int128.One << @int;

        return new(bits);
    }

    public struct Iterator(Int128 mask) : IEnumerator<int>, IEnumerable<int>
    {
        private Int128 Mask = mask;

        public int Current { get; private set; } = -1;

        readonly object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Mask == 0) return false;

            var trailing = (int)Int128.TrailingZeroCount(Mask) + 1;
            Mask >>= trailing;

            Current += trailing;
            return true;
        }

        public readonly IEnumerator<int> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;

        public void Reset() => throw new NotSupportedException();

        public readonly void Dispose() { /* Nothing to dispose. */ }
    }
}
