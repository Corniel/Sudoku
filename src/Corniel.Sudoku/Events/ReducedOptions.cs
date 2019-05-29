using System;

namespace Corniel.Sudoku.Events
{
    public class ReducedOptions : IEvent
    {
        public ReducedOptions(Type solver)
        {
            SolverType = solver;
        }

        public Type SolverType { get; }

        public override string ToString() => $"Reduced, {SolverType.Name}";

        public static ReducedOptions Ctor<TSolver>()
        {
            return new ReducedOptions(typeof(TSolver));
        }
    }
}
