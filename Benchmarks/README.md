# Benchmarks

## Value iteration
Test all 512 possible states of `Values`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

## Solvers
Tested on 1000 puzzles. Results per puzzle.

| Method    | Config     | Mean        | Ratio |
|---------- |----------- |------------:|------:|
| Reference | Diabolical | 2,459.00 us | 23.12 |
| Dynamic   | Diabolical |   106.36 us |  1.00 |
|           |            |             |       |
| Reference | Hard       | 1,115.58 us | 18.53 |
| Dynamic   | Hard       |    60.22 us |  1.00 |
|           |            |             |       |
| Reference | Medium     | 1,134.83 us | 18.64 |
| Dynamic   | Medium     |    60.93 us |  1.00 |
|           |            |             |       |
| Reference | Easy       | 1,129.74 us | 18.99 |
| Dynamic   | Easy       |    59.51 us |  1.00 |
