namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_09_29 : CtcPuzzle
{
    public override string Title => "3 In the Corner";
    public override string? Author => "James Kopp";
    public override Uri? Url => new("https://youtu.be/x6RrwaOb0Iw");

    // TODO: remove the arrow: it has not been specified, but could be deduced
    // by a hint that is not defined in a constraint
    public override Clues Clues { get; } = Clues.Parse("""
        .9.|...|...
        ..1|...|...
        ...|1..|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|1..
        ---+---+---
        ...|...|...
        ...|...|...
        3..|...|...
        """);

    public override ImmutableArray<Constraint> Constraints { get; } =
    [
        .. Rules.Standard,
        .. AtLeast3s(),
    ];

    public override Cells Solution { get; } = Cells.Parse("""
        594|738|261
        261|495|837
        837|162|594
        ---+---+---
        159|384|726
        726|951|483
        483|627|159
        ---+---+---
        948|273|615
        615|849|372
        372|516|948
        """);

    private static IEnumerable<Constraint> AtLeast3s()
    {
       foreach(var box in Box.All)
        {
            foreach (var c in box)
            {
                var w = c.W();
                if (box.Cells.Contains(w))
                {
                    yield return new AtLeast3(c, w);
                }
                var s = c.S();
                if(box.Cells.Contains(s))
                {
                    yield return new AtLeast3(c, s);
                }
            }
        }
    }

    public sealed class AtLeast3(Pos a, Pos b) : Constraint
    {
        public override bool IsSet => false;

        public override PosSet Cells { get; } = [a, b];

        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            new Reduce(a, b),
            new Reduce(b, a),
        ];

        public sealed class Reduce(Pos appliesTo, Pos other) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;
            public Pos Other {get;} =other;

            public Candidates Restrict(Cells cells)
            {
                var value = cells[Other];
                return value is 0
                    ? Candidates._1_to_9
                    : ~Candidates.Between(value - 2, value + 2);
            }
        }
    }
}
