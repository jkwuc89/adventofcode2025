using System;
using System.IO;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day4Tests
{
    private const string SampleInput = @"..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.";

    private readonly Day4 _day4 = new Day4();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("13", _day4.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle1_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day4input.txt"));

        Assert.Equal("1523", _day4.SolvePuzzle1(input));
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("43", _day4.SolvePuzzle2(SampleInput));
    }

    [Fact]
    public void Puzzle2_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day4input.txt"));

        Assert.Equal("9290", _day4.SolvePuzzle2(input));
    }
}
