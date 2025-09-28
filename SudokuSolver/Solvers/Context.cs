using SudokuSolver.Diagnostics;

namespace SudokuSolver.Solvers;

[Mutable]
public sealed class Context(Rules rules)
{
    private readonly Cell[] cells = [.. rules.ToArray().Select(Cell.New)];

    public Rules Rules { get; } = rules;

    /// <summary>Gets all houses (e.a. sets with size 9).</summary>
    public ImmutableArray<Rule> Houses { get; } = [.. rules.Where(r => r.IsSet && r.Count == _9)];

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

        public Constraint Constraint
        {
            get => field ??= new()
            {
                Cell = Pos,
                Candidates = Candidates,
                Peers = [.. Peers],
                Set = Peers,
                Restrictions = [.. Restrictions],
            };
        }

        public override string ToString() => Candidates.HasSingle
            ? $"{Pos} = {Candidates.First()}"
            : Format();

        private string Format() => $"{Pos} = {Candidates}, Peers = {Peers.Count}{(Restrictions.Count > 0 ? $", Res = {Restrictions.Count}" : string.Empty)}";

        public static Cell New(Constraint constraint) => new(constraint.Cell)
        {
            Candidates = constraint.Candidates,
            Peers = constraint.Set,
            Restrictions = [.. constraint.Restrictions],
        };
    }
}
