using Microsoft.VisualBasic;
using Puzzles.PuzzleBank;
using System.IO;

namespace Puzzles.Killer;

public sealed class KillerPuzzle(string title, Rules rules) : Puzzle
{
    public override string Title { get; } = title;

    public override Clues Clues { get; } = Clues.None;

    public override Rules Constraints { get; } = rules + KillerCages.Extend;

    public static IEnumerable<Puzzle> Load()
    {
        foreach (var name in typeof(PuzzleBankPuzzle).Assembly.GetManifestResourceNames().Where(n => n.StartsWith("Puzzles.Killer.")))
        {
            using var stream = typeof(PuzzleBankPuzzle).Assembly.GetManifestResourceStream(name)!;
            using var reader = new StreamReader(stream);

            var rules = Rules.Killer(reader.ReadToEnd());

            yield return new KillerPuzzle(name.Split('.')[^2], rules);
        }

        yield return new CrackingTheCryptic._2017_08_26();
        yield return new CrackingTheCryptic._2020_04_13();
        yield return new CrackingTheCryptic._2021_07_10();
    }
}
