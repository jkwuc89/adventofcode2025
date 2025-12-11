using System.Linq;

namespace AdventOfCode2025.Tests;

public class DayUtilsTests
{
    [Fact]
    public void SplitNonEmptyLines_ShouldTrimAndSkipEmpty()
    {
        string input = "\n  a  \r\n\nb\r\n  \n";

        var lines = DayUtils.SplitNonEmptyLines(input);

        Assert.Equal(new[] { "a", "b" }, lines);
    }

    [Fact]
    public void SplitLinesPreserveWhitespace_ShouldKeepInnerSpaces()
    {
        string input = " a  \n b ";

        var lines = DayUtils.SplitLinesPreserveWhitespace(input);

        Assert.Equal(new[] { " a  ", " b " }, lines);
    }

    [Fact]
    public void ParseIntLines_ShouldYieldValidIntegers()
    {
        var lines = new[] { "1", " 2 ", "abc", "3" };

        var values = DayUtils.ParseIntLines(lines).ToArray();

        Assert.Equal(new[] { 1, 2, 3 }, values);
    }
}
