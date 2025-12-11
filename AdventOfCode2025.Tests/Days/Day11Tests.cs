using System.IO;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day11Tests
{
    private const string SampleInput = @"aaa: you hhh
you: bbb ccc
bbb: ddd eee
ccc: ddd eee fff
ddd: ggg
eee: out
fff: out
ggg: out
hhh: ccc fff iii
iii: out";

    private const string SampleInputPuzzle2 = @"svr: aaa bbb
aaa: fft
fft: ccc
bbb: tty
tty: ccc
ccc: ddd eee
ddd: hub
hub: fff
eee: dac
dac: fff
fff: ggg hhh
ggg: out
hhh: out";

    private readonly Day11 _day11 = new Day11();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("5", _day11.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle1_WithInputFile_ShouldReturnResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day11input.txt"));

        Assert.Equal("724", _day11.SolvePuzzle1(input));
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("2", _day11.SolvePuzzle2(SampleInputPuzzle2));
    }

    [Fact]
    public void Puzzle2_WithInputFile_ShouldReturnResult()
    {
        string input = File.ReadAllText(
            Path.Combine("..", "..", "..", "..", "AdventOfCode2025", "input", "day11input.txt"));

        Assert.Equal("473930047491888", _day11.SolvePuzzle2(input));
    }
}
