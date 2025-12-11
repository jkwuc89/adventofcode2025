---
name: Day 7 Puzzle 1 Implementation
overview: Implement Day 7 Puzzle 1: Simulate tachyon beam propagation through a grid, counting how many times beams are split when they encounter splitters.
todos:
  - id: create-day7-class
    content: Create Day7.cs class implementing IPuzzle with SolvePuzzle1 method that simulates beam propagation and counts splits
    status: completed
  - id: create-day7-tests
    content: Create Day7Tests.cs with sample input test and input file test for Puzzle 1
    status: completed
---

# Day 7 Puzzle 1 Implementation

## Overview

Implement Day 7 Puzzle 1: Simulate tachyon beam propagation through a manifold grid. Beams start at 'S' and move downward. When a beam hits a splitter '^', it stops and two new beams are emitted (left and right). Count the total number of splits.

## Implementation Steps

### 1. Create Day7 Class

Create [`AdventOfCode2025/Days/Day7.cs`](AdventOfCode2025/Days/Day7.cs) that:

- Implements `IPuzzle` interface
- Parses the input grid (find 'S' starting position)
- Simulates beam propagation:
  - Start with one beam at 'S' position, moving downward
  - Track active beam positions and directions
  - For each step, move all active beams downward
  - When a beam hits a splitter '^':
    - Stop the current beam
    - Add two new beams: one to the left, one to the right (both moving down)
    - Increment split counter
  - Continue until all beams exit the grid or hit splitters
- Returns the total split count as a string

**Algorithm:**
1. Parse grid, find 'S' position
2. Initialize: active beams queue with starting position
3. Use a set to track processed positions (to avoid duplicate processing and handle beam merging)
4. For each simulation step:
   - Process all active beams in current batch
   - Move each beam one step downward
   - If beam hits '^' splitter:
     - Increment split count
     - Add left beam (same row, col-1)
     - Add right beam (same row, col+1)
   - If beam goes out of bounds, remove it
   - If beam position is already processed, skip (to handle merging)
5. Continue until no active beams remain
6. Return split count

**Key Considerations:**
- Beams merge when multiple beams reach the same position (only process once)
- Use a set to track processed positions to avoid counting splits multiple times
- Beams always move downward
- Splitters stop the incoming beam and emit two new beams left and right

### 2. Create Day7Tests Class

Create [`AdventOfCode2025.Tests/Days/Day7Tests.cs`](AdventOfCode2025.Tests/Days/Day7Tests.cs) that:

- Follows the same pattern as other test classes
- Includes `SampleInput` constant with the example from puzzle description
- Includes `_day7` field initialized with `new Day7()`
- Implements `Puzzle1_WithSampleInput_ShouldReturnExpectedResult` test expecting "21"
- Implements `Puzzle1_WithInputFile_ShouldReturnExpectedResult` test expecting "1560"

### 3. Update Program.cs

The `PuzzleFactory` will automatically discover Day7, so no manual update needed.

## Files to Create/Modify

- **Create**: [`AdventOfCode2025/Days/Day7.cs`](AdventOfCode2025/Days/Day7.cs) - Day 7 puzzle implementation
- **Create**: [`AdventOfCode2025.Tests/Days/Day7Tests.cs`](AdventOfCode2025.Tests/Days/Day7Tests.cs) - Day 7 unit tests

## Notes

- All code must conform to StyleCop rules (braces for all if statements, proper spacing, file ends with single newline, etc.)
- Use a queue to track active beams
- Use a set to track processed positions to handle beam merging
- Beams move one step at a time
- For Puzzle 1, only implement `SolvePuzzle1`; `SolvePuzzle2` can throw `NotImplementedException` for now
