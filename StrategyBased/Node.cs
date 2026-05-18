using Sudoku.Restrictions;

namespace StrategyBased;

[Mutable]
public sealed class Node : SudokuCell
{
    internal Node(Pos cell, Root root)
    {
        Pos = cell;
        Root = root;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Root Root;

    public Pos Pos { get; }

    public PosSet Links { get; set; }

    public PosSet Peers { get; set; }

    public Digits Digits
    {
        get => digits;
        set => SetCanidates(value, digits);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Digits digits = _1_to_9;

    public ImmutableArray<Pos> Backgtracking { get; set; } = [];

    public List<Restriction> Restrictions { get; } = [];

    public Dictionary<Pos, List<Pair>> PairedRestrictions { get; } = [];

    private void SetCanidates(Digits next, Digits curr)
    {
        if (curr == next || digit is not 0) return;

        Root.Version++;
        digits = next;

        if (Digits.HasSingle)
        {
            digit = Digits.Min();
            Root.Todo ^= Pos;

            foreach (var pos in Peers)
            {
                var peer = Root.Nodes[pos];
                peer.Digits ^= next;
                peer.Peers ^= Pos;
            }

            Peers = default;
        }
    }

    public int Digit
    {
#pragma warning disable S4275 // Getters and setters should access the expected fields
        // this in inteneded.
        set => Digits = Digits.New(value);
#pragma warning restore S4275 // Getters and setters should access the expected fields
        get => digit;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int digit;

    /// <inheritdoc />
    public override string ToString() => Digit is 0
        ? $"[{Pos}] Digits = {Digits}, Peers = {Peers.Count}, Links = {Links.Count}"
        : $"[{Pos}] Digit = {Digit}, Links = {Links.Count}";
}
