namespace Puzzles.CrackingTheCryptic;

public sealed class _2022_11_22 : CtcPuzzle
{
    public override string Title => "Can't Teach An Old Dog...";

    public override string? Author => "joi_al";

    public override Uri? Url => new("https://youtu.be/cF9GPpo27TA");

    public override O Duration => O.μs100;

    public override bool IsClassic => true;

    public override Clues Clues { get; } = Clues.New("""
        54.│...│.69
        7..│1..│..5
        ...│.3.│...
        ───┼───┼───
        .2.│..4│...
        ..6│...│4..
        ...│9..│.2.
        ───┼───┼───
        ...│.5.│...
        8..│..1│..6
        47.│...│.38
        """);

    public override Cells Solution { get; } = Cells.New("""
        541│287│369
        739│146│285
        268│539│714
        ───┼───┼───
        927│814│653
        386│725│491
        154│963│827
        ───┼───┼───
        613│458│972
        892│371│546
        475│692│138
        """);
}
