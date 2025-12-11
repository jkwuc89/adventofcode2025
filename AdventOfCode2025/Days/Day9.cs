using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025;

namespace AdventOfCode2025.Days;

public class Day9 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);
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
        var points = ParsePoints(input);
        if (points.Count < 2)
        {
            return "0";
        }

        var polygon = points;

        // Coordinate compression breakpoints (tile boundaries).
        var xs = new SortedSet<long>();
        var ys = new SortedSet<long>();
        foreach (var (x, y) in points)
        {
            xs.Add(x);
            xs.Add(x + 1);
            ys.Add(y);
            ys.Add(y + 1);
        }

        var xList = xs.ToList();
        var yList = ys.ToList();
        var xIndex = xList
            .Select((v, i) => (v, i))
            .ToDictionary(t => t.v, t => t.i);
        var yIndex = yList
            .Select((v, i) => (v, i))
            .ToDictionary(t => t.v, t => t.i);

        int xCells = xList.Count - 1;
        int yCells = yList.Count - 1;
        var green = new bool[xCells, yCells];

        // Mark boundary tiles (green lines between consecutive red tiles).
        for (int i = 0; i < points.Count; i++)
        {
            var a = points[i];
            var b = points[(i + 1) % points.Count];
            if (a.X == b.X)
            {
                long x = a.X;
                int xi = xIndex[x];
                int yStart = Math.Min(yIndex[a.Y], yIndex[b.Y]);
                int yEnd = Math.Max(yIndex[a.Y], yIndex[b.Y]);
                for (int yi = yStart; yi <= yEnd; yi++)
                {
                    green[xi, yi] = true;
                }
            }
            else if (a.Y == b.Y)
            {
                long y = a.Y;
                int yi = yIndex[y];
                int xStart = Math.Min(xIndex[a.X], xIndex[b.X]);
                int xEnd = Math.Max(xIndex[a.X], xIndex[b.X]);
                for (int xi = xStart; xi <= xEnd; xi++)
                {
                    green[xi, yi] = true;
                }
            }
        }

        // Mark interior tiles using point-in-polygon on cell midpoints.
        for (int xi = 0; xi < xCells; xi++)
        {
            double midX = (xList[xi] + xList[xi + 1]) * 0.5;
            for (int yi = 0; yi < yCells; yi++)
            {
                if (green[xi, yi])
                {
                    continue;
                }

                double midY = (yList[yi] + yList[yi + 1]) * 0.5;
                if (IsPointInsidePolygon(midX, midY, polygon))
                {
                    green[xi, yi] = true;
                }
            }
        }

        // Build area-weighted prefix sums for green coverage.
        var prefix = new long[xList.Count, yList.Count];
        for (int xi = 0; xi < xCells; xi++)
        {
            long width = xList[xi + 1] - xList[xi];
            for (int yi = 0; yi < yCells; yi++)
            {
                long height = yList[yi + 1] - yList[yi];
                long cellArea = green[xi, yi] ? width * height : 0;
                prefix[xi + 1, yi + 1] = cellArea
                    + prefix[xi, yi + 1]
                    + prefix[xi + 1, yi]
                    - prefix[xi, yi];
            }
        }

        long maxArea = 0;
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                var p1 = points[i];
                var p2 = points[j];
                if (p1.X == p2.X || p1.Y == p2.Y)
                {
                    continue;
                }

                long minX = Math.Min(p1.X, p2.X);
                long maxX = Math.Max(p1.X, p2.X);
                long minY = Math.Min(p1.Y, p2.Y);
                long maxY = Math.Max(p1.Y, p2.Y);

                int xiStart = xIndex[minX];
                int xiEnd = xIndex[maxX];
                int yiStart = yIndex[minY];
                int yiEnd = yIndex[maxY];

                long totalArea = (maxX - minX + 1) * (maxY - minY + 1);
                long greenArea = GetArea(prefix, xiStart, xiEnd, yiStart, yiEnd);
                if (greenArea == totalArea && greenArea > maxArea)
                {
                    maxArea = greenArea;
                }
            }
        }

        return maxArea.ToString();
    }

    private static List<(long X, long Y)> ParsePoints(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);
        var points = new List<(long X, long Y)>();
        foreach (var line in lines)
        {
            var parts = line.Trim().Split(',');
            if (parts.Length == 2 &&
                long.TryParse(parts[0], out long x) &&
                long.TryParse(parts[1], out long y))
            {
                points.Add((x, y));
            }
        }

        return points;
    }

    private static long GetArea(long[,] prefix, int xiStart, int xiEnd, int yiStart, int yiEnd)
    {
        // Inclusive ranges converted to prefix boundaries.
        int x1 = xiStart;
        int x2 = xiEnd + 1;
        int y1 = yiStart;
        int y2 = yiEnd + 1;
        return prefix[x2, y2]
            - prefix[x1, y2]
            - prefix[x2, y1]
            + prefix[x1, y1];
    }

    private static bool IsPointInsidePolygon(double x, double y, List<(long X, long Y)> polygon)
    {
        bool inside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            var pi = polygon[i];
            var pj = polygon[j];
            double xIntersection = ((double)(pj.X - pi.X) * (y - pi.Y) / (pj.Y - pi.Y)) + pi.X;
            bool intersect = ((pi.Y > y) != (pj.Y > y)) &&
                (x < xIntersection);
            if (intersect)
            {
                inside = !inside;
            }
        }

        return inside;
    }
}
