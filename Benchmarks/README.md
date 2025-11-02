# Benchmarks

## Digits iteration
Test all 512 possible states of `Digits`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

## Solvers
Tested on 1000 puzzles. Results per puzzle.

| Method    | Config     | Mean        | Ratio |
|---------- |----------- |------------:|------:|
| Reference | Diabolical | 2,517.51 us | 33.07 |
| DLX       | Diabolical |    85.41 us |  1.12 |
| Dynamic   | Diabolical |    76.15 us |  1.00 |
| Default   | Diabolical |   108.77 us |  1.43 |
| All       | Diabolical |   381.40 us |  5.01 |
|           |            |             |       |
| Reference | Hard       | 1,144.52 us | 42.86 |
| DLX       | Hard       |    65.44 us |  2.45 |
| Dynamic   | Hard       |    26.71 us |  1.00 |
| Default   | Hard       |    50.38 us |  1.89 |
| All       | Hard       |   289.95 us | 10.86 |
|           |            |             |       |
| Reference | Medium     | 1,122.50 us | 42.53 |
| DLX       | Medium     |    66.72 us |  2.53 |
| Dynamic   | Medium     |    26.40 us |  1.00 |
| Default   | Medium     |    49.67 us |  1.88 |
| All       | Medium     |   295.99 us | 11.21 |
|           |            |             |       |
| Reference | Easy       | 1,135.28 us | 42.01 |
| DLX       | Easy       |    65.82 us |  2.44 |
| Dynamic   | Easy       |    27.03 us |  1.00 |
| Default   | Easy       |    49.86 us |  1.85 |
| All       | Easy       |   300.53 us | 11.12 |
