using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GentleWare.Sudoku.UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new MapTest();
            var methods = typeof(MapTest).GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methods.Where(m => m.ReturnType == typeof(void) && m.GetParameters().Length == 0))
            {
                try
                {
                    Console.WriteLine(method.Name);

                    method.Invoke(test, null);

                    Console.WriteLine("OK");
                }
                catch (TargetInvocationException x)
                {
                    Console.WriteLine(x.InnerException.Message);
                }
            }
            Console.ReadLine();
        }
    }
}
