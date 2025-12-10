using System;
using System.IO;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day2Tests
{
    private const string SampleInput = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,
1698522-1698528,446443-446449,38593856-38593862,565653-565659,
824824821-824824827,2121212118-2121212124";

    private readonly Day2 _day2 = new Day2();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("1227775554", _day2.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle1_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day2input.txt"));

        Assert.Equal("64215794229", _day2.SolvePuzzle1(input));
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("4174379265", _day2.SolvePuzzle2(SampleInput));
    }

    [Fact]
    public void Puzzle2_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day2input.txt"));

        Assert.Equal("85513235135", _day2.SolvePuzzle2(input));
    }
}
