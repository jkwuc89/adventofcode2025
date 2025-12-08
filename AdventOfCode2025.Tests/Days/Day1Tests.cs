using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day1Tests
{
    private readonly Day1 _day1 = new();

    private const string SampleInput = @"L68
L30
R48
L5
R60
L55
L1
L99
R14
L82";

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        Assert.Equal("3", _day1.SolvePuzzle1(SampleInput));
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        // Arrange
        string input = SampleInput;

        // Act
        string result = _day1.SolvePuzzle2(input);

        // Assert
        // TODO: Update expected value once puzzle solution is implemented
        Assert.NotNull(result);
    }
}

