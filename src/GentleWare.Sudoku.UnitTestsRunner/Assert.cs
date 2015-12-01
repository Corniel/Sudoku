using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GentleWare.Sudoku.UnitTests
{
    public static class Assert
    {
        public static void AreEqual(object exp, object act)
        {
            if (!exp.Equals(act))
            {
                throw new AssertException(exp.ToString(), act.ToString());
            }
        }
    }
    public class AssertException : Exception
    {
        public AssertException(string exp, string act) :
            base(string.Format("Expected: <{0}>, Actual: <{1}>", exp, act)) { }
    }
}
