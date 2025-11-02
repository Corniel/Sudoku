namespace Puzzles.CrackingTheCryptic;

public sealed class _2022_05_03 : CtcPuzzle
{
    public override string Title => "The Dutch Miracle";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/wUnnXwLTbnA");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        1.2│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        874│192│356
        396│578│124
        521│346│798
        ───┼───┼───
        768│923│541
        245│781│963
        913│465│287
        ───┼───┼───
        689│217│435
        457│639│812
        132│854│679
        """);

    public override Rules Constraints { get; }
        = Rules.Standard
        + NamedCage.Parse("""
            .BCDEFGHI
            BCDEFGHIJ
            CDEFGHIJK
            DEFGHIJKL
            EFGHIJKLM
            FGHIJKLMN
            GHIJKLMNO
            HIJKLMNOP
            IJKLMNOP.
            """).Select(c => new DutchWhisper([.. c.Cells], true));
}
