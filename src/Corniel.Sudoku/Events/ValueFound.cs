using System;

namespace Corniel.Sudoku.Events
{
    public class ValueFound : IEvent
    {
        public ValueFound(int index, uint value, Type solver)
        {
            Index = index;
            Value = value;
            SolverType = solver;
        }

        public int Index { get; }
        public uint Value { get; }
        public Type SolverType { get; }

        public override string ToString() => $"[{Index / 9},{Index % 9}] = {SudokuPuzzle.Mapping[Value]}, {SolverType.Name}";

        public static ValueFound Ctor<TSolver>(int index, uint value)
        {
            return new ValueFound(index, value, typeof(TSolver));
        }
    }
}
