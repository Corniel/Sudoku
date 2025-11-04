using System.Diagnostics.Contracts;

namespace Sudoku;

[CollectionBuilder(typeof(Ints), nameof(New))]
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct Ints(Int128 bits) : IReadOnlyCollection<int>
{
    /// <summary>Only contains a zero.</summary>
    public static readonly Ints Zero = new(Int128.One);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int128 Bits = bits;

    /// <inheritdoc />
    public int Count => (int)Int128.PopCount(Bits);

    /// <summary>Gets all ints that are valid digits.</summary>
    public Digits Digits => new((uint)Bits);

    /// <inheritdoc cref="IEnumerable{TagList}.GetEnumerator()" />
    public Iterator GetEnumerator() => new(Bits);

    /// <inheritdoc />
    IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Ints operator -(Ints ints, Digits digits)
    {
        var bits = Int128.Zero;
        foreach (var digit in digits)
            bits |= ints.Bits >> digit;
        return new(bits);
    }

    public static Ints operator +(Ints ints, Digits digits)
    {
        var bits = Int128.Zero;
        foreach (var digit in digits)
            bits |= ints.Bits << digit;
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

    public static implicit operator Ints(Digits digits) => New(digits);

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
