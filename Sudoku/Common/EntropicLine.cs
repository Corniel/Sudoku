using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class EntropicLine(ImmutableArray<Pos> cells) : Rule(cells)
{
    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Init(cells)];

    private static IEnumerable<Restriction> Init(ImmutableArray<Pos> cells)
    {
        for (var f = 0; f < cells.Length - 1; f++)
        {
            for (var s = f + 1; s < cells.Length; s++)
            {
                if ((s - f) % 3 == 0)
                {
                    yield return new Same(cells[f], cells[s]);
                    yield return new Same(cells[s], cells[f]);
                }
                else
                {
                    yield return new Neighbors(cells[f], cells[s]);
                    yield return new Neighbors(cells[s], cells[f]);
                }
            }
        }
    }

    public sealed class Same(Pos appliesTo, Pos other) : Paired(appliesTo, other)
    {
        protected override Digits Restrict(Groups range) => range switch
        {
            Groups._123 => Digits._123,
            Groups._456 => Digits._456,
            Groups._789 => Digits._789,
            Groups._123 | Groups._456 => ~Digits._789,
            Groups._123 | Groups._789 => ~Digits._456,
            Groups._456 | Groups._789 => ~Digits._123,
            _ => Digits._1_to_9,
        };
    }

    public sealed class Neighbors(Pos appliesTo, Pos other) : Paired(appliesTo, other)
    {
        protected override Digits Restrict(Groups range) => range switch
        {
            Groups._123 => ~Digits._123,
            Groups._456 => ~Digits._456,
            Groups._789 => ~Digits._789,
            Groups._123 | Groups._456 => Digits._789,
            Groups._123 | Groups._789 => Digits._456,
            Groups._456 | Groups._789 => Digits._123,
            _ => Digits._1_to_9,
        };
    }

    public abstract class Paired(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public sealed override Digits Restrict(Digits other)
        {
            var range = Groups.None;
            if ((other & Digits._123).HasAny) range |= Groups._123;
            if ((other & Digits._456).HasAny) range |= Groups._456;
            if ((other & Digits._789).HasAny) range |= Groups._789;

            return Restrict(range);
        }

        protected abstract Digits Restrict(Groups range);
    }

    [Flags]
    public enum Groups
    {
        None = 0,
        _123 = 1,
        _456 = 2,
        _789 = 4,
    }
}
