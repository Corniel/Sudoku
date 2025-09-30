using SudokuSolver.Diagnostics;
using SudokuSolver.Restrictions;

namespace SudokuSolver.Solvers;

[Mutable]
public sealed class Context(Rules rules)
{
    private readonly Cell[] cells = [.. rules.ToArray().Select(Cell.New)];

    public List<Rule> Rules { get; } = [.. rules];

    /// <summary>Gets all houses (e.a. sets with size 9).</summary>
    public ImmutableArray<Rule> Houses { get; } = [.. rules.Where(r => r.IsHouse)];

    public Cell this[Pos cell] => cells[cell];

    public PosSet Singles { get; set; }

    public PosSet Todos => ~Singles;

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
