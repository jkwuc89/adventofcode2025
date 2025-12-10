using System.IO;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day5Tests
{
    private const string SampleInput = @"3-5
10-14
16-20
12-18

1
5
8
11
17
32";

    private readonly Day5 _day5 = new Day5();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("3", _day5.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle1_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day5input.txt"));

        Assert.Equal("761", _day5.SolvePuzzle1(input));
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("14", _day5.SolvePuzzle2(SampleInput));
    }

    [Fact]
    public void Puzzle2_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day5input.txt"));

        Assert.Equal("345755049374932", _day5.SolvePuzzle2(input));
    }
}
