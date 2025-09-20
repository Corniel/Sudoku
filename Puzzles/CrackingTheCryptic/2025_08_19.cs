
namespace Puzzles.CrackingTheCryptic;

public class _2025_08_19 : CtcPuzzle
{
    public override string Title => "Pile of 15";
    public override string? Author => "Cane_Puzzles";
    public override Uri? Url => new("https://youtu.be/stUFi592gxk");

    public override Clues Clues { get; } = Clues.None;
    
    public override Rules Constraints { get; } = 
        Rules.Killer("""
        .36|.4A|BB.
        .CD|D.A|EB.
        .CC|D.A|EE.
        ---+---+---
        .zz|z.F|FF.
        3GG|H.I|JJ.
        .KG|H.I|IJ.
        ---+---+---
        .KL|H.x|MM.
        2KL|L.x|xM.
        .NN|N..|...

        A = 15  B = 15  C = 15  D = 15  E = 15
        F = 15  G = 15  H = 15  I = 15  J = 15
        K = 15  L = 15  M = 15  N = 15
        z = 12  x = 18
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        736|948|152
        145|326|897
        829|751|436
        ---+---+---
        671|432|589
        394|685|271
        582|197|364
        ---+---+---
        467|813|925
        213|579|648
        958|264|713
        """);
}
