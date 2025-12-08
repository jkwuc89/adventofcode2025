# Advent of Code 2025

A .NET 10 console application for solving [Advent of Code 2025](https://adventofcode.com/2025) puzzles.

This project provides a structured approach to solving daily Advent of Code puzzles, with separate classes for each day, unit tests using sample inputs, and a command-line interface for running solutions.

## What This Project Does

This project implements solutions for the Advent of Code 2025 puzzles. Each day has two puzzles, and the second puzzle is unlocked after completing the first. The project structure includes:

- Separate classes for each day's puzzles in the `Days` folder
- Unit tests for each puzzle using the sample input from the puzzle description
- A command-line interface to run specific day/puzzle combinations
- Input files organized in the `input` directory

For more information about Advent of Code, visit [https://adventofcode.com/2025](https://adventofcode.com/2025).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

To verify your installation, run:
```bash
dotnet --version
```

## Building the Project

To build the solution:

```bash
dotnet build
```

This will build both the main console application and the test project.

## Running Tests

To run all tests:

```bash
dotnet test
```

To run tests for a specific project:

```bash
dotnet test AdventOfCode2025.Tests
```

To run tests for a particular puzzle day:

```bash
dotnet test --filter "FullyQualifiedName~Day<day>"
```

Replace `<day>` with the day number. For example, to run tests for Day 1 (which will run all tests in `Day1Tests.cs`):

```bash
dotnet test --filter "FullyQualifiedName~Day1"
```

This will run both `Puzzle1_WithSampleInput_ShouldReturnExpectedResult` and `Puzzle2_WithSampleInput_ShouldReturnExpectedResult` tests from the `Day1Tests` class.

## Running from the CLI

The application accepts two command-line arguments: the day number (1-25) and the puzzle number (1 or 2).

### Basic Usage

```bash
dotnet run --project AdventOfCode2025 -- <day> <puzzle>
```

### Examples

Run Day 1, Puzzle 1:
```bash
dotnet run --project AdventOfCode2025 -- 1 1
```

Run Day 1, Puzzle 2:
```bash
dotnet run --project AdventOfCode2025 -- 1 2
```

### Input Files

The application reads input from files in the `AdventOfCode2025/input` directory. Input files follow the naming pattern:
- `day1puzzle1input.txt`
- `day1puzzle2input.txt`
- `day2puzzle1input.txt`
- etc.

Make sure to add your puzzle input files to the `input` directory before running a solution.

### Error Handling

The application validates:
- Day number must be between 1 and 25
- Puzzle number must be 1 or 2
- Input file must exist
- Day must be implemented

If any validation fails, an error message will be displayed and the application will exit with a non-zero exit code.

