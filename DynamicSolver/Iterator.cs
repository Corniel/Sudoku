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
                    var restrictions = res.GetEnumerator();

                    while (valid && restrictions.MoveNext())
                    {
                        var restriction = restrictions.Current;
                        valid = !check.Contains(restriction.AppliesTo)
                            || trac.Track(Links, restriction.AppliesTo, restrictions.Current.Restrict(Links));
                    }
                }

                // Constraints do not reduce indvidual cells.
                valid &= link.Constraints is not { Count: > 0 } constraints
                    || constraints.All(c => c.IsSatisfied(Links));

                if (valid)
                {
                    if (todo.HasNone) return true;

                    state = Stack.Push(NextState(todo));
                    (link, todo, trac) = state;
                }

                // When there are a lot of options we potentially want to
                // reconsider the link to test based on the insights gain while
                // executing the state.
                //
                // We store the insight that the tested digit will never be
                // valid, and is therefore removed from the digits.
                if (state.Digits.Count > 4 && Stack.Count is 1)
                {
                    trac.Rollback(Links);

                    link.Digits = state.Digits ^ digit;

                    foreach (var restriction in link.Restrictions)
                        Links[restriction.AppliesTo].Digits &= restriction.Restrict(Links);

                    state = Stack.Set(NextState(todo | link.Pos));
                    (link, todo, trac) = state;
                }
                else
                {
                    trac.Rollback(Links);
                }
            }

            link.Digits = state.Digits;
            Stack.Pop();
        }
        return false;
    }

    private Stack.StateInfo NextState(PosSet todos)
    {
        var cell = Links[Pos.O];
        var best = double.MinValue;
        var opts = 0;

        foreach (var todo in todos)
        {
            var link = Links[todo];
            var count = link.Digits.Count;

            if (count is 1)
            {
                Options[1]++;
                return new(link, link.Digits, todos ^ todo);
            }

            var test = 0
                + Pars.Counts[count]
                + link.Bits
                + ((link.Peers & todos).Count * Pars.Peers);

            if (test > best)
            {
                opts = count;
                best = test;
                cell = link;
            }
        }

        Options[opts]++;

        return new(cell, cell.Digits, todos ^ cell.Pos);
    }

    public static readonly long[] Options = new long[_9 + 2];

    private PosSet Prepare()
    {
        do
        {
            Tracer.Clear();
            foreach (var pos in Links.Todos)
            {
                var link = Links[pos];

                if (link.Digits.HasSingle)
                {
                    var mask = ~link.Digits;

                    foreach (var peer in link.Peers & Links.Todos)
                        Tracer.Track(Links, peer, mask);
                }

                foreach (var res in link.Restrictions)
                    Tracer.Track(Links, res.AppliesTo, res.Restrict(Links));
            }
        }
        while (Tracer.Count is not 0);
        return Links.Todos;
    }

    /// <inheritdoc />
    void IDisposable.Dispose() { /* Nothing to dispose */ }

    /// <inheritdoc />
    void IEnumerator.Reset() => throw new NotSupportedException();

    /// <inheritdoc />
    public IEnumerator<Links> GetEnumerator() => this;

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => this;

    public Iterator Set(Clues clues, RuleSet rules)
    {
        Options[0]++;
        Stack.Reset();
        Links = Links.New(clues, rules);
        var todos = Prepare();
        var state = todos.HasNone ? new(Links[Pos.O], Links[Pos.O].Digits, todos) : NextState(todos);
        Stack.Push(state);
        return this;
    }
}
