using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests.Days;

public class Day1Tests
{
    private readonly Day1 _day1 = new();

    [Fact]
    public void Puzzle1_WithSampleInput_ShouldReturnExpectedResult()
    {
        // Arrange
        string input = _day1.SampleInput;

        // Act
        string result = _day1.SolvePuzzle1(input);

        // Assert
        // TODO: Update expected value once puzzle solution is implemented
        // Expected result from puzzle description: 3
        Assert.NotNull(result);
    }

    [Fact]
    public void Puzzle2_WithSampleInput_ShouldReturnExpectedResult()
    {
        // Arrange
        string input = _day1.SampleInput;

        // Act
        string result = _day1.SolvePuzzle2(input);

        // Assert
        // TODO: Update expected value once puzzle solution is implemented
        Assert.NotNull(result);
    }
}

