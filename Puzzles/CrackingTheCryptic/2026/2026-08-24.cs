namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_08_24 : CtcPuzzle
{
    public override string Title => "Double Thermos";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/BzWeEtdUb70");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        584│293│671
        367│185│924
        219│647│538
        ───┼───┼───
        195│368│247
        842│571│369
        736│924│815
        ───┼───┼───
        673│459│182
        928│716│453
        451│832│796
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.Thermometer("""
            ...│...│M..
            ...│.GL│R.Z
            ...│FKQ│.Yz
            ───┼───┼───
            .BE│JPU│Xyp
            ADI│OTW│xo.
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """)
        + Lines.Thermometer("""
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            .AD│HMP│Ua.
            BEI│NQV│b..
            ───┼───┼───
            FJ.│RWc│...
            K.S│X..│...
            ...│...│...
            """);
}
