---
name: Advent of Code 2025 Project Setup

prompt:
I want to create a new .NET console app project to complete the daily Advent of Code puzzles. Two puzzles will be made available on each day; the second puzzle is unlocked when the first is completed. See https://adventofcode.com/2025/day/1 for the first day 1 puzzle.

Please put a plan together to create a new C# .NET console app project. I would like to use separate classes for each day. The sample input for each puzzle will be a hard coded string. The full puzzle input will be a text file which will be named using the following pattern: day#puzzle#input.txt where the first # is the day and the second # is either 1 or 2.

The CLI syntax for the new console app will be:
adventofcode2025 # #
where the first # is the day and the second # is either 1 or 2 for the puzzle number for the specified day. Running this will run the solution for the puzzle for the specified day and puzzle number. It will use the input file described above to run the solution and will output the answer to the console.

The new project will include a unit test for each puzzle that uses the sample input to verify that the puzzle solution is working correctly.

This project will be committed to GitHub using my personal GitHub account.

The new project should not have a proposed solution for the first puzzle. I will use a separate plan for that.

overview: Create a .NET 10 console application with xUnit tests for solving Advent of Code 2025 puzzles. The project will support CLI arguments to run specific day/puzzle combinations, read input from files, and include unit tests with sample inputs.
todos:
  - id: create-solution
    content: Create solution file and project structure (.sln, .csproj files)
    status: pending
  - id: create-core-interface
    content: Create IPuzzle interface or PuzzleBase class for puzzle contract
    status: pending
  - id: create-day1-class
    content: Create Day1 class with empty puzzle methods and sample input
    status: pending
  - id: create-program-cs
    content: Create Program.cs with CLI argument parsing and puzzle execution logic
    status: pending
  - id: create-test-project
    content: Create test project with xUnit and Day1Tests class
    status: pending
  - id: create-input-files
    content: Create placeholder input files (day1puzzle1input.txt, day1puzzle2input.txt)
    status: pending
  - id: create-gitignore
    content: Create .gitignore file for .NET projects
    status: pending
---

# Advent of Code 2025 .NET Project Setup

## Project Structure

Create a solution with two projects:
- **Main console app**: `AdventOfCode2025` - contains puzzle solutions and CLI interface
- **Test project**: `AdventOfCode2025.Tests` - contains xUnit tests for each puzzle

## Files to Create

### Solution and Project Files
- `AdventOfCode2025.sln` - Solution file
- `AdventOfCode2025/AdventOfCode2025.csproj` - Console app project (.NET 10)
- `AdventOfCode2025.Tests/AdventOfCode2025.Tests.csproj` - Test project (.NET 10, xUnit)

### Core Application Files

#### `AdventOfCode2025/Program.cs`
- Parse command-line arguments (day number, puzzle number)
- Validate arguments (day 1-25, puzzle 1-2)
- Read input file from `day#puzzle#input.txt` pattern
- Instantiate appropriate day class and call the puzzle method
- Output the result to console
- Handle errors gracefully

#### `AdventOfCode2025/IPuzzle.cs` (or `PuzzleBase.cs`)
- Interface or base class defining the contract for puzzle solutions
- Methods: `SolvePuzzle1(string input)`, `SolvePuzzle2(string input)`
- Property: `SampleInput` (hard-coded string for each puzzle)

#### `AdventOfCode2025/Days/Day1.cs`
- Implements `IPuzzle` interface
- Contains `SampleInput` property with the example input from the puzzle description
- Contains `SolvePuzzle1(string input)` method (empty implementation - no solution)
- Contains `SolvePuzzle2(string input)` method (empty implementation - no solution)
- Helper method to read input file if needed

### Test Files

#### `AdventOfCode2025.Tests/Days/Day1Tests.cs`
- xUnit test class
- Test method for Puzzle 1 using `SampleInput` from `Day1`
- Test method for Puzzle 2 using `SampleInput` from `Day1`
- Verify expected output (will need expected value once solution is implemented)

### Input Files
- Create `day1puzzle1input.txt` (empty placeholder - user will add actual input)
- Create `day1puzzle2input.txt` (empty placeholder - user will add actual input)

### Additional Files
- `.gitignore` - Standard .NET gitignore patterns
- `README.md` - Project documentation (optional, but helpful)

## Implementation Details

1. **CLI Argument Parsing**: Use `args[0]` for day, `args[1]` for puzzle number
2. **Input File Reading**: Read from `day{day}puzzle{puzzle}input.txt` in the project root or a dedicated `inputs/` folder
3. **Day Class Pattern**: Each day will have its own class in `Days/` namespace
4. **Error Handling**: Validate day (1-25), puzzle (1-2), file existence
5. **Test Structure**: Each day gets a corresponding test class in `Days/` folder

## Project Configuration

- Target framework: .NET 10
- Console app with nullable reference types enabled
- Test project references main project
- xUnit test framework with appropriate test runner

