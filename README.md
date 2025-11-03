# Sudoku Solver
My attempt to write a [Sudoku](https://en.wikipedia.org/wiki/Sudoku) solver.

## Solver
The approach of my solver is that I specify both [clues](#Clues) and the
(potentially custom) [constraints](#Constraint) to apply when trying to solve
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
    8..│...│...
    ..3│6..│...
    .7.│.9.│2..
    ───┼───┼───
    .5.│..7│...
    ...│.45│7..
    ...│1..│.3.
    ───┼───┼───
    ..1│...│.68
    ..8│5..│.1.
    .9.│...│4..
    """);

var solution = Solver.Solve(clues);
```

### Anti-Knight Sudoku
In Anti-Knight (Antiknight), besides the regular houses, cells may also not
share a digit when they are on (chess) knight distance:

``` csharp
var clues = Clues.Parse("""
    ...│5..│...
    ...│..4│...
    .58│...│.2.
    ───┼───┼───
    ...│..9│...
    .6.│...│..5
    ...│1..│3..
    ───┼───┼───
    ..3│..2│4..
    6..│.78│...
    .9.│...│..1
    """);

var solution = Solver.Solve(clues, Rules.AntiKnight);

### Hyper Sudoku
Hyper Sudoku (also called Windoku) adds for extra 3x3 regions:

```
...│...│...
.11│1.2│22.
.11│1.2│22.
───┼───┼───
.11│1.2│22.
...│...│...
.33│3.4│44.
───┼───┼───
.33│3.4│44.
.33│3.4│44.
...│...│...
```

``` csharp
var clues = Clues.Parse("""
    .4.│...│..9
    9..│...│8..
    .1.│3..│...
    ───┼───┼───
    ...│4.2│..8
    ...│.3.│...
    ...│...│7.5
    ───┼───┼───
    ...│.9.│...
    .67│..4│...
    ...│..5│4..
    """);

var solution = Solver.Solve(clues, Rules.Hyper);
```

### Jigsaw Sudoku
Jigsaw Sudoku has irreguarly shared boxes instead of the standard 3x3 boxes.

``` csharp
var clues = Clues.Parse("""
    4..│7.9│.2.
    ...│.2.│...
    .9.│..8│...
    ───┼───┼───
    1.4│...│3..
    7..│4.1│..2
    ..2│...│1.3
    ───┼───┼───
    ...│6..│.1.
    ...│.4.│...
    .1.│2..│.45
    """);

var solution = Solver.Solve(clues, Rules.Jigsaw("""
    AAA│BBB│BCC
    AAA│BBB│BCC
    AAD│DEB│CCC 
    ───┼───┼───        
    ADD│DEE│FCC
    DDD│EEE│FFF
    GGD│EEF│FFH
    ───┼───┼───
    GGG│JEF│FHH
    GGJ│JJJ│HHH
    GGJ│JJJ│HHH
    """));
```

### Killer Sudoku
The solver can also solve [Killer Sudoku's](https://en.wikipedia.org/wiki/Killer_sudoku).
As there is no standard plain text format to describe these (that I'm aware of) there are
two support formats that seem logical:

``` csharp
var rules = KillerCages.Parse("""
    AAB│BBC│DEF
    GGH│HCC│DEF
    GGI│ICJ│KKF
    ───┼───┼───
    LMM│INJ│KOF
    LPP│QNJ│OOR
    SPT│QNU│VVR
    ───┼───┼───
    STT│QWU│UXX
    SYZ│WWa│aXX
    SYZ│Wbb│bcc

    A = 3   B = 15  C = 22  D = 4
    E = 16  F = 15  G = 25  H = 17
    I = 9   J = 8   K = 20  L = 6
    M = 14  N = 17  O = 17  P = 13
    Q = 20  R = 12  S = 27  T = 6
    U = 20  V = 6   W = 10  X = 14
    Y = 8   Z = 16  a = 15  b = 13  c = 17
""");

var rules_ = KillerCages.Parse("""
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
    .1.│2.3│.4.
    8..│...│6.5
    .7.│...│...
    ───┼───┼───
    4..│...│..6
    ...│...│...
    2..│...│..7
    ───┼───┼───
    ...│...│.9.
    7.9│...│..8
    .2.│3.4│.5.
    """);

var solution = Solver.Solve(clues, Rules.XSudoku);
```

### Cracking The Cryptic
[Cracking Tye Cryptic](https://www.youtube.com/@CrackingTheCryptic) is a YouTube
channel dedicated to solving world-class puzzles (their wording, not mine). With
the extra [constraints](#Constraint) implemented, the solver has been
able to solve the following puzzles (so far):

| Date       | Puzzle                                                          |       Speed |
|:----------:|---------------------------------------------------------------- |------------:|
| 2025-11-01 | [Parity Patrol 101](Puzzles/CrackingTheCryptic/2025_11_01.cs)   | 6,078.4 ms  |
| 2025-10-17 | [Who’s Afraid Of 13](Puzzles/CrackingTheCryptic/2025_10_17.cs)  |   385.3 µs  |
| 2025-10-07 | [Golden Arrow](Puzzles/CrackingTheCryptic/2025_10_07.cs)        |    95.6 µs  |
| 2025-09-15 | [Studious](Puzzles/CrackingTheCryptic/2025_09_15.cs)            |    18.3 ms  |
| 2025-09-13 | [Royalty](Puzzles/CrackingTheCryptic/2025_09_13.cs)             |    91.6 ms  |
| 2025-09-09 | [Seylla](Puzzles/CrackingTheCryptic/2025_09_09.cs)              |   159.8 s   |
| 2025-09-08 | [Four at a Time](Puzzles/CrackingTheCryptic/2025_09_08.cs)      | 3,591.7 ms  |
| 2025-09-04 | [Packing Problem](Puzzles/CrackingTheCryptic/2025_09_04.cs)     |   405.4 s   |
| 2025-08-21 | [Miracle Of Eleven](Puzzles/CrackingTheCryptic/2025_08_21.cs)   |   662.0 µs  |
| 2025-08-19 | [Pile Of 15](Puzzles/CrackingTheCryptic/2025_08_19.cs)          | 2,209.9 ms  |
| 2025-08-07 | [Unstable Seesaws](Puzzles/CrackingTheCryptic/2025_08_07.cs)    | 1,078.4 ms  |
| 2025-05-21 | [Stepped Thermos](Puzzles/CrackingTheCryptic/2025_05_21.cs)     | 2,759.3 µs  |
| 2025-05-11 | [Quadrants](Puzzles/CrackingTheCryptic/2025_05_11.cs)           | 1,558.2 ms  |
| 2025-05-02 | [Arrows v.s. Thermos](Puzzles/CrackingTheCryptic/2025_05_02.cs) |   515.1 µs  |
| 2025-03-25 | [Rapuzzle](Puzzles/CrackingTheCryptic/2025_03_25.cs)            | 2,686.6 µs  |
| 2025-01-07 | [Sort of Miraculous](Puzzles/CrackingTheCryptic/2025_01_07.cs)  |   726.8 ms  |
| 2024-12-24 | [Arrow Thermo 2](Puzzles/CrackingTheCryptic/2024_12_24.cs)      | 2,191.3 ms  |
| 2024-12-09 | [Elbow Join](Puzzles/CrackingTheCryptic/2024_12_09.cs)          |    26.4 min |
| 2024-12-08 | [Fortune Cookie II](Puzzles/CrackingTheCryptic/2024_12_08.cs)   | 8,992.1 ms  |
| 2024-11-18 | [80](Puzzles/CrackingTheCryptic/2024_11_18.cs)                  | 1,049.3 µs  |
| 2024-09-29 | [3 In the Corner](Puzzles/CrackingTheCryptic/2024_09_29.cs)     |          ?  |
| 2024-01-08 | [Tulpenblüte](Puzzles/CrackingTheCryptic/2024_01_08.cs)         |   194.5 ms  |
| 2022-05-03 | [The Dutch Miracle](Puzzles/CrackingTheCryptic/2022_05_03.cs)   |   459.0 µs  |
| 2022-03-13 | [The Trident](Puzzles/CrackingTheCryptic/2022_03_13.cs)         |          ?  |
| 2021-10-06 | [Dutch Whispers](Puzzles/CrackingTheCryptic/2021_10_06.cs)      |   208.4 µs  |
| 2020-09-30 | [Classic Sudoku!](Puzzles/CrackingTheCryptic/2020_09_30_1_.cs)  |    77.7 µs  |
| 2020-09-30 | [Tatooine Sunset](Puzzles/CrackingTheCryptic/2022_09_30.cs)     |    77.3 µs  |
| 2019-05-09 | [Jigsaw Sudoku](Puzzles/CrackingTheCryptic/2021_05_09.cs)       | 1,698.4 µs  |

## Models

### Cell
The `Cell` contains the [position](#Pos) and the value of the cell. The `0` value
indicates that the value of the cell is not known.

### Cells
The `Cells` contain all [cell](#Cell)s with their values. It is a wrapper for
an `array`, and the digits of cells can be changed.

### Clues
The `Clues` contain all given [cells](#Cell) for a puzzle.

## Constraint
The `Constraint` specfies the involved [positions](#PosSet) and the
[restrictions](#Restriction) per involved position.

### Digits
The `Digits` contain all possible digits for a specified [cell](#Cell).
The underlying `uint` ranges from `0` (no options) to `0b_111_111_111_0` when
all 9 digits are set. A single digit flag is calculated by
`1 << digit`, hence the zero-th bit will allways be zero. Using bit
operators (such as `&`, `│`, `^`, and `~`) it allows manipulation of the
digits.

### House
The `House` contains all cells as [set](#PosSet) that must have unique digits.
Common houses are: rows, columns, 3x3 boxes, and diagonals.

### Position
The `Pos` is an index based value type that can be deconstructed in a row and
column component. Its `ToString()` value also does this to help while debugging.

### PosSet
The `PosSet` is a a set of [positions](#Position) that uses bitmask
manipulation (similar to [Digits](#Digits)). It's iterator is fast too,
but iterating an `ImmutableArray<Pos>` is even faster, so while solving, the
latter is preferred.

### Restriction
The `Restricton` is defined on a [cell](#Cell), with a referenced to other
involved cells. It is able, based on a given state of [cells](#Cells), to
return a (restricted) set of [digits](#Digits).

## Test sets
Both [Kaggle](https://www.kaggle.com/datasets/rohanrao/sudoku/) as
[Sudoku Exchange(https://github.com/grantm/sudoku-exchange-puzzle-bank) published
test sets containing zillions of generated puzzles to solve.

| Set                 | Puzzles |   Dynamic Solver     |       Knuth's DLX            |     Reference backtracker      |
|:--------------------|--------:|----------:|---------:|----------:|---------:|------:|---------:|------------:|------:|
| Kaggle (300k)[1]    | 300,000 | 64.67 k/s | 15.46 µs | 15.37 k/s | 65.05 µs |  4.21 | 2.06 k/s |   486.46 µs | 31.46 |
| Exchange (easy)     | 100,000 | 85.31 k/s | 11.72 µs | 16.90 k/s | 59.16 µs |  5.05 | 1.53 k/s |   655.43 µs | 55.91 |
| Exchange (medium)   | 352,643 | 48.68 k/s | 20.54 µs | 15.61 k/s | 64.05 µs |  3.12 | 0.81 k/s | 1,237.18 µs | 60.22 |
| Exchange (hard)     | 183,357 | 36.13 k/s | 27.68 µs | 14.96 k/s | 66.85 µs |  2.42 | 0.76 k/s | 1,308.55 µs | 47.28 |
| Exchange (diabolic) | 119,681 | 28.58 k/s | 34.99 µs | 14.44 k/s | 69.25 µs |  1.98 | 0.72 k/s | 1,396.18 µs | 39.91 |
| Exchange (1000)[2]  |   1,000 | 12.76 k/s | 78.39 µs | 11.37 k/s | 87.96 µs |  1.12 | 0.41 k/s | 2,429.74 µs | 31.00 |

* [1] From the 9M puzzles (with an overkill of given digits) only the hardest 300k haven been chosen
* [2] The hardest 1000 of the diabolic set
