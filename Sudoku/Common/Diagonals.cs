namespace Sudoku.Common;

public static class Diagonals
{
    public static readonly ImmutableArray<PosSet> NESWs =
    [
        .. Grid.NamedGroups("""
        . B C D E F G H I
        B C D E F G H I J
        C D E F G H I J K
        D E F G H I J K L
        E F G H I J K L M
        F G H I J K L M N
        G H I J K L M N O
        H I J K L M N O P
        I J K L M N O P .
        """).Select(c => PosSet.New(c))
    ];

    public static readonly ImmutableArray<PosSet> NWSEs =
    [
        .. Grid.NamedGroups("""
        I J K L M N O P .
        H I J K L M N O P
        G H I J K L M N O
        F G H I J K L M N
        E F G H I J K L M
        D E F G H I J K L
        C D E F G H I J K
        B C D E F G H I J
        . B C D E F G H I
        """).Select(c => PosSet.New(c))
    ];
}
