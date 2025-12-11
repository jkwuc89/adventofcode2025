# Day 10 Puzzle 1 Implementation

## Problem Analysis
Each machine has:
- Indicator lights (initially all off): `.` = off, `#` = on
- Buttons that toggle specific lights (0-indexed)
- Goal: Find minimum total button presses to match the target pattern

Since toggling is XOR and pressing a button twice has no effect, we only need to press each button 0 or 1 time. This is a linear system over GF(2) where:
- Each light's target state = XOR of all button presses affecting it
- We solve using Gaussian elimination to find which buttons to press

## Implementation Steps

### 1. Create Day10.cs
**File**: `AdventOfCode2025/Days/Day10.cs`

- Implement `IPuzzle` interface
- `SolvePuzzle1`: Parse each machine line, solve for minimum presses, sum across all machines
- `SolvePuzzle2`: Return placeholder (e.g., "0" or empty string)

**Parsing**:
- Extract indicator pattern from `[brackets]`
- Extract button wiring from `(parentheses)` - multiple buttons per line
- Ignore joltage requirements in `{braces}`

**Algorithm**:
- For each machine:
  - Build system of equations: `target[i] = XOR(buttons affecting light i)`
  - Use Gaussian elimination over GF(2) to solve
  - Count number of buttons that need to be pressed (1 time each)
  - Sum total presses across all machines

**Helper methods**:
- `ParseMachine`: Parse one line into target pattern and button list
- `SolveMachine`: Solve one machine using Gaussian elimination, return minimum presses
- `GaussianElimination`: Perform elimination over GF(2), return solution vector

### 2. Create Day10Tests.cs
**File**: `AdventOfCode2025.Tests/Days/Day10Tests.cs`

- Test with the three sample machines from the problem:
  - Machine 1: `[.##.]` should return 2 presses
  - Machine 2: `[...#.]` should return 3 presses  
  - Machine 3: `[.###.#]` should return 2 presses
  - Total: 7 presses
- Test with actual input file `day10input.txt`
- Follow existing test patterns (private readonly field, Fact attributes)

### 3. Update PuzzleFactoryTests.cs
**File**: `AdventOfCode2025.Tests/PuzzleFactoryTests.cs`

- Update `GetPuzzleInstance_WithAllImplementedDays_ShouldReturnInstances`:
  - Change loop from `day <= 9` to `day <= 10`

## Technical Details

**Gaussian Elimination over GF(2)**:
- Represent system as augmented matrix
- Each row = one light's equation
- Each column = one button
- Right-hand side = target state (1 if light should be on, 0 if off)
- Operations: row swaps, row additions (XOR)
- Back-substitution to find solution
- If system is inconsistent, return 0 (shouldn't happen for valid inputs)

**Data Structures**:
- Use `bool[]` or `BitArray` for efficient GF(2) operations
- Or use `List<List<bool>>` for matrix representation

## Files to Create/Modify

1. **Create**: `AdventOfCode2025/Days/Day10.cs`
2. **Create**: `AdventOfCode2025.Tests/Days/Day10Tests.cs`
3. **Modify**: `AdventOfCode2025.Tests/PuzzleFactoryTests.cs` (line 30: change `day <= 9` to `day <= 10`)

## StyleCop Compliance
- No doc comments for class/methods (per user request)
- Use 4-space indentation
- Place using directives outside namespace
- System using directives first
- Follow existing code style patterns
