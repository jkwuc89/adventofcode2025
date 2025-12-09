using System;
using System.Collections.Generic;

namespace AdventOfCode2025.Days;

/// <summary>
/// Day 2: Invalid IDs
/// </summary>
public class Day2 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var ranges = ParseRanges(input);
        long sum = 0;

        foreach (var (start, end) in ranges)
        {
            for (long value = start; value <= end; value++)
            {
                if (IsInvalidIdPuzzle1(value))
                    sum += value;
            }
        }

        return sum.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var ranges = ParseRanges(input);
        long sum = 0;

        foreach (var (start, end) in ranges)
        {
            for (long value = start; value <= end; value++)
            {
                if (IsInvalidIdPuzzle2(value))
                    sum += value;
            }
        }

        return sum.ToString();
    }

    private static List<(long start, long end)> ParseRanges(string input)
    {
        var ranges = new List<(long, long)>();
        var parts = input.Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawPart in parts)
        {
            var part = rawPart.Trim();
            if (part.Length == 0)
                continue;

            var dashIndex = part.IndexOf('-');
            if (dashIndex < 0)
                continue;

            var start = long.Parse(part.AsSpan(0, dashIndex));
            var end = long.Parse(part.AsSpan(dashIndex + 1));
            ranges.Add((start, end));
        }

        return ranges;
    }

    private static bool IsInvalidIdPuzzle1(long value)
    {
        var text = value.ToString();
        if ((text.Length & 1) == 1) // odd length cannot be two repeated halves
            return false;

        var halfLength = text.Length / 2;
        var first = text.AsSpan(0, halfLength);
        var second = text.AsSpan(halfLength);

        return first.SequenceEqual(second);
    }

    private static bool IsInvalidIdPuzzle2(long value)
    {
        var text = value.ToString();
        var length = text.Length;

        // Try all possible divisors k >= 2
        for (int k = 2; k <= length; k++)
        {
            if (length % k != 0)
                continue;

            var partLength = length / k;
            var firstPart = text.AsSpan(0, partLength);
            bool allMatch = true;

            // Check if all k parts are identical
            for (int i = 1; i < k; i++)
            {
                var currentPart = text.AsSpan(i * partLength, partLength);
                if (!firstPart.SequenceEqual(currentPart))
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch)
                return true;
        }

        return false;
    }
}

