---
name: Day 8 Puzzle 1 Implementation
overview: Implement Day 8 Puzzle 1 solution that connects junction boxes by shortest distances using Union-Find, then calculates the product of the three largest circuit sizes after 1000 connections.
todos:
  - id: create-day8-class
    content: Create Day8.cs with IPuzzle implementation, including SolvePuzzle1 method with coordinate parsing, distance calculation, Union-Find structure, and circuit size calculation
    status: completed
  - id: implement-union-find
    content: Implement Union-Find (Disjoint Set Union) data structure with path compression and union by rank for efficient circuit tracking
    status: completed
  - id: implement-distance-calculation
    content: Implement Euclidean distance calculation for 3D coordinates: sqrt((x1-x2)² + (y1-y2)² + (z1-z2)²)
    status: completed
  - id: implement-main-algorithm
    content: Implement main algorithm: parse coordinates, calculate all pairwise distances, sort by distance, connect pairs using Union-Find, count circuit sizes, multiply top 3
    status: completed
  - id: create-day8-tests
    content: Create Day8Tests.cs with sample input test (20 boxes, 10 connections → 40) and file input test (1000 connections)
    status: completed
  - id: verify-stylecop
    content: Ensure all code conforms to StyleCop rules with proper XML documentation
    status: completed
---

# Day 8 Puzzle 1 Implementation

## Overview
Implement a solution that:
1. Parses 3D coordinates (X,Y,Z) from input
2. Calculates Euclidean distances between all pairs of junction boxes
3. Sorts pairs by distance (shortest first)
4. Uses Union-Find (Disjoint Set Union) to track connected circuits
5. Connects pairs sequentially (only if not already in same circuit)
6. After 1000 connections, finds the three largest circuits and multiplies their sizes

## Implementation Details

### Files Created

1. **`AdventOfCode2025/Days/Day8.cs`**
   - Implements `IPuzzle` interface
   - `SolvePuzzle1(string input)`: Main solution method
   - `SolvePuzzle2(string input)`: Returns "0" for now (puzzle 2 not yet implemented)
   - Helper methods:
     - Parse coordinates from input lines
     - Calculate Euclidean distance: `sqrt((x1-x2)² + (y1-y2)² + (z1-z2)²)`
     - Union-Find data structure for tracking circuits
     - Count circuit sizes after connections

2. **`AdventOfCode2025.Tests/Days/Day8Tests.cs`**
   - `Puzzle1_WithSampleInput_ShouldReturnExpectedResult()`: Test with sample input (20 junction boxes, 10 connections → result 40)
   - `Puzzle1_WithInputFile_ShouldReturnExpectedResult()`: Test with actual input file `day8input.txt` (1000 connections, verify expected result)

### Algorithm

1. **Parse Input**: Split input into lines, parse each line as "X,Y,Z"
2. **Calculate Distances**: For all pairs (i, j) where i < j, calculate Euclidean distance
3. **Sort Pairs**: Sort all pairs by distance (ascending)
4. **Union-Find Setup**: Initialize Union-Find with one element per junction box
5. **Connect Pairs**: Iterate through sorted pairs:
   - Process pairs in order until target number reached (10 for sample with 20 boxes, 1000 for full input)
   - Skip if already in same circuit (Find(i) == Find(j))
   - Otherwise, Union(i, j)
6. **Count Circuit Sizes**: After connections, count size of each circuit
7. **Find Top 3**: Get three largest circuit sizes and multiply them

### Union-Find Implementation
- Use path compression and union by rank for efficiency
- Track parent and rank for each element
- Find operation with path compression
- Union operation with rank optimization

### Sample Input Validation
From the problem description:
- 20 junction boxes
- After 10 shortest connections: circuits of sizes 5, 4, 2, 2, and 7 circuits of size 1
- Three largest: 5, 4, 2 → product = 40

### Key Implementation Notes

- The algorithm processes pairs in sorted order (by distance), skipping pairs that are already in the same circuit
- After processing the target number of pairs (10 for sample, 1000 for full input), it counts circuit sizes and multiplies the three largest
- The solution correctly handles both the sample case (10 connections) and the full input case (1000 connections)
- All code conforms to StyleCop rules with proper XML documentation

### Code Style
- Follow StyleCop rules (XML documentation for public methods)
- Use 4-space indentation
- Place using directives outside namespace
- System using directives first
