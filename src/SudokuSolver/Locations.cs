namespace SudokuSolver;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct Locations : IReadOnlyCollection<Location>
{
    public static readonly Locations None;

    private readonly ulong Lo;
    private readonly ulong Hi;

    public Locations(ulong lo, ulong hi)
    {
        Lo = lo;
        Hi = hi;
    }

    public int Count => BitOperations.PopCount(Lo) + BitOperations.PopCount(Hi);

    public bool HasAny => Lo != 0 || Hi != 0;

    public Location First
        => Lo != 0
        ? Location.Index(BitOperations.Log2(Lo))
        : Location.Index(BitOperations.Log2(Hi) + 64);

    public bool Contains(Location location)
        => location.Idx < 64
        ? (Lo & (1UL << (location.Idx - 00))) != 0
        : (Hi & (1UL << (location.Idx - 64))) != 0;

    public Locations Dequeue(out Location first)
    {
        first = First;
        return Except(first);
    }
 
    [Pure]
    public Locations Append(Location location)
    {
        var lo = Lo;
        var hi = Hi;

        if (location.Idx < 64)
        {
            lo |= 1ul << location.Idx;
        }
        else
        {
            hi |= 1ul << (location.Idx - 64);
        }
        return new(lo, hi);
    }

    [Pure]
    public Locations Append(Locations other)
    {
        var lo = Lo | other.Lo;
        var hi = Hi | other.Hi;
        return new(lo, hi);
    }

    [Pure]
    public Locations Append(IEnumerable<Location> other)
    {
        var appended = this;
        foreach(var location in other)
        {
            appended = appended.Append(location);
        }
        return appended;
    }

    [Pure]
    public Locations Except(Location location)
    {
        var lo = Lo;
        var hi = Hi;

        if(location.Idx < 64)
        {
            lo &= ~(1ul << location.Idx);
        }
        else
        {
            var bits = 1ul << (location.Idx - 64);
            hi &= ~bits;
        }

        return new(lo, hi);
    }

    [Pure]
    public Locations Except(Locations other)
    {
        var lo = Lo & ~other.Lo;
        var hi = Hi & ~other.Hi;

        return new(lo, hi);
    }


    [Pure]
    public Locations Not()
    {
        var lo = ~Lo;
        var hi = ~Hi & (0x1FFFF);
        return new(lo, hi);
    }

    public static Locations operator |(Locations locations, Location location) => locations.Append(location);
    
    public static Locations operator |(Locations l, Locations r) => l.Append(r);

    [Pure]
    public static Locations All(IEnumerable<Location> locations)
    {
        var all = None;
        foreach (var location in locations)
        {
            all |= location;
        }
        return all;
    }

    [Pure]
    public static Locations New(Location location)=> None.Append(location);

    [Pure]
    public Iterator GetEnumerator() => new(this);

    [Pure]
    IEnumerator<Location> IEnumerable<Location>.GetEnumerator() => GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Iterator : IEnumerator<Location>
    {
        internal Iterator(Locations buffer) => Buffer = buffer;

        private Locations Buffer;

        public Location Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Buffer.HasAny)
            {
                Current = Buffer.First;
                Buffer = Buffer.Except(Current);

                return true;
            }
            else return false;
        }

        public void Reset() => throw new NotSupportedException();

        public void Dispose() { /* Nothing dispose. */ }
    }
}
