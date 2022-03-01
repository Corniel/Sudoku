namespace SudokuSolver;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public class Regions : IReadOnlyCollection<Region>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Region[][] regions;
    private readonly Region[] all;

    public Regions(IEnumerable<Region> rs)
    {
        all = rs.ToArray();
        Locations = all.SelectMany(r => r).Distinct().Select(i => Location.Index(i)).ToArray();
        regions = new Region[Locations.Max(l => (int)l) + 1][];
        
        foreach (var index in Locations)
        {
            regions[index] = rs.Where(r => r.Contains(index)).ToArray();
        }
    }

    public int Count => all.Length;

    public IReadOnlyCollection<Location> Locations { get; }

    public IReadOnlyCollection<Region> this[int index] => regions[index];

    public IEnumerator<Region> GetEnumerator() => all.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    public static readonly Regions Default = new(All());

    public static IEnumerable<Region> All() => Rows().Concat(Columns()).Concat(Blocks());

    public static IEnumerable<Region> Rows()
        => Enumerable
        .Range(0, Cells.Size2)
        .Select(row => new Region(Enumerable.Range(row * Cells.Size2, Cells.Size2).ToArray(), RegionType.Row));

    public static IEnumerable<Region> Columns()
         => Enumerable
        .Range(0, Cells.Size2)
        .Select(col =>
        {
            var indexes = Enumerable.Range(0, Cells.Size2).Select(i => i * Cells.Size2 + col).ToArray();
            return new Region(indexes, RegionType.Column);
        });

    public static IEnumerable<Region> Blocks()
    {
        for (var block = 0; block < Cells.Size2; block++)
        {
            var dr = block / 3;
            var dc = block % 3;

            var indexes = new List<int>();
            for (var r = 0; r < Cells.Size; r++)
            {
                for (var c = 0; c < Cells.Size; c++)
                {
                    var index = r * Cells.Size2 + c;
                    index += dr * Cells.Size2 * Cells.Size;
                    index += dc * Cells.Size;
                    indexes.Add(index);
                }
            }
            yield return new Region(indexes.ToArray(), RegionType.Block);
        }
    }
}
