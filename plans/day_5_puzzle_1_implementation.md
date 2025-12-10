---
name: Day 5 Puzzle 1 Implementation
overview: Implement Day 5 Puzzle 1 solution following project patterns: create Day5 class implementing IPuzzle, add unit tests, and update Program.cs to include Day 5.
todos:
  - id: create-day5-class
    content: Create Day5.cs class implementing IPuzzle with SolvePuzzle1 method that counts fresh ingredient IDs
    status: completed
  - id: create-day5-tests
    content: Create Day5Tests.cs with sample input test and input file test for Puzzle 1
    status: completed
  - id: update-program-cs
    content: Update Program.cs switch statement to include Day 5
    status: completed
---

# Day 5 Puzzle 1 Implementation

## Overview

Implement Day 5 Puzzle 1: Count how many available ingredient IDs are fresh (fall within at least one range). The input consists of fresh ingredient ID ranges, a blank line, and a list of available ingredient IDs.

## Implementation Steps

### 1. Create Day5 Class

Create [`AdventOfCode2025/Days/Day5.cs`](AdventOfCode2025/Days/Day5.cs) that:

- Implements `IPuzzle` interface
- Parses input into ranges section and ingredient IDs section (split by blank line)
- Parses ranges in format "start-end" into (start, end) tuples
- Parses ingredient IDs (one per line after blank line)
- For Puzzle 1: Counts how many ingredient IDs fall within at least one range
- Returns count as string

**Algorithm:**
1. Split input by blank line to separate ranges from ingredient IDs
2. Parse ranges: split by newline, parse each "start-end" into tuple
3. Parse ingredient IDs: split by newline, parse each as integer
4. For each ingredient ID, check if it falls within any range (inclusive)
5. Count and return fresh ingredient IDs

### 2. Create Day5Tests Class

Create [`AdventOfCode2025.Tests/Days/Day5Tests.cs`](AdventOfCode2025.Tests/Days/Day5Tests.cs) that:

- Follows the same pattern as other test classes
- Includes `SampleInput` constant with the example from puzzle description:
  ```
  3-5
  10-14
  16-20
  12-18

  1
  5
  8
  11
  17
  32
  ```
- Includes `_day5` field initialized with `new Day5()`
- Implements `Puzzle1_WithSampleInput_ShouldReturnExpectedResult` test expecting "3"
- Implements `Puzzle1_WithInputFile_ShouldReturnExpectedResult` test reading from `day5input.txt`

### 3. Update Program.cs

Update [`AdventOfCode2025/Program.cs`](AdventOfCode2025/Program.cs) to:

- Add `5 => new Day5(),` to the switch statement in `GetPuzzleInstance` method

## Files to Create/Modify

- **Create**: [`AdventOfCode2025/Days/Day5.cs`](AdventOfCode2025/Days/Day5.cs) - Day 5 puzzle implementation
- **Create**: [`AdventOfCode2025.Tests/Days/Day5Tests.cs`](AdventOfCode2025.Tests/Days/Day5Tests.cs) - Day 5 unit tests
- **Modify**: [`AdventOfCode2025/Program.cs`](AdventOfCode2025/Program.cs) - Add Day 5 to switch statement

## Notes

- All code must conform to StyleCop rules (braces for all if statements, proper spacing, file ends with single newline, etc.)
- Use `string.Split()` with appropriate separators to parse input sections
- Ranges are inclusive (start and end are both included)
- An ingredient ID is fresh if it falls within ANY range (ranges can overlap)
- For Puzzle 1, only implement `SolvePuzzle1`; `SolvePuzzle2` can return empty string or throw `NotImplementedException` for now
