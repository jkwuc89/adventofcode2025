using System.IO;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day6Tests
{
    private const string SampleInput = @"123 328  51 64
 45 64  387 23
  6 98  215 314
*   +   *   +  ";

    private readonly Day6 _day6 = new Day6();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("4277556", _day6.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle1_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day6input.txt"));

        Assert.Equal("4412382293768", _day6.SolvePuzzle1(input));
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("3263827", _day6.SolvePuzzle2(SampleInput));
    }

    [Fact]
    public void Puzzle2_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day6input.txt"));

        Assert.Equal("7858808482092", _day6.SolvePuzzle2(input));
    }
}
