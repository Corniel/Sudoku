namespace DynamicSolver;

public static class Solver
{
    public static IEnumerable<Cells> SolveAll(Clues clues, Rules rules)
    {
        var links = Links.New(clues, rules);
        var tracer = new StepTracer();
        var todos = Prepare(links, tracer, PosSet.All);
        return SolveAll(links, tracer, todos);
    }

    public static Links Raw(Clues clues, Rules rules)
    {
        var links = Links.New(clues, rules);
        var tracer = new StepTracer();
        var todos = Prepare(links, tracer, links.Todos);
        Solve(links, tracer, todos);
        return links;
    }

    public static Cells Solve(Clues clues, Rules rules) => Cells.New(Raw(clues, rules));

    private static PosSet Prepare(Links links, StepTracer tracer, PosSet todos)
    {
        do
        {
            tracer.Clear();
            foreach (var pos in todos)
            {
                var link = links[pos];
                if (link.Digits.HasSingle)
                {
                    todos ^= pos;
                    var mask = ~link.Digits;

                    foreach (var peer in link.Peers & todos)
                        tracer.Track(links, peer, mask);
                }

                foreach (var res in link.Restrictions)
                    tracer.Track(links, res.AppliesTo, res.Restrict(links));
            }
        }
        while (tracer.Count is not 0);
        return todos;
    }

    private static IEnumerable<Cells> SolveAll(Links links, StepTracer tracer, PosSet todos)
    {
        if (todos.HasNone)
        {
            yield return Cells.New(links);
            yield break;
        }

        var pos = NextCell(links, todos);
        todos ^= pos;
        var link = links[pos];
        var digits = link.Digits;

        foreach (var digit in digits)
        {
            link.Digit = digit;
            var mask = ~link.Digits;
            var steps = 0;
            var valid = true;

            var peers = (link.Peers & todos).GetEnumerator();

            while (valid && peers.MoveNext())
            {
                var tracker = tracer.Track(links, peers.Current, mask);
                steps += tracker.Steps;
                valid = tracker.Valid;
            }

            if (valid && link.Restrictions is { Count: > 0 } res)
            {
                var restrictions = res.Where(x => todos.Contains(x.AppliesTo)).GetEnumerator();

                while (valid && restrictions.MoveNext())
                {
                    var tracker = tracer.Track(links, restrictions.Current.AppliesTo, restrictions.Current.Restrict(links));
                    steps += tracker.Steps;
                    valid = tracker.Valid;
                }
            }

            if (valid)
            {
                foreach (var cells in SolveAll(links, tracer, todos))
                    yield return cells;
            }

            tracer.Rollback(links, steps);
        }

        link.Digits = digits;
    }

    private static bool Solve(Links links, StepTracer tracer, PosSet todos)
    {
        if (todos.HasNone) return true;

        var pos = NextCell(links, todos);

        todos ^= pos;
        var link = links[pos];
        var digits = link.Digits;

        foreach (var digit in digits)
        {
            link.Digit = digit;
            var mask = ~link.Digits;
            var steps = 0;
            var valid = true;

            var peers = (link.Peers & todos).GetEnumerator();

            while (valid && peers.MoveNext())
            {
                var tracker = tracer.Track(links, peers.Current, mask);
                steps += tracker.Steps;
                valid = tracker.Valid;
            }

            if (valid && link.Restrictions is { Count: > 0 } res)
            {
                var restrictions = res.Where(x => todos.Contains(x.AppliesTo)).GetEnumerator();

                while (valid && restrictions.MoveNext())
                {
                    var tracker = tracer.Track(links, restrictions.Current.AppliesTo, restrictions.Current.Restrict(links));
                    steps += tracker.Steps;
                    valid = tracker.Valid;
                }
            }

            if (valid && Solve(links, tracer, todos)) return true;

            tracer.Rollback(links, steps);
        }

        link.Digits = digits;

        return false;
    }

    private static Pos NextCell(Links links, PosSet todos)
    {
        var cell = Pos.O;
        var best = int.MaxValue;
        var constraints = false;

        foreach (var todo in todos)
        {
            var link = links[todo];
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
            var link = links[todo];
            var test = link.Restrictions.Count * 3 - link.Digits.Count;
            if (test > best)
            {
                best = test;
                cell = todo;
            }
        }

        return cell;
    }
}
