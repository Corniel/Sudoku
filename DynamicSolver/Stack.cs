using Sudoku.Generics;
using System.Collections.Immutable;

namespace DynamicSolver;

[Mutable]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public sealed class Stack : IReadOnlyCollection<Stack.State>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly State[] States = [null!, .. range(_9x9).Select(_ => new State())];

    public int Count { get; private set; }

    public State Current => States[Count];

    public bool AllLast
    {
        get
        {
            for (var c = 1; c < Count; c++)
                if (!States[c].IsLast) return false;
            return true;
        }
    }

    public State Set(StateInfo state) => States[Count].Set(state);

    public State Push(StateInfo state) => States[++Count].Set(state);

    public State Pop() => States[Count--];

    public void Reset() => Count = 0;

    public IEnumerator<State> GetEnumerator() => States[1..(Count + 1)].Reverse().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [Mutable]
    public sealed class State
    {
        public StepTracer Tracer { get; } = new(128);

        public Link Link { get; private set; } = null!;

        public PosSet Todo { get; private set; }

        public Digits Digits { get; private set; }

        private ImmutableArray<int> Steps { get; set; }

        private int Step = 0;

        public bool IsLast => Step is 0;

        public void Deconstruct(out Link link, out PosSet todo, out StepTracer tracer) => (link, todo, tracer) = (Link, Todo, Tracer);

        public int? NextDigit()
        {
            if (Step > 0)
                return Steps[--Step];
            else return null;
        }

        public override string ToString() => $"{Link.Pos}, [{string.Join(',', Digits)}] ({Step}), Todo = {Todo.Count}";

        public State Set(StateInfo state)
        {
            Link = state.Link;
            Todo = state.Todo;
            Digits = state.Digits;
            Steps = Lookup[Digits];
            Step = Steps.Length;
            Tracer.Clear();
            return this;
        }
    }

    public readonly ref struct StateInfo(Link link, Digits digits, PosSet todo)
    {
        public readonly Link Link = link;
        public readonly Digits Digits = digits;
        public readonly PosSet Todo = todo;
    }

    private static readonly DigitLookup<ImmutableArray<int>> Lookup = Init();

    private static DigitLookup<ImmutableArray<int>> Init()
    {
        var lookup = new DigitLookup<ImmutableArray<int>>();

        foreach (var digits in Digits.All)
            lookup[digits] = [.. digits.Reverse()];

        return lookup;
    }
}
