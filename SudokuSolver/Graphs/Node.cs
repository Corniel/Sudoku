using SudokuSolver.Diagnostics;
using SudokuSolver.Restrictions;

namespace SudokuSolver.Graphs;

[Mutable]
public sealed class Node
{
    internal Node(Pos cell, Root root)
    {
        Cell = cell;
        Root = root;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Root Root;

    public Pos Cell { get; }

    public PosSet Links { get; set; }

    public PosSet Peers { get; set; }

    public Candidates Candidates
    {
        get => candidates;
        set => SetCanidates(value, candidates);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Candidates candidates = Candidates._1_to_9;

    public ImmutableArray<Pos> Backgtracking { get; set; } = [];

    public List<Restriction> Restrictions { get; } = [];

    public Dictionary<Pos, List<Pair>> PairedRestrictions { get; } = [];

    public IEnumerable<Node> Nodes => Peers.Select(peer => Root.Nodes[peer]);

    private void SetCanidates(Candidates next, Candidates curr)
    {
        if (curr == next || value is not 0) return;

        Root.Version++;
        candidates = next;

        if (Candidates.HasSingle)
        {
            value = Candidates.First();
            Root.Todo ^= Cell;
            var nodes = Nodes;
            Peers = default;

            foreach (var peer in nodes)
                peer.Candidates ^= next;
        }
        else
        {
            foreach (var peer in Nodes)
            {
                if ((peer.Candidates & next).HasNone)
                {
                    peer.Peers ^= this.Cell;
                    this.Peers ^= peer.Cell;
                }
            }
        }
    }

    public int Value => value;

    public void Test(int test) => value = test;

    public void Reset() => value = 0;

    public Node Freeze(PosSet todo)
    {
        Backgtracking = [.. Peers ^ todo];
        return this;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int value;

    /// <inheritdoc />
    public override string ToString() => Value is 0
        ? $"[{Cell}] Candidates = {Candidates}, Peers = {Peers.Count}, Links = {Links.Count}"
        : $"[{Cell}] Value = {Value}, Links = {Links.Count}";
}
