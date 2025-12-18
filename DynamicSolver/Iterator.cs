namespace DynamicSolver;

public sealed class Iterator : IEnumerator<Links>, IEnumerable<Links>
{
    private readonly StepTracer Tracer = new();
    private readonly Stack Stack = new();

    public Links Links { get; private set; } = null!;

    /// <inheritdoc />
    public Links Current => Links;

    /// <inheritdoc />
    object IEnumerator.Current => Links;

    /// <inheritdoc />
    public bool MoveNext()
    {
        while (Stack.Current is { } state)
        {
            var (link, todo, trac) = state;
            trac.Rollback(Links);

            while (state.NextDigit() is { } digit)
            {
                link.Digit = digit;
                var mask = ~link.Digits;
                var valid = true;

                var peers = (link.Peers & todo).GetEnumerator();

                while (valid && peers.MoveNext())
                    valid = trac.Track(Links, peers.Current, mask);

                if (valid && link.Restrictions is { Count: > 0 } res)
                {
                    var check = todo | link.Pos;
                    var restrictions = res.Where(x => check.Contains(x.AppliesTo)).GetEnumerator();

                    while (valid && restrictions.MoveNext())
                        valid = trac.Track(Links, restrictions.Current.AppliesTo, restrictions.Current.Restrict(Links));
                }

                if (valid)
                {
                    if (todo.HasNone) return true;

                    var next = NextCell(todo);
                    state = Stack.Push(Links[next], todo ^ next);
                    (link, todo, trac) = state;
                }
                else trac.Rollback(Links);
            }

            link.Digits = state.Digits;
            Stack.Pop();
        }
        return false;
    }

    private Pos NextCell(PosSet todos)
    {
        var cell = Pos.O;
        var best = int.MaxValue;
        var constraints = false;

        foreach (var todo in todos)
        {
            var link = Links[todo];
            var test = link.Digits.Count;
            if (test < best)
            {
                if (test is 1) return todo;

                best = test;
                cell = todo;
                constraints |= link.Restrictions.Count is not 0;
            }
        }
        if (best < 3 || todos.Count < 30 || !constraints) return cell;

        best = int.MinValue;

        foreach (var todo in todos)
        {
            var link = Links[todo];
            var test = (link.Restrictions.Count * 3) - link.Digits.Count;
            if (test > best)
            {
                best = test;
                cell = todo;
            }
        }

        return cell;
    }

    private PosSet Prepare(PosSet todos)
    {
        do
        {
            Tracer.Clear();
            foreach (var pos in todos)
            {
                var link = Links[pos];
                if (link.Digits.HasSingle)
                {
                    todos ^= pos;
                    var mask = ~link.Digits;

                    foreach (var peer in link.Peers & todos)
                        Tracer.Track(Links, peer, mask);
                }

                foreach (var res in link.Restrictions)
                    Tracer.Track(Links, res.AppliesTo, res.Restrict(Links));
            }
        }
        while (Tracer.Count is not 0);
        return todos;
    }

    /// <inheritdoc />
    void IDisposable.Dispose() { /* Nothing to dispose */ }

    /// <inheritdoc />
    void IEnumerator.Reset() => throw new NotSupportedException();

    /// <inheritdoc />
    public IEnumerator<Links> GetEnumerator() => this;

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => this;

    public Iterator Set(Clues clues, Rules rules)
    {
        Stack.Reset();
        Links = Links.New(clues, rules);
        var todos = Prepare(Links.Todos);
        var first = NextCell(todos);
        Stack.Push(Links[first], todos ^ first);
        return this;
    }
}
