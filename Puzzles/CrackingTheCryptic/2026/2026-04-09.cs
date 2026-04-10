namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_09 : CtcPuzzle
{
    public override string Title => "Mayan Ruins";

    public override string? Author => "Riffclown";

    public override Uri? Url => new("https://youtu.be/yuIocr3HDtk");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        389│452│716
        645│173│829
        712│869│453
        ───┼───┼───
        256│317│948
        974│628│531
        138│594│267
        ───┼───┼───
        421│786│395
        863│945│172
        597│231│684
        """);

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ..6│.1.│9..
        ...│...│...
        1..│...│..7
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    public override Rules Constraints { get; }
        = Rules.Standard
         + RenbanLines.Parse("""
        ...│...│...
        ...│.A.│...
        ...│A.A│...
        ───┼───┼───
        ...│...│...
        BB.│...│.CC
        ..B│...│C..
        ───┼───┼───
        ...│...│...
        ...│.D.│...
        ...│DDD│...
        """)
        + Lines.Parse("""
        A..│.E.│..I
        .B.│D.F│.H.
        K.C│.O.│G.S
        ───┼───┼───
        .L.│N.P│.R.
        ..M│.e.│Q..
        .b.│d.f│.h.
        ───┼───┼───
        a.c│.o.│g.i
        .l.│n.p│.r.
        k.m│...│q.s
        """).SelectMany(RunOnRenban);

    private static IEnumerable<RenbanLine> RunOnRenban(ImmutableArray<Pos> line)
        => range(0, 5).Select(i => new RenbanLine(line[i..(i + 5)]));
}
