# Sudoku Solver
My attempt to write a [Sudoku](https://en.wikipedia.org/wiki/Sudoku) solver.

## Solver
The approach of my solver is that I specify both [clues](#Clues) and the
(potentially custom) [rule set](#Rule Set) to apply when trying to solve
the puzzle. By doing so, my solver can solve a wide variety Sudoku variants.

## Dancing Links Solver
Donald Knuth's [Dancing Links](https://en.wikipedia.org/wiki/Dancing_links)
[algorithm](https://en.wikipedia.org/wiki/Knuth%27s_Algorithm_X) solves Sudoku
puzzles in an ellagant way. Apply this strategy also to variants is less trivial.

## Suported variants

### Standard Sudoku
Obviously, standard Suduko's are supported.

``` csharp
var clues = Clues.Parse("""
    8..|...|...
    ..3|6..|...
    .7.|.9.|2..
    ---+---+---  
    .5.|..7|...
    ...|.45|7..
    ...|1..|.3.
    ---+---+---
    ..1|...|.68
    ..8|5..|.1.
    .9.|...|4..
    """);

var solution = Solver.Solve(clues);
```

### Anti-Knight Sudoku
In Anti-Knight (Antiknight), besides the regular houses, cells may also not
share a digit when they are on (chess) knight distance:

``` csharp
var clues = Clues.Parse("""
    ...|5..|...
    ...|..4|...
    .58|...|.2.
    ---+---+---
    ...|..9|...
    .6.|...|..5
    ...|1..|3..
    ---+---+---
    ..3|..2|4..
    6..|.78|...
    .9.|...|..1
    """);

var solution = Solver.Solve(clues, Rules.AntiKnight);
```

### Hyper Sudoku
Hyper Sudoku (also called Windoku) adds for extra 3x3 regions:

``` csharp
...|...|...
.11|1.2|22.
.11|1.2|22.
---+---+---
.11|1.2|22.
...|...|...
.33|3.4|44.
---+---+---
.33|3.4|44.
.33|3.4|44.
...|...|...

var clues = Clues.Parse("""
    .4.|...|..9
    9..|...|8..
    .1.|3..|...
    ---+---+---
    ...|4.2|..8
    ...|.3.|...
    ...|...|7.5
    ---+---+---
    ...|.9.|...
    .67|..4|...
    ...|..5|4..
    """);

var solution = Solver.Solve(clues, Rules.Hyper);
```

### Jigsaw Sudoku
Jigsaw Sudoku has irreguarly shared boxes instead of the standard 3x3 boxes.

``` csharp
var clues = Clues.Parse("""
    4..7.9.2.
    ....2....
    .9...8...
    1.4...3..
    7..4.1..2
    ..2...1.3
    ...6...1.
    ....4....
    .1.2...45
    """);

var solution = Solver.Solve(clues, Rules.Jigsaw("""
    AAABBBBCC
    AAABBBBCC
    AADDEBCCC 
    ADDDEEFCC
    DDDEEEFFF
    GGDEEFFFH
    GGGJEFFHH
    GGJJJJHHH
    GGJJJJHHH
    """));
```

### Killer Sudoku
The solver can also solve [Killer Sudoku's](https://en.wikipedia.org/wiki/Killer_sudoku).
As there is no standard plain text format to describe these (that I'm aware of) there are
two support formats that seem logical:

``` csharp
var rules = Sudoku.Common.Groups.Cage("""
    AAB|BBC|DEF
    GGH|HCC|DEF
    GGI|ICJ|KKF
    ---+---+---
    LMM|INJ|KOF
    LPP|QNJ|OOR
    SPT|QNU|VVR
    ---+---+---
    STT|QWU|UXX
    SYZ|WWa|aXX
    SYZ|Wbb|bcc

    A = 3   B = 15  C = 22  D = 4
    E = 16  F = 15  G = 25  H = 17
    I = 9   J = 8   K = 20  L = 6
    M = 14  N = 17  O = 17  P = 13
    Q = 20  R = 12  S = 27  T = 6
    U = 20  V = 6   W = 10  X = 14
    Y = 8   Z = 16  a = 15  b = 13  c = 17
""");

var rules_ = Sudoku.Common.Groups.Cage("""
    27 = (0,0) + (0,1) + (1,0) + (2,0)
    13 = (0,2) + (1,1) + (1,2) + (2,1)
    15 = (0,3) + (1,3) + (2,3) + (3,3) + (4,3)
    28 = (2,2) + (3,0) + (3,1) + (3,2)
    17 = (4,0) + (5,0) + (4,1) + (4,2)
    17 = (5,1) + (5,2) + (5,3) + (5,4)
    20 = (6,0) + (6,1) + (6,2) + (7,2)
    25 = (7,0) + (7,1) + (8,0) + (8,1) + (8,2)
    33 = (5,5) + (6,5) + (6,4) + (6,3) + (7,3)
    16 = (0,4) + (1,4) + (1,5) + (1,6)
    16 = (0,5) + (0,6) + (0,7) + (0,8)
    27 = (1,7) + (1,8) + (2,7) + (2,8)
""");

var solution = Solver.Solve(Clues.Empty, rules);
```

### X-Sudoku
With X-Sudoku, the to diagonals are also considered [houses](#House).

``` csharp
var clues = Clues.Parse("""
    .1.|2.3|.4.
    8..|...|6.5
    .7.|...|...
    ---+---+---
    4..|...|..6
    ...|...|...
    2..|...|..7
    ---+---+---
    ...|...|.9.
    7.9|...|..8
    .2.|3.4|.5.
    """);

var solution = Solver.Solve(clues, Rules.XSudoku);
```

### Cracking The Cryptic
[Cracking Tye Cryptic](https://www.youtube.com/@CrackingTheCryptic) is a YouTube
channel dedicated to solving world-class puzzles (their wording, not mine). With
the extra [constraints](#Constraint) implemented, the solver has been
able to solve the following puzzles (so far):

| Date       | Puzzle                                                                       |      Speed |
|:----------:|------------------------------------------------------------------------------|-----------:|
| 2026-04-23 | [The Triple Crown](Puzzles/CrackingTheCryptic/2026/2026-04-23.cs)            |   535.7 ms |
| 2026-04-20 | [Williwaw](Puzzles/CrackingTheCryptic/2026/2026-04-20.cs)                    |   520.9 ms |
| 2026-04-18 | [Shirkflation](Puzzles/CrackingTheCryptic/2026/2026-04-18.cs)                |    39.4 ms |
| 2026-04-09 | [Mayan Ruins](Puzzles/CrackingTheCryptic/2026/2026-04-09.cs)                 | 2,340.8 µs |
| 2026-04-08 | [Colorful Whispers](Puzzles/CrackingTheCryptic/2026/2026-04-08.cs)           |   242.2 µs |
| 2026-04-04 | [Farrago](Puzzles/CrackingTheCryptic/2026/2026-04-04.cs)                     |   105.1 ms |
| 2026-03-30 | [The X and The V Squared](Puzzles/CrackingTheCryptic/2026/2026-04-04.cs)     | 2,403.1 µs |
| 2026-02-25 | [XII](Puzzles/CrackingTheCryptic/2026/2026-02-25.cs)                         |   154.2 µs |
| 2026-02-20 | [Catacomb](Puzzles/CrackingTheCryptic/2026/2026-02-20.cs)                    | 2,678.2 µs |
| 2026-01-13 | [Quality Street](Puzzles/CrackingTheCryptic/2026/2026-01-13.cs)              |   195.3 ms |
| 2026-01-08 | [Paper Snowflake](Puzzles/CrackingTheCryptic/2026/2026-01-08.cs)             | 7,428.3 µs |
| 2025-12-31 | [Venice](Puzzles/CrackingTheCryptic/2025/2025-12-31.cs)                      |   133.5 ms |
| 2025-12-27 | [Tinsel & Baubles](Puzzles/CrackingTheCryptic/2025/2025-12-27.cs)            |   554.3 ms |
| 2025-12-25 | [Xmas 2025](Puzzles/CrackingTheCryptic/2025/2025-12-25.cs)                   | 4,846.8 µs |
| 2025-12-24 | [Star Of Bethlehem](Puzzles/CrackingTheCryptic/2025/2025-12-24.cs)           | 5,372.1 µs |
| 2025-12-22 | [Arrow Renban Sudoku](Puzzles/CrackingTheCryptic/2025/2025-12-22.cs)         |    19.6 ms |
| 2025-12-20 | [Zebra Knights](Puzzles/CrackingTheCryptic/2025/2025-12-20.cs)               |   192.2 µs |
| 2025-12-17 | [The Fireflies' Pairing](Puzzles/CrackingTheCryptic/2025/2025-12-17.cs)      |   363.5 µs |
| 2025-12-15 | [For Daniël.](Puzzles/CrackingTheCryptic/2025/2025-12-15.cs)                 | 3,444.7 µs |
| 2025-12-12 | [Crossroads On Another World](Puzzles/CrackingTheCryptic/2025/2025-12-12.cs) |    32.4 ms |
| 2025-12-11 | [Fallen Mast In A Storm](Puzzles/CrackingTheCryptic/2025/2025-12-11.cs)      | 4,956.5 µs |
| 2025-12-08 | [Heavy Is The Crown](Puzzles/CrackingTheCryptic/2025/2025-12-08.cs)          | 5,866.3 µs |
| 2025-12-07 | [Odd Way To Even Out](Puzzles/CrackingTheCryptic/2025/2025-12-07.cs)         | 1,141.7 ms |
| 2025-12-04 | [Lockdown](Puzzles/CrackingTheCryptic/2025/2025-12-04.cs)                    | 4,331.1 ms |
| 2025-11-28 | [Brink](Puzzles/CrackingTheCryptic/2025/2025-11-28.cs)                       |    28.0 ms |
| 2025-11-25 | [Simple Miracle](Puzzles/CrackingTheCryptic/2025/2025-11-25.cs)              |   811.5 µs |
| 2025-11-23 | [Ice Breaker](Puzzles/CrackingTheCryptic/2025/2025-11-23.cs)                 |    69.5 ms |
| 2025-11-18 | [Equivalence](Puzzles/CrackingTheCryptic/2025/2025-11-18.cs)                 |    60.6 ms |
| 2025-11-17 | [wicked](Puzzles/CrackingTheCryptic/2025/2025-11-17.cs)                      |    20.2 ms |
| 2025-11-14 | [Braiding Sweetgrass](Puzzles/CrackingTheCryptic/2025/2025-11-14.cs)         | 7,059.6 µs |
| 2025-11-01 | [Parity Patrol 101](Puzzles/CrackingTheCryptic/2025/2025-11-01.cs)           |    34.3 ms |
| 2025-10-17 | [Who’s Afraid Of 13](Puzzles/CrackingTheCryptic/2025/2025-10-17.cs)          |    48.6 µs |
| 2025-10-13 | [Wanddeko](Puzzles/CrackingTheCryptic/2025/2025-10-13.cs)                    |   227.6 ms |
| 2025-10-07 | [Golden Arrow](Puzzles/CrackingTheCryptic/2025/2025-10-07.cs)                |    22.8 µs |
| 2025-09-25 | [Threads Of Silence](Puzzles/CrackingTheCryptic/2025/2025-09-25.cs)          | 1,819.6 ms |
| 2025-09-18 | [Diagonality](Puzzles/CrackingTheCryptic/2025/2025-09-18.cs)                 | 3,795.6 µs |
| 2025-09-15 | [Studious](Puzzles/CrackingTheCryptic/2025/2025-09-15.cs)                    |    47.9 ms |
| 2025-09-13 | [Royalty](Puzzles/CrackingTheCryptic/2025/2025-09-13.cs)                     |   110.4 ms |
| 2025-09-09 | [Seylla](Puzzles/CrackingTheCryptic/2025/2025-09-09.cs)                      |   101.1 ms |
| 2025-09-08 | [Four at a Time](Puzzles/CrackingTheCryptic/2025/2025-09-08.cs)              |    73.8 ms |
| 2025-09-05 | [Besties 2](Puzzles/CrackingTheCryptic/2025/2025-09-05.cs)                   | 2,074.1 µs |
| 2025-09-04 | [Packing Problem](Puzzles/CrackingTheCryptic/2025/2025-09-04.cs)             |    20.3 s  |
| 2025-09-03 | [Most Squares](Puzzles/CrackingTheCryptic/2025/2025-09-.cs)                | 1,204.4 ms |
| 2025-08-21 | [Miracle Of Eleven](Puzzles/CrackingTheCryptic/2025/2025-08-21.cs)           |    63.4 µs |
| 2025-08-19 | [Pile Of 15](Puzzles/CrackingTheCryptic/2025/2025-08-19.cs)                  |    33.8 ms |
| 2025-08-07 | [Unstable Seesaws](Puzzles/CrackingTheCryptic/2025/2025-08-07.cs)            | 5,913.9 µs |
| 2025-05-21 | [Stepped Thermos](Puzzles/CrackingTheCryptic/2025/2025-05-21.cs)             |   122.8 µs |
| 2025-05-11 | [Quadrants](Puzzles/CrackingTheCryptic/2025/2025-05-11.cs)                   |   193.0 ms |
| 2025-05-02 | [Arrows v.s. Thermos](Puzzles/CrackingTheCryptic/2025/2025-05-02.cs)         |   159.7 µs |
| 2025-04-23 | [Indifferent Neighbours](Puzzles/CrackingTheCryptic/2025/2025-24-23.cs)      |   304.1 µs |
| 2025-03-25 | [Rapuzzle](Puzzles/CrackingTheCryptic/2025/2025-03-25.cs)                    |   228.3 µs |
| 2025-01-31 | [ZL GW DA](Puzzles/CrackingTheCryptic/2025/2025-01-31.cs)                    |   156.7 ms |
| 2025-01-07 | [Sort of Miraculous](Puzzles/CrackingTheCryptic/2025/2025-01-07.cs)          |    21.8 ms |
| 2024-12-24 | [Arrow Thermo 2](Puzzles/CrackingTheCryptic/2024/2024-12-24.cs)              | 3,604.4 µs |
| 2024-12-09 | [Elbow Join](Puzzles/CrackingTheCryptic/2024/2024-12-09.cs)                  |    35.0 ms |
| 2024-12-08 | [Fortune Cookie II](Puzzles/CrackingTheCryptic/2024/2024-12-08.cs)           | 2,179.5 µs |
| 2024-11-18 | [Equivalenee](Puzzles/CrackingTheCryptic/2024/2024-11-18.cs)                 |   242.5 ms |
| 2024-11-16 | [80](Puzzles/CrackingTheCryptic/2024/2024-11-16.cs)                          |   560.8 µs |
| 2024-09-29 | [3 In the Corner](Puzzles/CrackingTheCryptic/2024/2024-09-29.cs)             |   208.5 µs |
| 2024-04-06 | [Seesaw](Puzzles/CrackingTheCryptic/2024/2024-04-06.cs)                      | 4,097.8 ms |
| 2024-02-21 | [Confiable](Puzzles/CrackingTheCryptic/2024/2024-02-21.cs)                   | 3,892.9 µs |
| 2024-01-08 | [Tulpenblüte](Puzzles/CrackingTheCryptic/2024/2024-01-08.cs)                 | 4,889.0 µs |
| 2023-01-15 | [Arbitrary Code Execution](Puzzles/CrackingTheCryptic/2023/2023-01-15.cs)    |    30.7 µs |
| 2022-12-15 | [The 15th Day Of Christmas](Puzzles/CrackingTheCryptic/2022/2022-12-15.cs)   | 2,056.3 µs |
| 2022-11-21 | [Can't Teach An Old Dog...](Puzzles/CrackingTheCryptic/2022/2022-11-21.cs)   |   184.0 µs |
| 2022-08-10 | [Superking](Puzzles/CrackingTheCryptic/2022/2022-08-10.cs)                   |    11.0 ms |
| 2022-05-03 | [The Dutch Miracle](Puzzles/CrackingTheCryptic/2022/2022-05-03.cs)           |    46.2 µs |
| 2022-04-27 | [The Aquarium](Puzzles/CrackingTheCryptic/2022/2022-04-27.cs)                | 5,018.7 ms |
| 2022-03-13 | [The Trident](Puzzles/CrackingTheCryptic/2022/2022-03-13.cs)                 | 4,478.3 ms |
| 2021-10-06 | [Dutch Whispers](Puzzles/CrackingTheCryptic/2021/2021-10-06.cs)              |    54.6 µs |
| 2021-08-15 | [Steering Wheel](Puzzles/CrackingTheCryptic/2021/2021-08-15.cs)              |   236.1 µs |
| 2021-08-05 | [Checkerboard](Puzzles/CrackingTheCryptic/2021/2021-08-05.cs)                |   267.1 s  |
| 2021-07-10 | [White Room](Puzzles/CrackingTheCryptic/2021/2021-07-10.cs)                  | 3,447.2 µs |
| 2021-05-01 | [Wave Particals](Puzzles/CrackingTheCryptic/2021/2021-05-01.cs)              |    81.3 ms |
| 2021-04-23 | [Wheels Of Arrows](Puzzles/CrackingTheCryptic/2021/2021-04-23.cs)            |   142.8 ms |
| 2021-04-19 | [Archers And Arrows](Puzzles/CrackingTheCryptic/2021/2021-04-19.cs)          | 6,941.6 µs |
| 2021-04-11 | [Third Times The Charm](Puzzles/CrackingTheCryptic/2021/2021-04-11.cs)       |   538.1 ms |
| 2021-02-29 | [Mounted Archery 3](Puzzles/CrackingTheCryptic/2021/2021-02-29.cs)           | 5,898.9 µs |
| 2021-02-21 | [Ten Knights](Puzzles/CrackingTheCryptic/2021/2021-02-21.cs)                 |    31.4 ms |
| 2021-01-19 | [German Whispers](Puzzles/CrackingTheCryptic/2021/2021-01-19.cs)             |   637.3 µs |
| 2021-01-06 | [Non-consecutive Killer](Puzzles/CrackingTheCryptic/2021/2021-01-06.cs)      |   192.2 ms |
| 2020-12-30 | [Dotless Kropki Sudoku X](Puzzles/CrackingTheCryptic/2020/2020-12-30.cs)     |   874.8 µs |
| 2020-10-15 | [Non-consecutive](Puzzles/CrackingTheCryptic/2020/2020-10-15.cs)             |    27.1 µs |
| 2020-09-30 | [Classic Sudoku!](Puzzles/CrackingTheCryptic/2020/2020-09-30-1.cs)           |    10.7 µs |
| 2020-09-30 | [Tatooine Sunset](Puzzles/CrackingTheCryptic/2020/2020-09-30.cs)             |    19.4 µs |
| 2020-09-18 | [Patto Patto](Puzzles/CrackingTheCryptic/2020/2020-09-18.cs)                 |   125.2 µs |
| 2020-09-15 | [Sudoku XV](Puzzles/CrackingTheCryptic/2020/2020-09-15.cs)                   |    98.9 µs |
| 2020-08-10 | [Heartbeat](Puzzles/CrackingTheCryptic/2020/2020-08-10.cs)                   | 7,677.2 µs |
| 2020-07-31 | [Arrow/Group Sum](Puzzles/CrackingTheCryptic/2020/2020-05-15.cs)             | 8,965.6 ms |
| 2020-07-26 | [Classic Sudoku](Puzzles/CrackingTheCryptic/2020/2020-07-26.cs)              |    12.7 µs |
| 2020-05-15 | [Equal Sudoku](Puzzles/CrackingTheCryptic/2020/2020-05-15.cs)                |   355.6 ms |
| 2020-05-06 | [Antiknight Kille](Puzzles/CrackingTheCryptic/2020/2020-05-06.cs)            |    11.3 ms |
| 2020-04-27 | [The Sequal](Puzzles/CrackingTheCryptic/2020/2020-04-27.cs)                  |   113.6 µs |
| 2020-04-22 | [CTC](Puzzles/CrackingTheCryptic/2020/2020-04-22.cs)                         |    57.9 µs |
| 2020-04-21 | [Partial Killer](Puzzles/CrackingTheCryptic/2020/2020-04-22.cs)              |    10.2 ms |
| 2020-04-13 | [Killer Sudoku](Puzzles/CrackingTheCryptic/2020/2020-04-13.cs)               | 3,302.2 µs |
| 2020-04-12 | [Magic Square Sudoku](Puzzles/CrackingTheCryptic/2020/2020-04-12.cs)         |   290.9 µs |
| 2020-03-14 | [Pi](Puzzles/CrackingTheCryptic/2020/2020-03-14.cs)                          |    51.1 µs |
| 2020-02-19 | [Classic Sudoku](Puzzles/CrackingTheCryptic/2020/2020-02-19.cs)              |   188.1 µs |
| 2020-02-09 | [Thermo Sudoku](Puzzles/CrackingTheCryptic/2020/2020-02-09.cs)               |   120.8 µs |
| 2020-01-19 | [New York Times](Puzzles/CrackingTheCryptic/2020/2020-01-19.cs)              |    52.0 µs |
| 2020-01-11 | [<= 5](Puzzles/CrackingTheCryptic/2020/2020-01-11.cs)                        |   602.9 µs |
| 2019-11-27 | [Non-consecutive Anti-Knight](Puzzles/CrackingTheCryptic/2019/2019-11-27.cs) |   249.1 µs |
| 2019-11-16 | [Jigsaw](Puzzles/CrackingTheCryptic/2019/2019-11-16.cs)                      | 4,728.9 ms |
| 2019-10-24 | [Square Killer](Puzzles/CrackingTheCryptic/2019/2019-10-24.cs)               | 5,554.2 ms |
| 2019-09-26 | [New York Times](Puzzles/CrackingTheCryptic/2019/2019-09-26.cs)              |    27.0 µs |
| 2019-08-29 | [New York Times](Puzzles/CrackingTheCryptic/2019/2019-08-29.cs)              |    28.7 µs |
| 2019-05-26 | [Thermo Sudoku](Puzzles/CrackingTheCryptic/2019/2019-05-26.cs)               | 3,498.0 µs |
| 2019-05-09 | [Jigsaw Sudoku](Puzzles/CrackingTheCryptic/2019/2019-05-09.cs)               | 3,932.0 µs |
| 2019-04-19 | [Jigsaw Sudoku](Puzzles/CrackingTheCryptic/2019/2019-04-19.cs)               | 7,428.3 µs |
| 2019-03-18 | [X-Wing Sudoku](Puzzles/CrackingTheCryptic/2019/2019-03-18.cs)               |    36.6 µs |
| 2019-02-01 | [New York Times](Puzzles/CrackingTheCryptic/2019/2019-02-01.cs)              |    43.5 µs |
| 2019-01-15 | [Hard Sudoku](Puzzles/CrackingTheCryptic/2019/2019-01-15.cs)                 |    52.9 µs |
| 2018-09-19 | [New York Times](Puzzles/CrackingTheCryptic/2018/2018-09-19.cs)              | 8,600.0 ns |
| 2018-06-07 | [New York Times](Puzzles/CrackingTheCryptic/2018/2018-06-07.cs)              |    93.2 µs |
| 2017-09-23 | [Diabolic](Puzzles/CrackingTheCryptic/2017/2017-09-23.cs)                    |    23.0 µs |
| 2017-09-18 | [Super Fiendish](Puzzles/CrackingTheCryptic/2017/2017-09-18.cs)              |    17.5 µs |
| 2017-08-31 | [Super Fiendish](Puzzles/CrackingTheCryptic/2017/2017-08-31.cs)              |    17.9 µs |
| 2017-08-26 | [Killer Sudoku](Puzzles/CrackingTheCryptic/2017/2017-08-06.cs)               |   180.9 µs |

## Models

### Cell
The `Cell` contains the [position](#Pos) and the value of the cell. The `0` value
indicates that the value of the cell is not known.

### Cells
The `Cells` contain all [cell](#Cell)s with their values. It is a wrapper for
an `array`, and the digits of cells can be changed.

### Clues
The `Clues` contain all given [cells](#Cell) for a puzzle.

### Digits
The `Digits` contain all possible digits for a specified [cell](#Cell).
The underlying `uint` ranges from `0` (no options) to `0b_111_111_111_0` when
all 9 digits are set. A single digit flag is calculated by
`1 << digit`, hence the zero-th bit will allways be zero. Using bit
operators (such as `&`, `|`, `^`, and `~`) it allows manipulation of the
digits.

### House
The `House` contains all cells as [set](#Set) that must have unique digits.
Common houses are: rows, columns, 3x3 boxes, and diagonals.

### Position
The `Pos` is an index based value type that can be deconstructed in a row and
column component. Its `ToString()` value also does this to help while debugging.

### PosSet
The `PosSet` is a a set of [positions](#Position) that uses bitmask
manipulation (similar to [Digits](#Digits)). It's iterator is fast too,
but iterating an `ImmutableArray<Pos>` is even faster, so while solving, the
latter is preferred.

## Rule
There are 3 different types of rules:

### Constraint
A `Constraint` specifies if for a specific state of the [cells](#Cells) it is
saticfied.

### Restriction
A `Restriction` specifies the allowed [digits](#Digits) for a [cell](#Cell)
base on the state of the [cells](#Cells).

### Set
A `Set` which group of [cells](#Cell) must have different [digits](#Digits). 

## Rule Set
A `RuleSet` is an (immutable) collection of [rules](#Rule).

## Test Sets
Both [Kaggle](https://www.kaggle.com/datasets/rohanrao/sudoku/) as
[Sudoku Exchange(https://github.com/grantm/sudoku-exchange-puzzle-bank) published
test sets containing zillions of generated puzzles to solve.

| Set                  | Puzzles |   Dynamic Solver   | |       Knuth's DLX       | | |      Reference backtracker   | | |
|:---------------------|--------:|----------:|---------:|----------:|---------:|-----:|---------:|-------.-----:|-------:|
| Kaggle (300k)[1]     | 300,000 | 55.21 k/s | 18.11 µs | 14.47 k/s | 69.13 µs | 3.82 | 1.87 k/s |    534.00 µs |  29.48 |
| Exchange (easy)      | 100,000 | 58.84 k/s | 17.00 µs | 15.54 k/s | 64.35 µs | 3.79 | 1.38 k/s |    724.43 µs |  42.62 |
| Exchange (medium)    | 352,643 | 45.08 k/s | 22.18 µs | 14.43 k/s | 69.31 µs | 3.12 | 0.71 k/s |  1,408.18 µs |  63.49 |
| Exchange (hard)      | 183,357 | 36.23 k/s | 27.61 µs | 13.71 k/s | 72.95 µs | 2.64 | 0.69 k/s |  1,450.88 µs |  52.56 |
| Exchange (diabolic)  | 119,681 | 29.25 k/s | 34.18 µs | 13.12 k/s | 76.21 µs | 2.23 | 0.65 k/s |  1,542.81 µs |  45.13 |
| Exchange (1000*)[2]  |   1,000 | 14.37 k/s | 69.59 µs | 10.71 k/s | 93.41 µs | 1.34 | 0.37 k/s |  2,716.72 µs |  39.04 |
| Generated (hard)     |  17,905 | 22.79 k/s | 43.88 µs | 12.84 k/s | 77.91 µs | 1.78 | 0.56 k/s |  1,776.96 µs |  40.49 |
| New York Times       |     717 | 17.65 k/s | 56.65 µs | 11.87 k/s | 84.21 µs | 1.49 | 0.10 k/s |  9,767.92 µs | 172.43 |
| Cracking the Cryptic |      17 | 12.83 k/s | 77.92 µs | 11.64 k/s | 85.94 µs | 1.10 | 0.06 k/s | 17,523.12 µs | 224.89 |

* [1] From the 9M puzzles (with an overkill of given digits) only the hardest 300k haven been chosen
* [2] The hardest 1000 of the diabolic set
