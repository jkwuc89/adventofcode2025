using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2025;

public static class DayUtils
{
    public static string[] SplitNonEmptyLines(string input)
    {
        return input
            .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .ToArray();
    }

    public static string[] SplitLinesPreserveWhitespace(string input)
    {
        return input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static IEnumerable<int> ParseIntLines(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            if (int.TryParse(line, out int value))
            {
                yield return value;
            }
        }
    }
}
