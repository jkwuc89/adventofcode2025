---
name: day10-puzzle2
overview: Implement Day 10 puzzle 2 using project patterns, add tests, and refactor shared parsing/solver helpers.
todos:
  - id: parse-shared
    content: Add shared parsing incl. joltage targets
    status: completed
  - id: solver-p2
    content: Implement integer solver and puzzle2 entry
    status: completed
  - id: tests
    content: Add puzzle2 sample/real tests and factory check
    status: completed
---

# Plan for Day 10 Puzzle 2 using GPT-5.1 Codex Max model

1) Parsing consolidation

- Extend Day10 parsing to also read joltage targets from `{}` while keeping light/button parsing for puzzle 1. Factor shared parsing helpers (e.g., `ParseMachine` returning lights/buttons/jolts) to avoid duplication between puzzles.

2) Joltage solver algorithm

- For each machine, build coefficient matrix (#counters x #buttons) with 0/1 entries and target vector from joltage requirements.
- Perform exact Gaussian elimination over rationals to derive pivot/free variables (reuseable fraction helper to avoid double rounding).
- Enumerate bounded non-negative integer assignments for free variables, compute pivot variables from reduced system, and track the minimal total presses; detect impossibility if no feasible solution. Keep counts in `long`/`int` and prune with simple lower bounds (e.g., remaining max requirement) to keep search small.

3) Wire puzzle entry points

- Implement `SolvePuzzle2` to parse input lines, solve each machine with the new solver, sum minimal presses, and return as string. Ensure puzzle1 continues to use shared parsing helpers.

4) Tests and factory coverage

- Add puzzle 2 sample test (expected total 33) and real-input test mirroring puzzle1 style in `AdventOfCode2025.Tests/Days/Day10Tests.cs`.
- Verify `PuzzleFactoryTests` covers day 10 (update if needed).
- Refactor any duplicated helper code between puzzles into private methods to satisfy StyleCop and keep Day10 concise.
