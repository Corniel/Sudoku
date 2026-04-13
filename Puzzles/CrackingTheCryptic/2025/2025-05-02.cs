namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_05_02 : CtcPuzzle
{
    public override string Title => "Arrows v.s. Thermo";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/kjKeq8fTyS8");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        ...|...|...
        .1.|...|...
        ...|2..|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|..4
        ...|...|...
        """);

    protected override Rules GetConstraints() =>
        Rules.Standard
        + Arrows.Parse("""
            .A.|F.K|.P.
            .B.|G.L|.Q.
            .C.|H.M|.R.
            ---+---+---
            .D.|I.N|.S.
            ...|...|...
            ..d|.i.|n..
            ---+---+---
            ..c|.h.|m..
            ..b|.g.|l..
            ..a|.f.|k..
            """)
        + Thermometers.Parse("""
            A..|.F.|..K
            B..|.G.|..L
            C..|.H.|..M
            ---+---+---
            D..|.I.|..N
            ...|...|...
            .d.|...|.n.
            ---+---+---
            .c.|...|.m.
            .b.|...|.l.
            .a.|...|.k.
            """);

    public override Cells Solution { get; } = Cells.Parse("""
        482|956|371
        619|473|825
        735|281|946
        ---+---+---
        846|392|517
        127|845|693
        593|617|482
        ---+---+---
        374|529|168
        961|738|254
        258|164|739
        """);
}
