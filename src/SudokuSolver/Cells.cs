namespace SudokuSolver;

public readonly struct Cells : IReadOnlyCollection<Cell>, IEquatable<Cells>
{
    public static readonly int Size = 3;
    public static readonly int Size2 = Size * Size;
    public static readonly int Size3 = Size2 * Size;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly uint[] cells;

    public static Cells Parse(string str) => Parser.Parse(str);

    internal Cells(uint[] cs) => cells = cs;

    public int Count => cells.Length;

    public bool Solved() => Values.All(cell => cell.SingleValue());

    public Values this[int index] => cells[index];
    public Values this[int row, int column] => cells[row * Size2 + column];

    public Cells And(Location location, Values cell) => Update(location, cells[location] & (uint)cell);

    public Cells Not(Location location, Values cell) => Update(location, cells[location] & ~(uint)cell);

    private Cells Update(Location location, uint updated)
    {
        if (updated == 0)
        {
            throw new InvalidPuzzle();
        }
        // create copy when changing.
        if (updated != cells[location])
        {
            var copy = new uint[cells.Length];
            Array.Copy(cells, copy, cells.Length);
            copy[location] = updated;
            return new Cells(copy);
        }
        else return this;
    }

    public IEnumerable<Values> Values => new Iterator(cells);

    public IEnumerable<Cell> Region(Region region) 
    {
        var local = cells;
        return region.Select(loc => new Cell(loc, local[loc])); 
    }

    public IEnumerable<Cell> Delta(Cells other)
    {
        for(var i = 0; i < cells.Length; i++)
        {
            if (cells[i] != other.cells[i])
            {
                yield return new Cell(Location.Index(i), cells[i]);
            }
        }
    }

    /// <summary>Represents the Sudoku state as string.</summary>
    public override string ToString()
    {
        var sb = new StringBuilder();

        for (var index = 0; index < cells.Length; index++)
        {
            Values cell = cells[index];

            if (index % Size2 == 0) { sb.AppendLine(); }
            else if (index % Size == 0) { sb.Append('|'); }

            if (index > 0 && index % Size3 == 0)
            {
                sb.AppendLine("---+---+---");
            }

            sb.Append(cell.SingleValue() ? cell.ToString() : ".");
        }
        return sb.ToString();
    }

    public override bool Equals(object? obj) => obj is Cells other && Equals(other);

    public bool Equals(Cells other)
        => cells.Length == other.cells.Length
        && Enumerable.SequenceEqual(cells, other.cells);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 0;
            foreach (var cell in cells)
            {
                hash *= 17;
                hash += cell.GetHashCode();
            }
            return hash;
        }
    }

    public static bool operator ==(Cells l, Cells r) => l.Equals(r);
    public static bool operator !=(Cells l, Cells r) => !(l == r);

    public IEnumerator<Cell> GetEnumerator() => new CellIterator(cells);
    IEnumerator IEnumerable.GetEnumerator()=> GetEnumerator();

    private struct CellIterator: IEnumerator<Cell>
    {
        private readonly uint[] Cells;
        private int Index;

        public CellIterator(uint[] cells)
        {
            Cells = cells;
            Index = -1;
            Current = default;
        }

        public Cell Current { get; private set; }
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (++Index < Cells.Length)
            {
                Current = new(Location.Index(Index), Cells[Index]);
                return true;
            }
            else return false;
        }
        public void Dispose() { /* Do nothing */ }
        public void Reset() => throw new NotSupportedException();
    }

    private struct Iterator : IEnumerator<Values>, IEnumerable<Values>
    {
        private readonly uint[] Cells;
        private int Index;

        public Iterator(uint[] cells)
        {
            Cells = cells;
            Index = -1;
            Current = default;
        }

        public Values Current { get; private set; }
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (++Index < Cells.Length)
            {
                Current = Cells[Index];
                return true;
            }
            else return false;
        }

        public IEnumerator<Values> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose() { /* Do nothing */ }
        public void Reset() => throw new NotSupportedException();
    }
}
