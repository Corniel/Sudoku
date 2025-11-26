using Sudoku.Houses;

namespace Specs.Generator;

public class Puzzle_characteristics
{
    [Test]
    public void boxes()
    {
        var boxes = new int[10];
        var clues = new int[81];

        foreach(var cl in Puzzles.PuzzleBank.PuzzleBankPuzzle.Diabolical.Select(p => p.Clues))
        {
            var box = Digits.None;

            foreach (var pos in cl.Select(c => c.Pos))
            {
                box |= Box.IndexOf(pos) + 1;
            }
            boxes[box.Count]++;
            clues[cl.Count]++;
        }

        Console.WriteLine("Minimal number of boxes");
        for (var i = 1; i < boxes.Length; i++)
        {
            Console.WriteLine($"{i} => {boxes[i],8:#,##0}");
        }

        Console.WriteLine("Minimal number of clues");
        for (var i = 17; i < clues.Length; i++)
        {
            Console.WriteLine($"{i} => {clues[i],8:#,##0}");
        }

        Assert.Inconclusive();
    }
}
