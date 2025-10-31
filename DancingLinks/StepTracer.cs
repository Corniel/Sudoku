namespace DancingLinks;

public sealed class StepTracer : Stack<Step>
{
    public Tracker Track(Links nodes, Pos cell, Digits mask)
    {
        var curr = nodes[cell].Digits;
        var next = curr & mask;

        if (next == Digits.None)
        {
            return Tracker.Invalid;
        }
        else if (next != curr)
        {
            nodes[cell].Digits = next;
            Push(new(cell, curr));
            return Tracker.One;
        }
        else return Tracker.Zero;
    }

    public void Rollback(Links nodes, int steps)
    {
        while (steps-- > 0)
        {
            var step = Pop();
            nodes[step.Cell].Digits = step.Prev;
        }
    }
}
