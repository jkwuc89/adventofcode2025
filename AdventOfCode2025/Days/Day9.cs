using System;
using System.Collections.Generic;

namespace AdventOfCode2025.Days;

public class Day9 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = input.Trim().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return "0";
        }

        // Parse coordinates
        var redTiles = new List<(int X, int Y)>();
        foreach (var line in lines)
        {
            var parts = line.Trim().Split(',');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y))
            {
                redTiles.Add((x, y));
            }
        }

        if (redTiles.Count < 2)
        {
            return "0";
        }

        // Find maximum rectangle area
        long maxArea = 0;
        for (int i = 0; i < redTiles.Count; i++)
        {
            for (int j = i + 1; j < redTiles.Count; j++)
            {
                var tile1 = redTiles[i];
                var tile2 = redTiles[j];

                // Check if they can form opposite corners (different x and different y)
                if (tile1.X != tile2.X && tile1.Y != tile2.Y)
                {
                    // Calculate area: number of tiles (inclusive of both corners)
                    long width = Math.Abs((long)tile2.X - tile1.X) + 1;
                    long height = Math.Abs((long)tile2.Y - tile1.Y) + 1;
                    long area = width * height;
                    if (area > maxArea)
                    {
                        maxArea = area;
                    }
                }
            }
        }

        return maxArea.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        return "0";
    }
}
