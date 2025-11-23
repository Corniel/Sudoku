namespace Sudoku.Restrictions;

public sealed class LookupPair(Pos appliesTo, Pos other, LookupDigits lookup) : Pair(appliesTo, other)
{
    private readonly LookupDigits Lookup = lookup;

    /// <inheritdoc />
    public override Digits Restrict(Digits other) => Lookup[other];

    /// <summary>Creates a couple of the restrictions.</summary>
    [Pure]
    public Couple<Pair> Couple() => new(this, new LookupPair(Other, AppliesTo, Lookup));

    /// <summary>Creates a lookup combining the digits allowed per digit.</summary>
    public static LookupDigits Init(Digits[] byDigit)
    {
        var lookup = new LookupDigits();

        foreach (var digits in Digits.All)
            foreach (var digit in digits)
                lookup[digits] |= byDigit[digit];

        return lookup;
    }

    /// <summary>Creates a lookup combining the digits allowed per digit.</summary>
    public static LookupDigits Init(Func<int, Digits> calculate)
    {
        var lookup = new LookupDigits();

        foreach (var digits in Digits.All)
            foreach (var digit in digits)
                lookup[digits] |= calculate(digit);

        return lookup;
    }

    /// <summary>Creates a lookup that ensures that bot cells are of the same class.</summary>
    /// <remarks>
    /// A cell can be member of a class, when the other cell can also not be an
    /// member of that class.
    /// </remarks>
    public static LookupDigits SameClass(Digits[] classes)
    {
        var lookup = new LookupDigits();
        var allowed = Digits.New(classes);

        foreach (var digits in Digits.All.Skip(1))
        {
            var restriction = allowed;
            foreach (var cls in classes)
            {
                if ((digits & cls).HasNone)
                    restriction ^= cls;
            }
            lookup[digits] = restriction;
        }

        return lookup;
    }

    /// <summary>Creates a lookup that ensures that bot cells are of a different class.</summary>
    /// <remarks>
    /// A cell can not be a member of a class, when the other cell nust be
    /// member of that class.
    /// </remarks>
    public static LookupDigits DiffClass(Digits[] classes)
    {
        var lookup = new LookupDigits();
        var allowed = Digits.New(classes);

        foreach (var digits in Digits.All.Skip(1))
        {
            lookup[digits] = classes.FirstOrNone(cls => (digits ^ cls).HasNone) is { } cls
                ? allowed ^ cls
                : allowed;

            //if(

            //foreach (var cls in classes)
            //{
            //    var inverse = digits ^ cls;
            //    if (inverse.HasNone)
            //    {
            //        lookup[digits] = ;
            //        break;
            //    }
            //}
        }

        return lookup;
    }
}
