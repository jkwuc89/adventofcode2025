---
name: day11-puzzle1
overview: Implement Day 11 puzzle 1 to count directed paths from you to out, add tests, and update factory coverage.
todos:
  - id: day11-logic
    content: Add Day11 puzzle1 logic with DFS path counting
    status: pending
  - id: day11-wire
    content: Wire Day11 into factory/puzzle entry
    status: pending
  - id: day11-tests
    content: Add Day11 tests and update factory test
    status: pending
---

# Plan for Day 11 Puzzle 1

1) Implement puzzle logic
- Add `Day11` class with `SolvePuzzle1` parsing lines like `src: dst1 dst2` into a directed adjacency map.
- Count all distinct paths from `you` to `out` using DFS with memoization to avoid recomputation and detect unreachable graphs; return count as string.
- Handle missing nodes gracefully by treating absent entries as no outgoing edges.

2) Wire into runners
- Ensure `PuzzleFactory` can instantiate Day11 (class in `AdventOfCode2025.Days`).
- Keep `SolvePuzzle2` as a placeholder returning "0" for now.

3) Tests
- Add sample test expecting 5 paths for provided example in `AdventOfCode2025.Tests/Days/Day11Tests.cs`.
- Add input-file test mirroring prior days reading `input/day11input.txt` and asserting non-empty result.
- Update `PuzzleFactoryTests.GetPuzzleInstance_WithAllImplementedDays_ShouldReturnInstances` range to include day 11 if needed.

4) Cleanup/StyleCop
- Refactor any shared parsing/helpers if duplication arises; ensure StyleCop rules (ordering, spacing) are satisfied and no doc comments are added.
