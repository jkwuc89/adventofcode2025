---
name: Day 5 Puzzle 2 Implementation
overview: Implement Day 5 Puzzle 2: Count all unique ingredient IDs that fall within any fresh ingredient ID range, ignoring the available ingredient IDs section.
todos:
  - id: implement-solvepuzzle2
    content: Implement SolvePuzzle2 method in Day5.cs that counts unique ingredient IDs across all ranges
    status: completed
  - id: add-puzzle2-tests
    content: Add Puzzle2_WithSampleInput_ShouldReturnExpectedResult and Puzzle2_WithInputFile_ShouldReturnExpectedResult tests
    status: completed
---

# Day 5 Puzzle 2 Implementation

## Overview

Implement Day 5 Puzzle 2: Count all unique ingredient IDs that are considered fresh according to the fresh ingredient ID ranges. Unlike Puzzle 1, we ignore the available ingredient IDs section and count all unique IDs that fall within any range.

## Implementation Steps

### 1. Implement SolvePuzzle2 in Day5.cs

Update [`AdventOfCode2025/Days/Day5.cs`](AdventOfCode2025/Days/Day5.cs) to:

- Implement `SolvePuzzle2` method that:
  1. Parses only the ranges section (first part before blank line)
  2. Merges overlapping and adjacent ranges using interval merging algorithm
  3. Sums up the sizes of the merged ranges to get total unique IDs
  4. Returns the count as a string

**Algorithm:**
1. Split input by blank line to get ranges section (only need first part)
2. Parse ranges using existing `ParseRanges` method
3. Sort ranges by start value
4. Merge overlapping/adjacent ranges by iterating through sorted ranges
5. Sum up the sizes of merged ranges: (end - start + 1) for each merged range
6. Return total count

### 2. Add Tests for Puzzle 2

Update [`AdventOfCode2025.Tests/Days/Day5Tests.cs`](AdventOfCode2025.Tests/Days/Day5Tests.cs) to:

- Add `Puzzle2_WithSampleInput_ShouldReturnExpectedResult` test:
  - Uses the same `SampleInput` constant
  - Expects "14" (from the example: 3, 4, 5, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20)
- Add `Puzzle2_WithInputFile_ShouldReturnExpectedResult` test:
  - Reads from `day5input.txt`
  - Expects "345755049374932"

## Files to Modify

- **Modify**: [`AdventOfCode2025/Days/Day5.cs`](AdventOfCode2025/Days/Day5.cs) - Implement `SolvePuzzle2` method with interval merging
- **Modify**: [`AdventOfCode2025.Tests/Days/Day5Tests.cs`](AdventOfCode2025.Tests/Days/Day5Tests.cs) - Add Puzzle 2 tests

## Notes

- Use interval merging algorithm instead of HashSet to handle extremely large ranges efficiently
- Ranges are inclusive (both start and end are included)
- Adjacent ranges (end + 1 == start) should be merged
- Only parse the ranges section (first part before blank line), ignore the ingredient IDs section
- All code must conform to StyleCop rules
