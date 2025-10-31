namespace DancingLinks;

public readonly struct Tracker(int steps, bool valid)
{
    public static readonly Tracker Invalid;
    public static readonly Tracker Zero = new(0, true);
    public static readonly Tracker One = new(1, true);

    public readonly int Steps = steps;
    public readonly bool Valid = valid;

    public override string ToString() => Valid ? Steps.ToString() : "{invalid}";
}
