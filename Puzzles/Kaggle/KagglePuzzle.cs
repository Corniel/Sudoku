using System.IO;

namespace Puzzles.Kaggle;

/// <summary>Kaggle test set puzzle.</summary>
/// <remarks>
/// The 9M, 1.4Gb data set is not included in the repo.
/// </remarks>
public sealed class KagglePuzzle(Clues clues, Cells solution) : Puzzle
{
    public override string Title => "Kaggle puzzle";

    public override string? Author => "Rohan Rao";

    public override Uri? Url => new("https://www.kaggle.com/datasets/rohanrao/sudoku/");

    public override Clues Clues { get; } = clues;

    public override Cells Solution { get; } = solution;

    public static IEnumerable<KagglePuzzle> Load(StreamReader reader)
    {
        // Skip header.
        reader.ReadLine();
        while (reader.ReadLine() is { } line)
        {
            if (line.Split(',', StringSplitOptions.TrimEntries) is { Length: 2 } parts)
                yield return new(Clues.New(parts[0]), Cells.New(parts[1]));
        }
    }
}
