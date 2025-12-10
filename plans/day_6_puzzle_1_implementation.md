---
name: Day 6 Puzzle 1 Implementation
overview: Implement Day 6 Puzzle 1: Parse a grid of vertically arranged math problems, solve each problem, and return the sum of all problem answers.
todos:
  - id: create-day6-class
    content: Create Day6.cs class implementing IPuzzle with SolvePuzzle1 method that parses grid and solves math problems
    status: completed
  - id: create-day6-tests
    content: Create Day6Tests.cs with sample input test and input file test for Puzzle 1
    status: completed
  - id: update-program-cs
    content: Update Program.cs switch statement to include Day 6
    status: completed
---

# Day 6 Puzzle 1 Implementation

## Overview

Implement Day 6 Puzzle 1: Parse a grid where math problems are arranged vertically. Each problem has numbers stacked in columns with an operator at the bottom. Problems are separated by columns of only spaces. Calculate each problem's result and return the sum of all results.

## Implementation Steps

### 1. Create Day6 Class

Create [`AdventOfCode2025/Days/Day6.cs`](AdventOfCode2025/Days/Day6.cs) that:

- Implements `IPuzzle` interface
- Parses the input grid line by line
- Identifies problem boundaries (columns containing only spaces)
- Groups columns between separators into problems
- For each problem:
  - Extracts numbers from each row (excluding the operator row)
  - Extracts the operator from the last row
  - Calculates the result (apply operator to all numbers)
- Sums all problem results
- Returns the grand total as a string

**Algorithm:**
1. Split input into lines
2. Determine grid width (max line length)
3. For each column index:
   - Check if column is all spaces (problem separator)
   - If not, collect characters from that column across all rows
4. Group consecutive non-separator columns into problems
5. For each problem:
   - Extract numbers from rows (except last row)
   - Extract operator from last row
   - Parse numbers and apply operator sequentially
6. Sum all problem results

**Parsing Details:**
- Problems are separated by columns that are entirely spaces
- Numbers can be left/right aligned (trim whitespace when parsing)
- Operator row is the last row
- Apply operator sequentially: for `*`, multiply all numbers; for `+`, add all numbers

### 2. Create Day6Tests Class

Create [`AdventOfCode2025.Tests/Days/Day6Tests.cs`](AdventOfCode2025.Tests/Days/Day6Tests.cs) that:

- Follows the same pattern as other test classes
- Includes `SampleInput` constant with the example from puzzle description:
  ```
  123 328  51 64 
   45 64  387 23 
    6 98  215 314
  *   +   *   +  
  ```
- Includes `_day6` field initialized with `new Day6()`
- Implements `Puzzle1_WithSampleInput_ShouldReturnExpectedResult` test expecting "4277556"
- Implements `Puzzle1_WithInputFile_ShouldReturnExpectedResult` test expecting "4412382293768"

### 3. Update Program.cs

Update [`AdventOfCode2025/Program.cs`](AdventOfCode2025/Program.cs) to:

- Add `6 => new Day6(),` to the switch statement in `GetPuzzleInstance` method

## Files to Create/Modify

- **Create**: [`AdventOfCode2025/Days/Day6.cs`](AdventOfCode2025/Days/Day6.cs) - Day 6 puzzle implementation
- **Create**: [`AdventOfCode2025.Tests/Days/Day6Tests.cs`](AdventOfCode2025.Tests/Days/Day6Tests.cs) - Day 6 unit tests
- **Modify**: [`AdventOfCode2025/Program.cs`](AdventOfCode2025/Program.cs) - Add Day 6 to switch statement

## Notes

- All code must conform to StyleCop rules (braces for all if statements, proper spacing, file ends with single newline, etc.)
- Handle variable line lengths (pad or use max length)
- Trim whitespace when parsing numbers
- Problems are separated by full columns of spaces
- Apply operators sequentially: `a op b op c` means `((a op b) op c)`
- For Puzzle 1, only implement `SolvePuzzle1`; `SolvePuzzle2` can throw `NotImplementedException` for now
