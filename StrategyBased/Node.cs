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
    private Digits digits = Digits._1_to_9;

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
            digit = Digits.First();
            Root.Todo ^= Pos;

            foreach (var peer in Peers)
                Root.Nodes[peer].Digits ^= next;

            Peers = default;
        }
    }

    public int Digit => digit;

    public void Test(int test) => digit = test;

    public void Reset() => digit = 0;

    public Node Freeze(PosSet todo)
    {
        Backgtracking = [.. Peers ^ todo];
        return this;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int digit;

    /// <inheritdoc />
    public override string ToString() => Digit is 0
        ? $"[{Pos}] Digits = {Digits}, Peers = {Peers.Count}, Links = {Links.Count}"
        : $"[{Pos}] Digit = {Digit}, Links = {Links.Count}";
}
