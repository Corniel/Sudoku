namespace DancingLinks;

public static class LinksSolver
{
    public static IEnumerable<Cells> SolveAll(Clues clues, Rules rules)
    {
        var links = Links.New(clues, rules);
        return SolveAll(links, new(), PosSet.All);
    }

    public static Cells Solve(Clues clues, Rules rules)
    {
        var links = Links.New(clues, rules);
        Solve(links, new(), PosSet.All);
        return links.Cells;
    }

    private static IEnumerable<Cells> SolveAll(Links links, StepTracer tracer, PosSet todos)
    {
        if (todos.HasNone)
        {
            yield return links.Cells;
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

        foreach (var todo in todos)
        {
            var test = links[todo].Digits.Count;
            if (test < best)
            {
                if (test is 1) return todo;

                best = test;
                cell = todo;
            }
        }

        return cell;
    }
}
