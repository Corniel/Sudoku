using SudokuSolver.Parsing;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_13 : CtcPuzzle
{
    public override string Title => "Royalty";
    public override string? Author => "zetamath";
    public override Uri? Url => new("https://youtu.be/uyTSKJ1DB6c");

    public override Clues Clues { get; } = Clues.Parse("""
        ...|...|.8.
        ..6|...|...
        1..|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        """);

    public override ImmutableArray<Constraint> Constraints { get; } =
    [
        .. Rules.Standard,
        .. NamedCage.Parse("""
            AAA|BCC|C..
            A..|B.D|C..
            .EE|DDD|FFF
            ---+---+---
            .EG|GG.|..F
            JEG|..H|IIF
            J.G|HHH|I..
            ---+---+---
            JJ.|...|I.K
            .LM|MMN|.KK
            LLM|NNN|.K.
            """)
        .Select(cage => new Renban(cage.Cells))
    ];

    public override Cells Solution { get; } = Cells.Parse("""
        324|956|781
        596|871|432
        178|324|956
        ---+---+---
        861|543|297
        753|219|648
        942|768|513
        ---+---+---
        685|197|324
        439|682|175
        217|435|869
        """);

    public sealed class Renban(ImmutableArray<Pos> cells) : Constraint
    {
        public override bool IsSet => true;

        public override PosSet Cells { get; } = [.. cells];

        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            .. cells.Select(c => new Reduce(c, cells.Remove(c))),
        ];

        public sealed class Reduce(Pos appliesTo, ImmutableArray<Pos> others) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;

            public ImmutableArray<Pos> Others { get; } = others;

            public Candidates Restrict(Cells cells)
            {
                var min = int.MaxValue;
                var max = int.MinValue;

                foreach(var val in Others.Select(o => cells[o]).Where(v => v is not 0))
                {
                    min = Math.Min(min, val);
                    max = Math.Max(max, val);
                }

                if (min is int.MaxValue) return Candidates._1_to_9;

                var dt = Others.Length - (max - min);
                return Candidates.Between(min - dt, max + dt);
            }
        }
    }
}
