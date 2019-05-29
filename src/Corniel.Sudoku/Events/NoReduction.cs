namespace Corniel.Sudoku.Events
{
    public class NoReduction : IEvent
    {
        public static readonly NoReduction Instance = new NoReduction();

        public override string ToString() => "No reduction";
    }
}
