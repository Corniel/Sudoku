namespace Corniel.Sudoku.Events
{
    public class ReducedOption : IEvent
    {
        public static readonly ReducedOption Instance = new ReducedOption();

        public override string ToString() => "Reduced option";
    }
}
