using System.IO;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day8Tests
{
    private const string SampleInput = @"162,817,812
57,618,57
906,360,560
592,479,940
352,342,300
466,668,158
542,29,236
431,825,988
739,650,466
52,470,668
216,146,977
819,987,18
117,168,530
805,96,715
346,949,466
970,615,88
941,993,340
862,61,35
984,92,344
425,690,689";

    private readonly Day8 _day8 = new Day8();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("40", _day8.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle1_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day8input.txt"));

        Assert.Equal("79056", _day8.SolvePuzzle1(input));
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("25272", _day8.SolvePuzzle2(SampleInput));
    }

    [Fact]
    public void Puzzle2_WithInputFile_ShouldReturnExpectedResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day8input.txt"));

        Assert.Equal("4639477", _day8.SolvePuzzle2(input));
    }
}
