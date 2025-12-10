---
name: Day 6 Puzzle 2 Implementation
overview: Implement Day 6 Puzzle 2: Parse grid with numbers read right-to-left column by column (most significant digit at top), solve each problem, and return the sum of all problem answers.
todos:
  - id: implement-solvepuzzle2
    content: Implement SolvePuzzle2 method in Day6.cs that reads numbers right-to-left column by column
    status: completed
  - id: add-puzzle2-tests
    content: Add Puzzle2_WithSampleInput_ShouldReturnExpectedResult and Puzzle2_WithInputFile_ShouldReturnExpectedResult tests
    status: completed
---

# Day 6 Puzzle 2 Implementation

## Overview

Implement Day 6 Puzzle 2: Numbers are read right-to-left, column by column, with the most significant digit at the top and least significant digit at the bottom. Each number occupies its own column(s). Problems are still separated by columns of only spaces, and operators are at the bottom.

## Implementation Steps

### 1. Implement SolvePuzzle2 in Day6.cs

Update [`AdventOfCode2025/Days/Day6.cs`](AdventOfCode2025/Days/Day6.cs) to:

- Implement `SolvePuzzle2` method that:
  1. Parses the input grid (similar to Puzzle 1)
  2. Identifies problem boundaries (columns of only spaces)
  3. For each problem, reads numbers right-to-left, column by column:
     - Each column represents one number
     - Read digits from top to bottom (most significant to least significant)
     - Process columns from right to left
  4. Extracts the operator from the bottom row
  5. Calculates each problem's result
  6. Sums all problem results

**Key Difference from Puzzle 1:**
- Each column represents one number (not multiple numbers per column)
- Digits within a column are read top-to-bottom (most significant at top)
- Columns are processed right-to-left
- Numbers are separated naturally by columns

**Algorithm:**
1. Split input into lines and create grid (same as Puzzle 1)
2. Find separator columns (all spaces)
3. Group columns into problems (between separators)
4. For each problem (reading columns right-to-left):
   - For each column, read digits top-to-bottom to form one number
   - Extract operator from bottom row
   - Calculate result and add to grand total

### 2. Add Tests for Puzzle 2

Update [`AdventOfCode2025.Tests/Days/Day6Tests.cs`](AdventOfCode2025.Tests/Days/Day6Tests.cs) to:

- Add `Puzzle2_WithSampleInput_ShouldReturnExpectedResult` test:
  - Uses the same `SampleInput` constant
  - Expects "3263827" (from the example: 1058 + 3253600 + 625 + 8544)
- Add `Puzzle2_WithInputFile_ShouldReturnExpectedResult` test:
  - Reads from `day6input.txt`
  - Expects "7858808482092"

## Files to Modify

- **Modify**: [`AdventOfCode2025/Days/Day6.cs`](AdventOfCode2025/Days/Day6.cs) - Implement `SolvePuzzle2` method
- **Modify**: [`AdventOfCode2025.Tests/Days/Day6Tests.cs`](AdventOfCode2025.Tests/Days/Day6Tests.cs) - Add Puzzle 2 tests

## Notes

- Each column represents one number
- Digits within a column are read top-to-bottom (most significant at top)
- Columns are processed right-to-left
- All code must conform to StyleCop rules
