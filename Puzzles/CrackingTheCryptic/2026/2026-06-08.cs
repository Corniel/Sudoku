namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_08 : CtcPuzzle
{
    public override string Title => "Hot or Cold";

    public override string? Author => "James Kopp";

    public override Uri? Url => new("https://youtu.be/Rn_svsVv_FA");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        534│876│192
        286│193│754
        971│524│863
        ───┼───┼───
        425│968│317
        168│357│249
        397│241│586
        ───┼───┼───
        742│685│931
        653│719│428
        819│432│675
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + pos(2, 2).Clue(1)
        + Grid.NamedGroups("""
        d..│...│...
        ...│...│...
        .AB│..a│b..
        ───┼───┼───
        .CD│..c│d..
        ...│...│...
        ..A│B..│ab.
        ───┼───┼───
        ..C│D..│cd.
        ...│...│...
        ...│...│..A
        """).SelectMany(HotCold)
       + Borders()
       ;

    private static Rules HotCold(NamedGroup group)
        => group.SelectMany(p => HotCold(group.Name, p));

    private static Rules HotCold(char name, Pos pos) => char.IsUpper(name)
        ? pos.LT([.. Others(name, pos)])
        : pos.GT([.. Others(name, pos)]);

    private static PosArray Others(char name, Pos pos) => char.ToUpper(name) switch
    {
        'A' => [pos.N()!.Value, pos.W()!.Value],
        'B' => [pos.N()!.Value, pos.E()!.Value],
        'C' => [pos.S()!.Value, pos.W()!.Value],
        'D' => [pos.S()!.Value, pos.E()!.Value],
        _ => [],
    };

    public static Rules Borders() => Dominos.Ort
        .Where(d => Box.IndexOf(d.A) != Box.IndexOf(d.B))
        .SelectMany(DutchWhisper.New);
}
