namespace Specs.Clues_specs;

public class Parses
{
    [Test]
    public void ignores_noise()
    {
        var clues = Clues.Parse("""
        ...|1.2|...
        .6.|...|.7.
        ..8|...|9..
        ---+---+---
        4..|...|..3
        .5.|..7|...
        2..|.0.|..1
        ---+---+---
        ..9|.?.|8..
        .7.|...|.6.
        ...|3.4|...
        """);

        Cell[] hints =
        [
            new((0, 3), 1), new((0, 5), 2),
            new((1, 1), 6), new((1, 7), 7),
            new((2, 2), 8), new((2, 6), 9),

            new((3, 0), 4), new((3, 8), 3),
            new((4, 1), 5), new((4, 5), 7),
            new((5, 0), 2), new((5, 8), 1),

            new((6, 2), 9), new((6, 6), 8),
            new((7, 1), 7), new((7, 7), 6),
            new((8, 3), 3), new((8, 5), 4),
        ];

        clues.Should().BeEquivalentTo(hints);
    }
}
