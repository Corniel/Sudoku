namespace Corniel.Sudoku.Events
{
    public class SolvedPuzzle : IEvent
    {
        public static readonly SolvedPuzzle Instance = new SolvedPuzzle();
        public override string ToString() => "Solved";
    }
}
