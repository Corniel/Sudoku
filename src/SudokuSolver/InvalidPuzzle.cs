using System.Runtime.Serialization;

namespace SudokuSolver;

/// <summary>Represents an exception thrown if the Sudoku puzzle can not be solved due to an inconsistency.</summary>
[Serializable]
public class InvalidPuzzle : Exception
{
	/// <summary>Constructor.</summary>
	public InvalidPuzzle() : this("The puzzle contains inconsistencies.") { }
	
	/// <summary>Constructor.</summary>
	public InvalidPuzzle(string? message) : base(message) { }
	
	/// <summary>Constructor.</summary>
	public InvalidPuzzle(string? message, Exception? inner) : base(message, inner) { }
	
	/// <summary>Constructor.</summary>
	protected InvalidPuzzle(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
