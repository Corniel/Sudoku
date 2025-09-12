namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_04 : CtcPuzzle
{
    public override string Title => "Packing Problem";
    public override string? Author => "clover!";
    public override Uri? Url => new("https://youtu.be/OMqUAduLZfI");

    public override Clues Clues { get; } = Clues.None;

    public override Cells Solution { get; } = Cells.Parse("""
        865|793|421
        743|251|869
        291|486|753
        ---+---+---
        674|932|518
        952|618|347
        318|547|692
        ---+---+---
        186|324|975
        537|169|284
        429|875|136
        """);

     public override ImmutableArray<Constraint> Constraints { get; } =
    [
        .. Rules.Standard,
        .. Cages()
    ];

    public sealed class Cage(PosSet cells, bool isSet) : Constraint
    {
        public override bool IsSet { get; } = isSet;

        public override PosSet Cells { get; } = cells;

        public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducer.Create([.. cells], isSet)];

        public sealed class Reducer(Pos appliesTo, ImmutableArray<Pos> others, ImmutableArray<Candidates> lookup) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;

            public ImmutableArray<Pos> Others { get; } = others;

            public ImmutableArray<Candidates> Candidates { get; } = lookup;

            public Candidates Restrict(Cells cells) => Candidates[0
                + cells[Others[0]] * 1
                + cells[Others[1]] * 10
                + cells[Others[2]] * 100];

            public static IEnumerable<Reducer> Create(ImmutableArray<Pos> cells, bool isSet) =>
            [
                new(cells[0], cells.Remove(cells[0]), isSet ? Sets : NonSets),
                new(cells[1], cells.Remove(cells[1]), isSet ? Sets : NonSets),
                new(cells[2], cells.Remove(cells[2]), isSet ? Sets : NonSets),
                new(cells[3], cells.Remove(cells[3]), isSet ? Sets : NonSets),
            ];
        }
    }

    public readonly struct Sum(int a, int b, int c, int total)
    {
        private readonly ImmutableArray<int> Values = [a, b, c, total];

        public readonly Candidates Candidates = [a, b, c, total];

        public bool Matches(params ReadOnlySpan<int> other)
        {
            var value = Values.AsSpan();

            var count = 0;

            while (!other.IsEmpty && !value.IsEmpty)
            {
                if (other[0] is 0)
                {
                    other = other[1..];
                    count++;
                }
                else
                {
                    if (value[0] == other[0])
                    {
                        other = other[1..];
                        value = value[1..];
                        count++;
                    }
                    else if (other[0] > value[0])
                    {
                        value = value[1..];
                    }
                    else
                    {
                        other = other[1..];
                    }
                }
            }
            return count == 3;
        }

        public override string ToString() => $"{Values[0]} + {Values[1]} + {Values[2]} = {Values[3]}";
    }

    public static readonly ImmutableArray<Candidates> Sets = [.. Lookup(
    [
        new(1, 2, 3, 6),
        new(1, 2, 4, 7),
        new(1, 2, 5, 8),
        new(1, 2, 6, 9),
        new(1, 3, 4, 8),
        new(1, 3, 5, 9),
        new(2, 3, 4, 9),
     ])];

    public static readonly ImmutableArray<Candidates> NonSets = [.. Lookup(
    [
        new(1, 2, 3, 6),
        new(1, 2, 4, 7),
        new(1, 2, 5, 8),
        new(1, 2, 6, 9),
        new(1, 3, 4, 8),
        new(1, 3, 5, 9),
        new(2, 3, 4, 9),

        new(1, 1, 2, 4),
        new(1, 1, 3, 5),
        new(1, 1, 4, 6),
        new(1, 1, 5, 7),
        new(1, 1, 6, 8),
        new(1, 1, 7, 9),

        new(1, 2, 2, 5),
        new(1, 3, 3, 7),
        new(1, 4, 4, 9),
        new(2, 3, 3, 8),
    ],
    false)];

    public static Candidates[] Lookup(ImmutableArray<Sum> sums, bool isSet = true)
    {
        var lookup = new Candidates[1000];
        
        for (var i = 0; i <= 999; i++)
        {
            var a = i % 10;
            var b = (i / 10) % 10;
            var c = i / 100;

            int[] abc = [a, b, c];
            Array.Sort(abc);

            foreach (var sum in sums.Where(s => s.Matches(abc)))
                lookup[i] |= sum.Candidates;
            
            if (isSet)
                lookup[i] ^= [a, b, c];
        }
        return lookup;
    }

    public static IEnumerable<Cage> Cages()
    {
        var str = """
            ...|.BB|BBC
            .EE|DDD|..C
            EEA|AD.|.CC
            ---+---+---
            ..A|ALM|MMM
            GFF|.LL|NN.
            GFF|.L.|.NN
            ---+---+---
            G..|III|I..
            GH.|JJJ|JKK
            HHH|...|KK.
            """;

        var cages = new PosSet[27];
        var pos = Pos.O;

        foreach(var ch in str)
        {
            if (ch is '.') pos++;
            else if(ch is >= 'A' and <= 'Z')
            {
                cages[ch - 'A'] |= pos++;
            }
        }

        return cages.Where(ps => ps.Count is 4)
            .Select((ps, i) => new Cage(ps, i is not 0));

    }
}
