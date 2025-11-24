namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_04_06 : CtcPuzzle
{
    public override string Title => "Seesaw";

    public override string? Author => "Celery";

    public override Uri? Url => new("https://youtu.be/oPnTgXUxbhY");

    public override O Duration => O.s10;

    public override Cells Solution { get; } = Cells.Parse("""
        872│153│946
        693│784│521
        514│629│387
        ───┼───┼───
        326│415│798
        951│378│462
        748│962│153
        ───┼───┼───
        469│837│215
        135│246│879
        287│591│634
        """);

    public override Rules Constraints { get; }
        = Rules.Killer("""
            .AA│AA.│.BB
            ..A│.CC│.BB
            ...│.CD│...
            ───┼───┼───
            ...│..D│D..
            .FF│F..│.GG
            .F.│...│HHG
            ───┼───┼───
            II.│.JH│HGG
            II.│JJH│K..
            ...│...│K..
        
            A = 18  B = 13  C = 14  D = 21  F = 13  G = 17  H = 21  I = 14  J = 9  K = 14
            """)
        + Sets.Parse("""
            ...│...│...
            ...│...│...
            ...│...│..E
            ───┼───┼───
            ...│...│.EE
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """)
        + KillerCages.Extend;
}
