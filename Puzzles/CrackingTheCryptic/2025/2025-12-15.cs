using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_15 : CtcPuzzle
{
    public override string Title => "For Daniël.";

    public override string? Author => "Jonesy";

    public override Uri? Url => new("https://youtu.be/XHtBWmDLsA0");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        629│475│318
        518│923│476
        347│618│925
        ───┼───┼───
        852│749│631
        961│832│547
        734│561│892
        ───┼───┼───
        183│254│769
        496│187│253
        275│396│184
        """);

    protected override Rules GetConstraints()
        => Rules.Killer("""
        ...│...│A..
        ...│...│A..
        ...│...│...
        ───┼───┼───
        C..│...│...
        C..│...│..B
        DD.│...│..B
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│.EE│...
        A = 7  B = 9  C = 17  D = 10  E = 15
        """)
       + WhiteDots.Parse("""
        ...│...│...
        ...│...│..A
        ...│...│..A
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│.BB│...
        ...│...│...
        """)
       + NotTens()
       + Knights()
       + Kings()
       + KillerCages.Extend;

    private static IEnumerable<Pair> NotTens()
    {
        foreach (var o in PosSet.All)
        {
            if (o.E() is { } e && o != pos(5, 0))
            {
                var hor = new LookupPair(o, e, NotTen).Couple();
                yield return hor.One;
                yield return hor.Two;
            }
            if (o.S() is { } s)
            {
                var ver = new LookupPair(o, s, NotTen).Couple();
                yield return ver.One;
                yield return ver.Two;
            }
        }
    }

    private static IEnumerable<Pair> Knights()
    {
        var center = Box.All[4].Cells;
        foreach (var pair in AntiKnight.All.Select(k => k.Cells).Where(center.Overlaps))
        {
            var couple = new LookupPair(pair.First(), pair.Last(), DiffentParity).Couple();
            yield return couple.One;
            yield return couple.Two;
        }
    }

    public static IEnumerable<Pair> Kings()
    {
        foreach (var pair in AntiKing.All.Select(k => k.Cells))
        {
            var couple = new LookupPair(pair.First(), pair.Last(), King).Couple();
            yield return couple.One;
            yield return couple.Two;
        }
    }

    public static readonly LookupDigits NotTen = LookupPair.Init(d => d switch
    {
        1 => Digits._1_to_9 ^ 9,
        2 => Digits._1_to_9 ^ 8,
        3 => Digits._1_to_9 ^ 7,
        4 => Digits._1_to_9 ^ 6,
        5 => Digits._1_to_9 ^ 5,
        6 => Digits._1_to_9 ^ 4,
        7 => Digits._1_to_9 ^ 3,
        8 => Digits._1_to_9 ^ 2,
        9 => Digits._1_to_9 ^ 1,
        _ => Digits._1_to_9,
    });

    public static readonly LookupDigits DiffentParity = LookupPair.Init(d => d switch
    {
        1 or 3 or 5 or 7 or 9 => Digits.Even,
        2 or 4 or 6 or 8 => Digits.Odd,
        _ => Digits._1_to_9,
    });

    public static readonly LookupDigits King = LookupPair.Init(d => d switch
    {
        2 => Digits._1_to_9 ^ 2,
        4 => Digits._1_to_9 ^ 4,
        6 => Digits._1_to_9 ^ 6,
        8 => Digits._1_to_9 ^ 8,
        _ => Digits._1_to_9,
    });
}
