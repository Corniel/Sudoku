using Puzzles.PuzzleBank;
using System.IO;

namespace Puzzles.Killer;

public sealed class KillerPuzzle(string title, Rules rules) : Puzzle
{
    public override string Title { get; } = title;

    public override Clues Clues { get; } = Clues.None;

    public override Rules Constraints { get; } = rules;

    public static IEnumerable<KillerPuzzle> Load()
    {
        foreach (var name in typeof(PuzzleBankPuzzle).Assembly.GetManifestResourceNames().Where(n => n.StartsWith("Puzzles.Killer.")))
        {
            using var stream = typeof(PuzzleBankPuzzle).Assembly.GetManifestResourceStream(name)!;
            using var reader = new StreamReader(stream);

            var rules = Rules.Killer(reader.ReadToEnd());

            yield return new KillerPuzzle(name.Split('.')[^2], rules);
        }
    }
}
