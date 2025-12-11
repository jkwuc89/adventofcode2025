---
name: Day 7 Puzzle 2 Implementation
overview: Implement Day 7 Puzzle 2: Count all unique pathways (timelines) from S to any exit at the bottom of the quantum tachyon manifold, where each splitter creates two timelines (left and right paths).
todos:
  - id: implement-solvepuzzle2
    content: Implement SolvePuzzle2 method in Day7.cs using recursive DFS with memoization to count unique pathways
    status: pending
  - id: add-puzzle2-tests
    content: Add Puzzle2_WithSampleInput_ShouldReturnExpectedResult and Puzzle2_WithInputFile_ShouldReturnExpectedResult tests to Day7Tests.cs
    status: pending
---

# Day 7 Puzzle 2 Implementation

## Overview

Implement `SolvePuzzle2` in [`AdventOfCode2025/Days/Day7.cs`](AdventOfCode2025/Days/Day7.cs) to count all unique pathways (timelines) through the quantum tachyon manifold. Unlike Puzzle 1 where beams merge, Puzzle 2 counts every unique path from S to any exit at the bottom.

## Key Differences from Puzzle 1

- **Puzzle 1**: Counts beam splits; beams merge when they reach the same position
- **Puzzle 2**: Counts unique pathways; each path is a separate timeline even if paths end at the same position
- At each splitter, the particle takes both left AND right paths (creating two timelines)
- Need to count all unique paths from S to any bottom exit

## Algorithm

Use recursive DFS (depth-first search) with memoization:

1. **Parse grid** and find starting position 'S' (same as Puzzle 1)

2. **Recursive path counting function** `CountTimelines(row, col, grid, memo)`:
   - **Base case**: If `row >= grid.Count`, return 1 (reached bottom - one timeline)
   - **Base case**: If out of bounds, return 0 (invalid path)
   - **Memoization check**: If `(row, col)` already computed, return memoized value
   - **Current cell logic**:
     - If cell is '^' (splitter):
       - Recursively count timelines from left path: `CountTimelines(row + 1, col - 1, ...)`
       - Recursively count timelines from right path: `CountTimelines(row + 1, col + 1, ...)`
       - Return sum of left + right timelines
     - If cell is '.' or 'S' or '|':
       - Continue downward: `CountTimelines(row + 1, col, ...)`
   - **Memoize result** for `(row, col)` and return

3. **Start recursion** from S position: `CountTimelines(startRow, startCol, grid, memo)`

4. **Return result** as string

## Implementation Details

- Use `Dictionary<(int row, int col), long>` for memoization to cache results
- Handle bounds checking for left/right paths at splitters
- The recursive approach naturally explores all unique paths
- Memoization ensures we don't recompute paths from the same position multiple times

## Testing

Add tests to [`AdventOfCode2025.Tests/Days/Day7Tests.cs`](AdventOfCode2025.Tests/Days/Day7Tests.cs):

- `Puzzle2_WithSampleInput_ShouldReturnExpectedResult` expecting "40"
- `Puzzle2_WithInputFile_ShouldReturnExpectedResult` (run to get expected value)

## Files to Modify

- **Modify**: [`AdventOfCode2025/Days/Day7.cs`](AdventOfCode2025/Days/Day7.cs) - Implement `SolvePuzzle2` method
- **Modify**: [`AdventOfCode2025.Tests/Days/Day7Tests.cs`](AdventOfCode2025.Tests/Days/Day7Tests.cs) - Add Puzzle 2 tests

## Notes

- All code must conform to StyleCop rules
- Use `long` for timeline counts (may be large)
- Memoization key is `(row, col)` position
- Recursive calls handle left/right paths at splitters
- Base case returns 1 when reaching bottom (any column is valid exit)

