using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;
using NUnit.Framework;
using GentleWare.Sudoku;

namespace GentleWare.Sudoku.UnitTests
{
	/// <summary>Tests the Sudoku value SVO.</summary>
	[TestFixture]
	public class SudokuValueTest
	{
		/// <summary>The test instance for most tests.</summary>
		public static readonly SudokuValue TestStruct = 8;

		#region Sudoku value IsEmpty tests

		/// <summary>Value.IsUnknown() should be true for the default of Sudoku value.</summary>
		[Test]
		public void IsUnknown_Default_IsTrue()
		{
			Assert.IsTrue(default(SudokuValue).IsUnknown());
		}
		/// <summary>Value.IsUnknown() should be true for Value.Unknown.</summary>
		[Test]
		public void IsUnknown_Unknown_IsTrue()
		{
			Assert.IsTrue(SudokuValue.Unknown.IsUnknown());
		}
		/// <summary>Value.IsUnknown() should be false for the TestStruct.</summary>
		[Test]
		public void IsUnknown_TestStruct_IsFalse()
		{
			Assert.IsFalse(TestStruct.IsUnknown());
		}

		#endregion

		#region IFormattable / ToString tests

		[Test]
		public void ToString_None_StringEmpty()
		{
			var act = TestStruct.ToString();
			var exp = "8";
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void ToString_4_StringEmpty()
		{
			var act = TestStruct.ToString(4);
			var exp = " 8";
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void ToString_Unknown_QuestionMark()
		{
			var act = SudokuValue.Unknown.ToString();
			var exp = "?";
			Assert.AreEqual(exp, act);
		}


		#endregion

		#region IEquatable tests

		/// <summary>GetHash should not fail for Value.Empty.</summary>
		[Test]
		public void GetHash_Unknown_0()
		{
			Assert.AreEqual(0, SudokuValue.Unknown.GetHashCode());
		}

		/// <summary>GetHash should not fail for the test struct.</summary>
		[Test]
		public void GetHash_TestStruct_8()
		{
			Assert.AreEqual(8, SudokuValueTest.TestStruct.GetHashCode());
		}
				
		#endregion
	}
}
