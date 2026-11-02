# Benchmarks

## Digits iteration
Test 1_000_000 states of `Digits` (every state roughtly 2000 times).

| Method       | Mean      | Ratio |
|------------- |----------:|------:|
| Iterator     |  7.153 ms |  1.00 |
| Immutable    |  9.065 ms |  1.27 |
| List         | 11.466 ms |  1.60 |
| Array        |  9.113 ms |  1.27 |
| Old_Iterator |  9.849 ms |  1.38 |

## Binary conversions
To convert cells from and to an `Int128`.
| Binary   | Mean     |
|--------- |---------:|
| ToInt128 | 444.1 ns |
| ToCells  | 943.7 ns |

## Generation

### Grids
A randomly filled valid grid.
| Generate | Mean     |  per sec. |
|----------|---------:|----------:|
| Grid     | 167.0 ns | 5.988 M/s |

### Puzzles
A humanly solvable puzzle requiring more than naked singles.
| Generate | Mean       |  per sec. |
|--------- |-----------:|----------:|
| Puzzles  | 5,927.5 us | 168.7 k/s |
