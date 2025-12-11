---
name: day11-puzzle2
overview: Implement Day 11 puzzle 2 counting paths from svr to out that visit dac and fft, add tests, and update factory coverage.
todos:
  - id: d11p2-logic
    content: Add Day11 puzzle2 path counting with dac/fft mask
    status: completed
  - id: d11p2-tests
    content: Add puzzle2 tests (sample & input) and factory update
    status: completed
---

# Plan for Day 11 Puzzle 2

1) Reuse parsing

- Reuse Day11 graph parser from puzzle 1 to build adjacency lists; no input format changes.

2) Path counting with required nodes

- Implement `SolvePuzzle2` to count paths from `svr` to `out` that include both `dac` and `fft` in any order.
- Use DFS with memoization keyed by `(node, mask)` where mask tracks whether `dac` and `fft` have been visited; accumulate counts modulo nothing (use `long`). Handle cycles with a visiting set.

3) Tests

- Add sample test expecting 2 valid paths for the provided example in `Day11Tests`.
- Add input-file test (reads `input/day11input.txt`) asserting non-empty result for puzzle 2.
- Update `PuzzleFactoryTests.GetPuzzleInstance_WithAllImplementedDays_ShouldReturnInstances` to include day 11 if needed.

4) Cleanup/StyleCop

- Refactor shared helpers in Day11 if needed to avoid duplication, ensure ordering/spacing per StyleCop, and keep `SolvePuzzle1` behavior intact.
