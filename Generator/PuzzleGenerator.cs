using StrategyBased;
using Sudoku.Houses;
using System.Diagnostics;

namespace Generator;

public sealed class PuzzleGenerator(ReduceOptions options, Random rnd)
    : IEnumerator<Generated>, IEnumerable<Generated>
{

    private readonly Rules Rules = Rules.Standard;
    private readonly ReduceOptions Options = options;
    private readonly Grids Candidates = new(rnd);
    private readonly Random Rnd = rnd;
    private readonly Pos[][] Boxes = [.. Box.All.Select(box => box.ToArray())];
    private readonly List<Overlay> Overlays = [];
    private readonly HashSet<StrategyType> StrategyTypes = [];

    public Generated Current { get; private set; } = new()
    {
        Clues = Clues.None,
        Solution = Cells.Empty,
        Rules = Rules.Standard,
        Strategies = [StrategyType.NakedSingles]
    };

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        do
        {
            var (solution, done) = NextCandidate();

            var size = done.Count;

            done = NextOverlays(solution, done);
            done = ApplyOverlays(solution, done);

            Stats.Reductions[size - done.Count]++;
            Stats.ClueCounts[done.Count]++;

            Current = NextGenerated(solution, done);
        }
        while (Current.Strategies.Length < 2);

        return true;
    }

    private Generated NextGenerated(Cells solution, PosSet done)
    {
        StrategyTypes.Clear();
        var clues = new Clues(done.Select(p => new Cell(p, solution[p])));
        var nodes = Nodes.Empty;
        var solved = new StrategyBasedSolver(nodes & Rules & clues, Options);
        
        foreach (var strategy in solved)
            StrategyTypes.Add(strategy.Type);

        foreach (var strategy in Current.Strategies)
            Stats.Strategies[(int)strategy]++;

        return new()
        {
            Clues = clues,
            Solution = solution,
            Rules = Rules,
            Strategies = [StrategyType.NakedSingles, .. StrategyTypes.Order()],
        };
    }

    private PosSet ApplyOverlays(Cells solution, PosSet done)
    {
        Clues clues;
        Nodes nodes;

        foreach (var overlay in Overlays)
        {
            done ^= overlay.Pos;
            clues = new Clues(done.Select(p => new Cell(p, solution[p])));
            nodes = Nodes.Empty;
            var testr = new StrategyBasedSolver(nodes & Rules & clues, Options);
            while (testr.MoveNext()) {/* Solve what can be solved. */ }

            Stats.Tries[overlay.Count]++;

            if (!nodes.IsSolved)
            {
                done |= overlay.Pos;
            }
            else
            {
                Stats.Fetches[overlay.Count]++;
            }
        }
        return done;
    }

    private PosSet NextOverlays(Cells solution, PosSet done)
    {
        Overlays.Clear();
        foreach (var cell in done)
        {
            var shared = Masks[cell] & done;

            var other = ~Digits.New(shared.Select(p => solution[p]));

            // The shared clues give all but one hint, so this one can be removed.
            if (other.Count is 1)
            {
                Stats.Tries[1]++;
                Stats.Fetches[1]++;
                done ^= cell;
            }
            else if (other.Count < 8)
            {
                Overlays.Add(new(cell, shared, other));
            }
        }
        Overlays.Sort();

        return done;
    }

    private Candidate NextCandidate()
    {
        Candidates.MoveNext();
        var solution = Candidates.Current;
        var clues = PosSet.Empty;
        Rnd.Shuffle(Boxes);

        var nodes = Nodes.Empty & Rules;
        var solver = new StrategyBasedSolver(nodes, TestOptions);

        // There are (almost none) not unique soltions with less then 6 boxes filled.
        for (var i = 1; i <= 6; i++)
        {
            var pos = Boxes[i][Rnd.Next(_9)];
            clues |= pos;
            nodes[pos].Digits = Digits.New(solution[pos]);
        }

        while (clues.Count < 20 || !nodes.IsSolved)
        {
            var node = nodes.OrderByDescending(Weight).First();
            clues |= node.Pos;
            var clue = new Cell(node.Pos, solution[node.Pos]);
            node.Digits = Digits.New(clue.Digit);

            while (solver.MoveNext()) {/* Solve what can be solved. */ }
        }
        return new(solution, clues);
    }

    private int Weight(Node node)
        => node.Digits.Count is 1 ? -1
        : node.Digits.Count * 128
        + node.Peers.Count * 1024
        + Rnd.Next(2048);

    void IDisposable.Dispose() { /* Nothging to dispose */ }

    void IEnumerator.Reset() => throw new NotSupportedException();

    public IEnumerator<Generated> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;

    [DebuggerDisplay("{Pos}, {Digits} [{Count}]")]
    private readonly record struct Overlay(Pos Pos, PosSet Mask, Digits Digits) : IComparable<Overlay>
    {
        public int Count => Digits.Count;

        public int CompareTo(Overlay other)=> Count.CompareTo(other.Count);
    }

    private readonly record struct Candidate(Cells Solution, PosSet Clues);

    private static readonly ReduceOptions TestOptions = new(
        StrategyType.HiddenSingles,
        StrategyType.PointingDigits,
        StrategyType.HiddenPairs,
        StrategyType.NakedPairs);

    private static readonly ImmutableArray<PosSet> Masks =
    [
        .. PosSet.All.Select(cell => 
        {
            var (r, c) = cell;
            return Row.All[r].Cells
                | Col.All[c].Cells
                | Box.All[Box.IndexOf(cell)].Cells
                ^ cell;
        })
    ];

    public readonly GeneratorStats Stats = new();
}
