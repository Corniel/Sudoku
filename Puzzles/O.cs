namespace Puzzles;

/// <summary>Order of magnitude.</summary>
public enum O
{
    Unknown = 0,
    
    /// <summary>10 nanoseconds.</summary>
    ns10 = 1,
    
    /// <summary>100 nanoseconds.</summary>
    ns100 = 2,
    
    /// <summary>1 microsecond.</summary>
    μs = 3,

    /// <summary>10 microseconds.</summary>
    μs10 = 4,

    /// <summary>100 microseconds.</summary>
    μs100 = 5,

    /// <summary>1 millisecond.</summary>
    ms = 6,

    /// <summary>10 millisecond.</summary>
    ms10 = 7,

    /// <summary>100 millisecond.</summary>
    ms100 = 8,

    /// <summary>1 second.</summary>
    s = 9,

    /// <summary>10 seconds.</summary>
    s10 = 10,

    /// <summary>100 seconds.</summary>
    s100 = 11,

    /// <summary>1,000 seconds (15 minutes).</summary>
    s1000 = 12,

    /// <summary>10,000 seconds (2 hours and 45 minutes).</summary>
    s10000 = 13,

    /// <summary>Infinitally (not finished).</summary>
    oo = int.MaxValue,
}
