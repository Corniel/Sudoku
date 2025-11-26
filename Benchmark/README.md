# Benchmarks

## Digits iteration
Test all 512 possible states of `Digits`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

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
