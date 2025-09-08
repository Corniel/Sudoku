using System.Globalization;
using System.IO;

namespace Puzzles.PuzzleBank;

/// <summary>Sudoku exchange puzzle bank.</summary>
/// <remarks>
/// See: https://github.com/grantm/sudoku-exchange-puzzle-bank.
/// </remarks>
public sealed class PuzzleBankPuzzle(string title, Clues clues) : Puzzle
{
    public override string Title { get; } = title;

    public override Clues Clues { get; } = clues;

    public decimal Level { get; init; }

    public static ImmutableArray<PuzzleBankPuzzle> Easy => [.. Load(nameof(Easy))];

    public static ImmutableArray<PuzzleBankPuzzle> Medium => [.. Load(nameof(Medium))];

    public static ImmutableArray<PuzzleBankPuzzle> Hard => [..Load(nameof(Hard))];

    public static ImmutableArray<PuzzleBankPuzzle> Diabolical => [..Load(nameof(Diabolical))];

    public override string ToString() => $"{Title} ({Level:0.0})";

    private static IEnumerable<PuzzleBankPuzzle> Load(string file)
    {
        using var stream = typeof(PuzzleBankPuzzle).Assembly.GetManifestResourceStream($"Puzzles.PuzzleBank.{file}.txt")!;
        using var reader = new StreamReader(stream);

        while (reader.ReadLine() is { } line)
        {
            if (line.Split(' ', StringSplitOptions.RemoveEmptyEntries) is { Length: 3 } parts)
            {
                yield return new PuzzleBankPuzzle(parts[0], Clues.Parse(parts[1]))
                {
                    Level = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                };
            }
        }
    }
}
