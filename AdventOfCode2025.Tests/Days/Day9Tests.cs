using System.IO;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day9Tests
{
    private const string SampleInput = @"7,1
11,1
11,7
9,7
9,5
2,5
2,3
7,3";

    private readonly Day9 _day9 = new Day9();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("50", _day9.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle1_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day9input.txt"));

        Assert.Equal("4755064176", _day9.SolvePuzzle1(input));
    }
}
