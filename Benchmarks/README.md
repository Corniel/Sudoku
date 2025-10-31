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
| Reference | Diabolical | 2,448.36 us | 33.47 |
| DLX       | Diabolical |    85.08 us |  1.16 |
| Dancing   | Diabolical |    73.14 us |  1.00 |
| Default   | Diabolical |   104.15 us |  1.42 |
| All       | Diabolical |   384.61 us |  5.26 |
|           |            |             |       |
| Reference | Hard       | 1,153.27 us | 40.49 |
| DLX       | Hard       |    64.49 us |  2.26 |
| Dancing   | Hard       |    28.50 us |  1.00 |
| Default   | Hard       |    49.66 us |  1.74 |
| All       | Hard       |   293.96 us | 10.32 |
|           |            |             |       |
| Reference | Medium     | 1,129.05 us | 40.67 |
| DLX       | Medium     |    63.97 us |  2.30 |
| Dancing   | Medium     |    27.76 us |  1.00 |
| Default   | Medium     |    50.43 us |  1.82 |
| All       | Medium     |   284.69 us | 10.26 |
|           |            |             |       |
| Reference | Easy       | 1,133.79 us | 40.51 |
| DLX       | Easy       |    64.57 us |  2.31 |
| Dancing   | Easy       |    27.99 us |  1.00 |
| Default   | Easy       |    49.28 us |  1.76 |
| All       | Easy       |   290.71 us | 10.39 |
