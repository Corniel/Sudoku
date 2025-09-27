# Benchmarks

## Value iteration
Test all 512 possible states of `Values`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

## Solvers
Tested on 1000 puzzles. Results per puzzle.

| Method            | Config     | Mean        | Ratio |
|------------------ |----------- |------------:|------:|
| Reference         | Diabolical | 2,459.03 us | 23.03 |
| No pre processing | Diabolical |   114.24 us |  1.07 |
| Default           | Diabolical |   106.77 us |  1.00 |
| All               | Diabolical |   115.65 us |  1.08 |
|                   |            |             |       |
| Reference         | Hard       | 1,140.97 us | 22.29 |
| No pre processing | Hard       |    56.81 us |  1.11 |
| Default           | Hard       |    51.20 us |  1.00 |
| All               | Hard       |    65.54 us |  1.28 |
|                   |            |             |       |
| Reference         | Medium     | 1,122.61 us | 21.66 |
| No pre processing | Medium     |    52.14 us |  1.01 |
| Default           | Medium     |    51.87 us |  1.00 |
| All               | Medium     |    65.18 us |  1.26 |
|                   |            |             |       |
| Reference         | Easy       | 1,145.31 us | 22.21 |
| No pre processing | Easy       |    53.37 us |  1.04 |
| Default           | Easy       |    51.61 us |  1.00 |
| Pairs             | Easy       |    52.95 us |  1.03 |
