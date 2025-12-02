namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_04_11 : CtcPuzzle
{
    public override string Title => "Third Times The Charm";

    public override string? Author => "udukos";

    public override Uri? Url => new("https://youtu.be/6I0-7pWCUWM");

    public override O Duration => O.s100;

    public override Cells Solution { get; } = Cells.Parse("""
        867│291│435
        932│548│167
        415│763│982
        ───┼───┼───
        371│652│894
        654│819│723
        289│374│651
        ───┼───┼───
        598│137│246
        146│925│378
        723│486│519
        """);

    public override Rules Constraints { get; } =
        Rules.XSudoku
        + Arrows.Parse("""
        ...│...│HI.
        ..D│..F│G.P
        BC.│...│.KO
        ───┼───┼───
        .A.│...│LN.
        ...│...│...
        .ag│...│...
        ───┼───┼───
        bf.│..R│.T.
        c.y│x..│S..
        .dz│..V│WX.
        """);
}
