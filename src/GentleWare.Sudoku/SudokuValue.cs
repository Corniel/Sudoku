using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GentleWare.Sudoku.Conversion;

namespace GentleWare.Sudoku
{
	/// <summary>Represents a Sudoku value.</summary>
	[DebuggerDisplay("{DebuggerDisplay}")]
	[Serializable]
	[TypeConverter(typeof(SudokuValueTypeConverter))]
	public struct SudokuValue : ISerializable, IXmlSerializable, IComparable, IComparable<SudokuValue>
	{
		/// <summary>Represents an empty/not set Sudoku value.</summary>
		public static readonly SudokuValue Unknown = new SudokuValue() { m_Value = default(Int32) };

		#region Properties

		/// <summary>The inner value of the Sudoku value.</summary>
		private Int32 m_Value;

		#endregion

		#region Methods

		/// <summary>Returns true if the Sudoku value is unknown, otherwise false.</summary>
		public bool IsUnknown() { return m_Value == default(Int32); }

		#endregion

		#region (XML) (De)serialization

		/// <summary>Initializes a new instance of Sudoku value based on the serialization info.</summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		private SudokuValue(SerializationInfo info, StreamingContext context)
		{
			if (info == null) { throw new ArgumentNullException("info"); }
			m_Value = info.GetByte("Value");
		}

		/// <summary>Adds the underlying propererty of Sudoku value to the serialization info.</summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) { throw new ArgumentNullException("info"); }
			info.AddValue("Value", m_Value);
		}

		/// <summary>Gets the xml schema to (de) xml serialize a Sudoku value.</summary>
		/// <remarks>
		/// Returns null as no schema is required.
		/// </remarks>
		XmlSchema IXmlSerializable.GetSchema() { return null; }

		/// <summary>Reads the Sudoku value from an xml writer.</summary>
		/// <remarks>
		/// Uses the string parse function of Sudoku value.
		/// </remarks>
		/// <param name="reader">An xml reader.</param>
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			var s = reader.ReadElementString();
			var val = Parse(s, CultureInfo.InvariantCulture);
			m_Value = val.m_Value;
		}

		/// <summary>Writes the Sudoku value to an xml writer.</summary>
		/// <remarks>
		/// Uses the string representation of Sudoku value.
		/// </remarks>
		/// <param name="writer">An xml writer.</param>
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(ToString());
		}

		#endregion

		#region IFormattable / ToString

		/// <summary>Returns a System.String that represents the current Sudoku value for debug purposes.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				if (IsUnknown()) { return "?"; }
				return m_Value.ToString();
			}
		}

		/// <summary>Returns a System.String that represents the current Sudoku value.</summary>
		public override string ToString()
		{
			return IsUnknown() ? "?" : m_Value.ToString();
		}

		/// <summary>Returns a System.String that represents the current Sudoku value.</summary>
		public string ToString(int dimension)
		{
			var str = IsUnknown() ? "?" : m_Value.ToString();

			var dim2 = dimension * dimension;
			var length = 1;
			while (dim2 > 10)
			{
				length++;
				dim2 /= 10;
			}

			if (length > str.Length)
			{
				str = new string(' ', length - str.Length) + str;
			}
			return str;
		}

		#endregion
		
		#region IEquatable

		/// <summary>Returns true if this instance and the other object are equal, otherwise false.</summary>
		/// <param name="obj">An object to compair with.</param>
		public override bool Equals(object obj){ return base.Equals(obj); }

		/// <summary>Returns the hash code for this Sudoku value.</summary>
		/// <returns>
		/// A 32-bit signed integer hash code.
		/// </returns>
		public override int GetHashCode() { return m_Value.GetHashCode(); }

		/// <summary>Returns true if the left and right operand are not equal, otherwise false.</summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand</param>
		public static bool operator ==(SudokuValue left, SudokuValue right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns true if the left and right operand are equal, otherwise false.</summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand</param>
		public static bool operator !=(SudokuValue left, SudokuValue right)
		{
			return !(left == right);
		}

		#endregion

		#region IComparable

		/// <summary>Compares this instance with a specified System.Object and indicates whether
		/// this instance precedes, follows, or appears in the same position in the sort
		/// order as the specified System.Object.
		/// </summary>
		/// <param name="obj">
		/// An object that evaluates to a Sudoku value.
		/// </param>
		/// <returns>
		/// A 32-bit signed integer that indicates whether this instance precedes, follows,
		/// or appears in the same position in the sort order as the value parameter.Value
		/// Condition Less than zero This instance precedes value. Zero This instance
		/// has the same position in the sort order as value. Greater than zero This
		/// instance follows value.-or- value is null.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// value is not a Sudoku value.
		/// </exception>
		public int CompareTo(object obj)
		{
			if (obj is SudokuValue)
			{
				return CompareTo((SudokuValue)obj);
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Argument must be a Sudoku value."), "obj");
		}

		/// <summary>Compares this instance with a specified Sudoku value and indicates
		/// whether this instance precedes, follows, or appears in the same position
		/// in the sort order as the specified Sudoku value.
		/// </summary>
		/// <param name="other">
		/// The Sudoku value to compare with this instance.
		/// </param>
		/// <returns>
		/// A 32-bit signed integer that indicates whether this instance precedes, follows,
		/// or appears in the same position in the sort order as the value parameter.
		/// </returns>
		public int CompareTo(SudokuValue other) { return m_Value.CompareTo(other.m_Value); }


		/// <summary>Returns true if the left operator is less then the right operator, otherwise false.</summary>
		public static bool operator <(SudokuValue l, SudokuValue r) { return l.CompareTo(r) < 0; }

		/// <summary>Returns true if the left operator is greater then the right operator, otherwise false.</summary>
		public static bool operator >(SudokuValue l, SudokuValue r) { return l.CompareTo(r) > 0; }

		/// <summary>Returns true if the left operator is less then or equal the right operator, otherwise false.</summary>
		public static bool operator <=(SudokuValue l, SudokuValue r) { return l.CompareTo(r) <= 0; }

		/// <summary>Returns true if the left operator is greater then or equal the right operator, otherwise false.</summary>
		public static bool operator >=(SudokuValue l, SudokuValue r) { return l.CompareTo(r) >= 0; }

		#endregion
	   
		#region (Explicit) casting

		/// <summary>Casts a Sudoku value to a System.Int32.</summary>
		public static explicit operator Int32(SudokuValue val) { return val.m_Value; }
		/// <summary>Casts an System.Int32 to a Sudoku value.</summary>
		public static implicit operator SudokuValue(Int32 val) { return SudokuValue.Create(val); }

		#endregion

		#region Factory methods

		/// <summary>Converts the string to a Sudoku value.</summary>
		/// <param name="s">
		/// A string containing a Sudoku value to convert.
		/// </param>
		/// <returns>
		/// A Sudoku value.
		/// </returns>
		/// <exception cref="System.FormatException">
		/// s is not in the correct format.
		/// </exception>
		public static SudokuValue Parse(string s)
		{
		   return Parse(s, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the string to a Sudoku value.</summary>
		/// <param name="s">
		/// A string containing a Sudoku value to convert.
		/// </param>
		/// <param name="formatProvider">
		/// The specified format provider.
		/// </param>
		/// <returns>
		/// A Sudoku value.
		/// </returns>
		/// <exception cref="System.FormatException">
		/// s is not in the correct format.
		/// </exception>
		public static SudokuValue Parse(string s, IFormatProvider formatProvider)
		{
			SudokuValue val;
			if (SudokuValue.TryParse(s, formatProvider, out val))
			{
				return val;
			}
			throw new FormatException("Not a valid Sudoku value");
		}

		/// <summary>Converts the string to a Sudoku value.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing a Sudoku value to convert.
		/// </param>
		/// <returns>
		/// The Sudoku value if the string was converted successfully, otherwise Value.Empty.
		/// </returns>
		public static SudokuValue TryParse(string s)
		{
			SudokuValue val;
			if (SudokuValue.TryParse(s, out val))
			{
				return val;
			}
			return SudokuValue.Unknown;
		}

		/// <summary>Converts the string to a Sudoku value.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing a Sudoku value to convert.
		/// </param>
		/// <param name="result">
		/// The result of the parsing.
		/// </param>
		/// <returns>
		/// True if the string was converted successfully, otherwise false.
		/// </returns>
		public static bool TryParse(string s, out SudokuValue result)
		{
			return TryParse(s, CultureInfo.CurrentCulture, out result);
		}

		/// <summary>Converts the string to a Sudoku value.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing a Sudoku value to convert.
		/// </param>
		/// <param name="formatProvider">
		/// The specified format provider.
		/// </param>
		/// <param name="result">
		/// The result of the parsing.
		/// </param>
		/// <returns>
		/// True if the string was converted successfully, otherwise false.
		/// </returns>
		public static bool TryParse(string s, IFormatProvider formatProvider, out SudokuValue result)
		{
			result = SudokuValue.Unknown;
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			Int32 value;

			if (Int32.TryParse(s, NumberStyles.Number, formatProvider, out value))
			{
				result = new SudokuValue() { m_Value = value };
				return true;
			}
			return false;
		}



		///  <summary >Creates a Sudoku value from a Int32.</summary >
		///  <param name="val" >
		/// A decimal describing a Sudoku value.
		///  </param >
		///  <exception cref="System.FormatException" >
		/// val is not a valid Sudoku value.
		///  </exception >
		public static SudokuValue Create(Int32? val)
		{
			return new SudokuValue() { m_Value = val.GetValueOrDefault() };
		}
		#endregion
	 }
}