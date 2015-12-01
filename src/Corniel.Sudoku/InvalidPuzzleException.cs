using System;
using System.Runtime.Serialization;

namespace Corniel.Sudoku
{
	/// <summary>Represents an exception thrown if the Sudoku puzzle can not be solved due to an inconsistency.</summary>
	[Serializable]
	public class InvalidPuzzleException : Exception
	{
		/// <summary>Constructor.</summary>
		public InvalidPuzzleException(): this("The puzzle contains inconstancies.") { }
		
		/// <summary>Constructor.</summary>
		public InvalidPuzzleException(string message) : base(message) { }
		
		/// <summary>Constructor.</summary>
		public InvalidPuzzleException(string message, Exception inner) : base(message, inner) { }
		
		/// <summary>Constructor.</summary>
		protected InvalidPuzzleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
