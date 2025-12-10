using System;
using System.Collections.Generic;

namespace AdventOfCode2025.Days;

public class Day5 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var parts = input.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.None);
        if (parts.Length < 2)
        {
            return "0";
        }

        var rangesSection = parts[0].Trim();
        var idsSection = parts[1].Trim();

        var ranges = ParseRanges(rangesSection);
        var ingredientIds = ParseIngredientIds(idsSection);

        int freshCount = 0;
        foreach (var id in ingredientIds)
        {
            if (IsFresh(id, ranges))
            {
                freshCount++;
            }
        }

        return freshCount.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var parts = input.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.None);
        var rangesSection = parts.Length > 0 ? parts[0].Trim() : input.Trim();

        var ranges = ParseRanges(rangesSection);
        var mergedRanges = MergeRanges(ranges);

        long totalCount = 0;
        foreach (var (start, end) in mergedRanges)
        {
            totalCount += (end - start) + 1;
        }

        return totalCount.ToString();
    }

    private static List<(long start, long end)> MergeRanges(List<(long start, long end)> ranges)
    {
        if (ranges.Count == 0)
        {
            return new List<(long, long)>();
        }

        // Sort ranges by start value
        var sortedRanges = new List<(long start, long end)>(ranges);
        sortedRanges.Sort((a, b) => a.start.CompareTo(b.start));

        var merged = new List<(long start, long end)>();
        var current = sortedRanges[0];

        for (int i = 1; i < sortedRanges.Count; i++)
        {
            var next = sortedRanges[i];

            // If current and next overlap or are adjacent, merge them
            if (next.start <= current.end + 1)
            {
                // Merge: extend current range to include next
                current = (current.start, Math.Max(current.end, next.end));
            }
            else
            {
                // No overlap: add current to merged list and start new range
                merged.Add(current);
                current = next;
            }
        }

        // Add the last range
        merged.Add(current);

        return merged;
    }

    private static List<(long start, long end)> ParseRanges(string rangesSection)
    {
        var ranges = new List<(long, long)>();
        var lines = rangesSection.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.Length == 0)
            {
                continue;
            }

            var dashIndex = trimmedLine.IndexOf('-');
            if (dashIndex < 0)
            {
                continue;
            }

            var start = long.Parse(trimmedLine.AsSpan(0, dashIndex));
            var end = long.Parse(trimmedLine.AsSpan(dashIndex + 1));
            ranges.Add((start, end));
        }

        return ranges;
    }

    private static List<long> ParseIngredientIds(string idsSection)
    {
        var ids = new List<long>();
        var lines = idsSection.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.Length == 0)
            {
                continue;
            }

            var id = long.Parse(trimmedLine);
            ids.Add(id);
        }

        return ids;
    }

    private static bool IsFresh(long id, List<(long start, long end)> ranges)
    {
        foreach (var (start, end) in ranges)
        {
            if (id >= start && id <= end)
            {
                return true;
            }
        }

        return false;
    }
}
