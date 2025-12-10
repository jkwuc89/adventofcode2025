using AdventOfCode2025;
using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests;

public class PuzzleFactoryTests
{
    [Fact]
    public void GetPuzzleInstance_WithValidDay_ShouldReturnCorrectInstance()
    {
        var day1 = PuzzleFactory.GetPuzzleInstance(1);
        Assert.NotNull(day1);
        Assert.IsType<Day1>(day1);

        var day6 = PuzzleFactory.GetPuzzleInstance(6);
        Assert.NotNull(day6);
        Assert.IsType<Day6>(day6);
    }

    [Fact]
    public void GetPuzzleInstance_WithInvalidDay_ShouldReturnNull()
    {
        Assert.Null(PuzzleFactory.GetPuzzleInstance(0));
        Assert.Null(PuzzleFactory.GetPuzzleInstance(26));
    }

    [Fact]
    public void GetPuzzleInstance_WithAllImplementedDays_ShouldReturnInstances()
    {
        for (int day = 1; day <= 6; day++)
        {
            var instance = PuzzleFactory.GetPuzzleInstance(day);
            Assert.NotNull(instance);
            Assert.IsAssignableFrom<IPuzzle>(instance);
        }
    }
}
