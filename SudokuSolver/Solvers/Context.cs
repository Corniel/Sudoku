using SudokuSolver.Diagnostics;
using SudokuSolver.Houses;
using SudokuSolver.Restrictions;

namespace SudokuSolver.Solvers;

[Mutable]
public sealed class Context(Rules rules)
{
    private readonly Cell[] cells = [.. rules.ToArray().Select(Cell.New)];

    public List<Rule> Rules { get; } = [.. rules];

    /// <summary>Gets all houses (e.a. sets with size 9).</summary>
    public ImmutableArray<Rule> Houses { get; } = [.. rules.Where(r => r.IsHouse)];

    /// <summary>Gets all rows.</summary>
    public ImmutableArray<Row> Rows { get; } = [.. rules.OfType<Row>()];

    /// <summary>Gets all cols.</summary>
    public ImmutableArray<Col> Cols { get; } = [.. rules.OfType<Col>()];

    public Cell this[Pos cell] => cells[cell];

    /// <summary>All (resolved) cells with single value.</summary>
    public PosSet Singles { get; set; }

    /// <summary>All non-resolved cells.</summary>
    public PosSet Todos => ~Singles;

    /// <summary>Indicates that value is can not occur in any of the cells.</summary>
    public bool CanNotOccur(int value, PosSet cells) => !cells.Any(cell => this[cell].Candidates.Contains(value));

    [Mutable]
    public sealed class Cell(Pos cell)
    {
        public Pos Pos { get; } = cell;

        public Candidates Candidates { get; set; }

        public PosSet Peers { get; set; }

        public List<Restriction> Restrictions { get; init; } = [];

        public Dictionary<Pos, ImmutableArray<Pair>> PairRestrictions { get; init; } = [];

        public Constraint Constraint
        {
            get => field ??= new()
            {
                Cell = Pos,
                Candidates = Candidates,
                Peers = [.. Peers],
                Set = Peers,
                Restrictions = [.. PairRestrictions.SelectMany(r => r.Value), .. Restrictions],
            };
        }

        public override string ToString() => Candidates.HasSingle
            ? $"{Pos} = {Candidates.First()}"
            : Format();

        private string Format()
            => $"{Pos} = {Candidates}, "
            + $"Peers = {Peers.Count}"
            + ((Restrictions.Count + Peers.Count > 0)
                ? $", Res = {Restrictions.Count + Peers.Count}"
                : string.Empty);

        public static Cell New(Constraint constraint) => new(constraint.Cell)
        {
            Candidates = constraint.Candidates,
            Peers = constraint.Set,
            Restrictions = [.. constraint.Restrictions.Where(r => r is not Pair)],
            PairRestrictions = constraint.Restrictions.OfType<Pair>()
                .GroupBy(p => p.Other)
                .ToDictionary(p => p.Key, p => p.ToImmutableArray()),
        };
    }
}
