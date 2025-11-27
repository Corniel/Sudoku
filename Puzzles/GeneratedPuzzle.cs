using Microsoft.VisualBasic;
using Puzzles.IO;
using System.IO;

namespace Puzzles;

public sealed class GeneratedPuzzle(Clues clues, Cells solution) : Puzzle, IEquatable<GeneratedPuzzle>
{
    public override string Title => nameof(GeneratedPuzzle);

    public override Clues Clues { get; } = clues;

    public override Cells Solution { get; } = solution;

    public override bool Equals(object? obj) => obj is BinaryPuzzle other && Equals(other);

    public bool Equals(GeneratedPuzzle? other)
        => other is { }
        && Clues.Equals(other.Clues)
        && Solution.Equals(other.Solution);

    public override int GetHashCode()
        => Clues.GetHashCode()
        ^ Solution.GetHashCode();

    public static IEnumerable<GeneratedPuzzle> Load(FileInfo file)
    {
        using var stream = file.OpenRead();
        return Load(stream);
    }

    public static IReadOnlyCollection<GeneratedPuzzle> Load(Stream stream)
    {
        var set = new HashSet<GeneratedPuzzle>();
        using var reader = new StreamReader(stream);
        while (reader.ReadLine() is { } line)
        {
            var parts = line.Split(' ');
            if (parts.Length is 2 && parts[0].Length is _9x9 && parts[1].Length is _9x9)
            {
                set.Add(new(Clues.Parse(parts[0]), Cells.Parse(parts[1])));
            }
        }
        return set;
    }
}
