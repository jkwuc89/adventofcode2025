---
name: Day 8 Code Refactoring
overview: Refactor Day 8 solutions to extract common code (coordinate parsing, distance calculation, sorting) into reusable helper methods to eliminate duplication between SolvePuzzle1 and SolvePuzzle2.
todos:
  - id: extract-parse-coordinates
    content: Create ParseCoordinates helper method to extract coordinate parsing logic
    status: pending
  - id: extract-distance-calculation
    content: Create CalculateAllPairwiseDistances helper method that calculates and sorts all pairwise distances
    status: pending
  - id: extract-circuit-sizes
    content: Create GetCircuitSizes helper method to count circuit sizes
    status: pending
  - id: refactor-solvepuzzle1
    content: Refactor SolvePuzzle1 to use the new helper methods
    status: pending
  - id: refactor-solvepuzzle2
    content: Refactor SolvePuzzle2 to use the new helper methods
    status: pending
  - id: verify-tests
    content: Run all tests to ensure refactoring didn't break anything
    status: pending
---

# Day 8 Code Refactoring

## Overview
Extract common code from `SolvePuzzle1` and `SolvePuzzle2` into reusable helper methods to eliminate duplication and improve maintainability.

## Common Code Identified

Both methods share:
1. **Coordinate Parsing**: Parsing input lines into list of (X, Y, Z) tuples
2. **Distance Calculation**: Calculating all pairwise Euclidean distances
3. **Distance Sorting**: Sorting pairs by distance (shortest first)
4. **Union-Find Initialization**: Creating Union-Find structure

## Refactoring Plan

### Helper Methods to Create

1. **`ParseCoordinates(string input)`**
   - Takes input string
   - Returns `List<(int X, int Y, int Z)>`
   - Handles empty input validation
   - Parses lines in format "X,Y,Z"

2. **`CalculateAllPairwiseDistances(List<(int X, int Y, int Z)> boxes)`**
   - Takes list of boxes
   - Returns `List<(int Box1, int Box2, double Distance)>` sorted by distance (ascending)
   - Calculates all pairs where Box1 < Box2
   - Sorts results by distance

3. **`GetCircuitSizes(UnionFind unionFind, int boxCount)`**
   - Takes Union-Find structure and box count
   - Returns `Dictionary<int, int>` mapping root to circuit size
   - Used by Puzzle 1 to count circuit sizes

### Files to Modify

1. **`AdventOfCode2025/Days/Day8.cs`**
   - Extract coordinate parsing into `ParseCoordinates` method
   - Extract distance calculation and sorting into `CalculateAllPairwiseDistances` method
   - Extract circuit size counting into `GetCircuitSizes` method
   - Refactor `SolvePuzzle1` to use helper methods
   - Refactor `SolvePuzzle2` to use helper methods

### Benefits

- Eliminates code duplication
- Improves maintainability
- Makes code more testable
- Reduces chance of bugs from inconsistent implementations
