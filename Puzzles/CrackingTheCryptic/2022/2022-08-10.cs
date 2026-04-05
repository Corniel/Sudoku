namespace Puzzles.CrackingTheCryptic;

public sealed class _2022_08_10 : CtcPuzzle
{
    public override string Title => "Superking";

    public override string? Author => "Aart van de Wetering";

    public override Uri? Url => new("https://youtu.be/Sd8tPBYj16E");

    public override O Duration => O.ms10;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        7..│...│..8
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        .5.│...│.8.
        ...│6.7│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        849│516│273
        132│798│465
        765│432│198
        ───┼───┼───
        498│165│732
        516│273│849
        327│984│651
        ───┼───┼───
        654│321│987
        981│657│324
        273│849│516
        """);

    public override Rules Constraints { get; }
        = Rules.XSudoku
        + Dominos.Dig.SelectMany(d => NonConsecutive.New(d));
}
