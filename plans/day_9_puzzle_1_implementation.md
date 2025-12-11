---
name: Day 9 Puzzle 1 Implementation
overview: Implement Day 9 Puzzle 1 solution that finds the largest rectangle formed by two red tiles as opposite corners, where the rectangle area is maximized.
todos:
  - id: create-day9-class
    content: Create Day9.cs with IPuzzle implementation, including SolvePuzzle1 method that parses coordinates and finds maximum rectangle area
    status: pending
  - id: implement-coordinate-parsing
    content: Implement coordinate parsing from input (format: X,Y)
    status: pending
  - id: implement-max-rectangle-finding
    content: Implement algorithm to find maximum rectangle area by checking all pairs of red tiles
    status: pending
  - id: create-day9-tests
    content: Create Day9Tests.cs with sample input test (expects 50) and file input test
    status: pending
  - id: update-puzzle-factory-tests
    content: Update PuzzleFactoryTests.cs to include day 9 in the test
    status: pending
---

# Day 9 Puzzle 1 Implementation

## Overview
Implement a solution that:
1. Parses red tile coordinates (X,Y) from input
2. For each pair of red tiles, checks if they can form a rectangle (opposite corners)
3. Calculates the area of each valid rectangle
4. Returns the maximum area

## Implementation Details

### Files to Create/Modify

1. **`AdventOfCode2025/Days/Day9.cs`**
   - Implement `IPuzzle` interface
   - `SolvePuzzle1(string input)`: Main solution method
   - `SolvePuzzle2(string input)`: Return "0" for now
   - Parse coordinates from input (format: "X,Y")
   - Find maximum rectangle area

2. **`AdventOfCode2025.Tests/Days/Day9Tests.cs`**
   - `Puzzle1_WithSampleInput_ShouldReturnExpectedResult()`: Test with sample input (expects 50)
   - `Puzzle1_WithInputFile_ShouldReturnExpectedResult()`: Test with actual input file

3. **`AdventOfCode2025.Tests/PuzzleFactoryTests.cs`**
   - Update `GetPuzzleInstance_WithAllImplementedDays_ShouldReturnInstances` to include day 9

### Algorithm

1. **Parse Input**: Split input into lines, parse each line as "X,Y"
2. **Find Maximum Rectangle**: For each pair of red tiles (i, j) where i < j:
   - Check if they can form opposite corners: `x1 != x2 && y1 != y2`
   - Calculate area: `|x2 - x1| * |y2 - y1|`
   - Track maximum area
3. **Return Result**: Return the maximum area found

### Key Implementation Notes

- Two tiles (x1, y1) and (x2, y2) are opposite corners if:
  - They have different x coordinates AND different y coordinates
  - The rectangle area is: `Math.Abs(x2 - x1) * Math.Abs(y2 - y1)`
- Need to check all pairs of red tiles
- Return maximum area found

### Sample Input Validation
From the problem description:
- Red tiles at: 7,1; 11,1; 11,7; 9,7; 9,5; 2,5; 2,3; 7,3
- Largest rectangle has area 50 (between 2,5 and 11,1)

### Code Style
- No doc comments needed (as per user request)
- Follow StyleCop rules otherwise
- Use 4-space indentation
- Place using directives outside namespace
