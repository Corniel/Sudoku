#pragma warning disable S2365
// Properties should not make collection or array copies
// Required for debugging purposes.

using System.Diagnostics.CodeAnalysis;

namespace SudokuSolver.Diagnostics;

/// <summary>Allows the debugger to display collections.</summary>
[ExcludeFromCodeCoverage(Justification = "Debugger purposes only")]
internal sealed class CollectionDebugView(IEnumerable enumeration)
{
    /// <summary>A reference to the enumeration to display.</summary>
    private readonly IEnumerable enumeration = enumeration;

    /// <summary>The array that is shown by the debugger.</summary>
    /// <remarks>
    /// Every time the enumeration is shown in the debugger, a new array is created.
    /// By doing this, it is always in sync with the current state of the enumeration.
    /// </remarks>
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public List<object> Items => [.. enumeration.Cast<object>()];
}
