---
name: day-utils-refactor
overview: Refactor shared Day helpers into DayUtils with tests.
todos:
  - id: discover
    content: Identify shared patterns across Day classes
    status: pending
  - id: utils
    content: Add DayUtils helpers for repeated logic
    status: pending
  - id: refactor-days
    content: Update Days to use DayUtils helpers
    status: pending
  - id: tests
    content: Add DayUtils unit tests and rerun suite
    status: pending
---

# Plan for Day Utilities Refactor

1) Discover common code
- Survey existing Day classes (Day1–Day11) for duplicated helpers (e.g., trimming/splitting input lines, integer parsing, adjacency parsing, range reads, coordinate handling).

2) Design DayUtils
- Add `DayUtils` next to `Program.cs`/`PuzzleFactory.cs` with public static helpers (only for truly repeated patterns) while preserving behavior. Keep signatures simple (e.g., TrimSplitLines, ParseIntsPerLine, BuildAdjacency).

3) Refactor Days
- Replace duplicated logic in Day classes with DayUtils calls without altering puzzle outputs. Focus on obvious shared code only; avoid changing algorithms unless duplication.

4) Tests
- Create `DayUtils` test class validating each new public helper. Ensure existing Day tests still pass.

5) Verify
- Run formatting/StyleCop-compliant build and full test suite to confirm no regressions.
