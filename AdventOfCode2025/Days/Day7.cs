using System;
using System.Collections.Generic;
using AdventOfCode2025;

namespace AdventOfCode2025.Days;

public class Day7 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);
        if (lines.Length == 0)
        {
            return "0";
        }

        // Parse grid and find starting position
        var grid = new List<string>();
        int startRow = -1;
        int startCol = -1;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            grid.Add(line);

            int sIndex = line.IndexOf('S');
            if (sIndex >= 0)
            {
                startRow = i;
                startCol = sIndex;
            }
        }

        if (startRow == -1 || startCol == -1)
        {
            return "0";
        }

        int rows = grid.Count;
        int cols = grid[0].Length;

        // Simulate beam propagation
        int splitCount = 0;
        var activeBeams = new Queue<(int row, int col)>();
        var processedPositions = new HashSet<(int row, int col)>();

        // Start with initial beam at S
        activeBeams.Enqueue((startRow, startCol));
        processedPositions.Add((startRow, startCol));

        while (activeBeams.Count > 0)
        {
            var currentBeams = new List<(int row, int col)>();
            while (activeBeams.Count > 0)
            {
                currentBeams.Add(activeBeams.Dequeue());
            }

            foreach (var (row, col) in currentBeams)
            {
                // Move beam downward
                int nextRow = row + 1;

                // Check if beam exits grid
                if (nextRow >= rows)
                {
                    continue;
                }

                // Check bounds for column
                if (col < 0 || col >= cols)
                {
                    continue;
                }

                char nextCell = grid[nextRow][col];

                if (nextCell == '^')
                {
                    // Hit a splitter - beam stops, two new beams emitted left and right
                    splitCount++;

                    // Left beam
                    int leftCol = col - 1;
                    if (leftCol >= 0 && leftCol < cols)
                    {
                        var leftPos = (nextRow, leftCol);
                        if (!processedPositions.Contains(leftPos))
                        {
                            activeBeams.Enqueue(leftPos);
                            processedPositions.Add(leftPos);
                        }
                    }

                    // Right beam
                    int rightCol = col + 1;
                    if (rightCol >= 0 && rightCol < cols)
                    {
                        var rightPos = (nextRow, rightCol);
                        if (!processedPositions.Contains(rightPos))
                        {
                            activeBeams.Enqueue(rightPos);
                            processedPositions.Add(rightPos);
                        }
                    }
                }
                else if (nextCell == '.' || nextCell == '|')
                {
                    // Beam continues downward
                    var nextPos = (nextRow, col);
                    if (!processedPositions.Contains(nextPos))
                    {
                        activeBeams.Enqueue(nextPos);
                        processedPositions.Add(nextPos);
                    }
                }
            }
        }

        return splitCount.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);
        if (lines.Length == 0)
        {
            return "0";
        }

        // Parse grid and find starting position
        var grid = new List<string>();
        int startRow = -1;
        int startCol = -1;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            grid.Add(line);

            int sIndex = line.IndexOf('S');
            if (sIndex >= 0)
            {
                startRow = i;
                startCol = sIndex;
            }
        }

        if (startRow == -1 || startCol == -1)
        {
            return "0";
        }

        int rows = grid.Count;
        int cols = grid[0].Length;

        // Memoization dictionary: (row, col) -> number of timelines
        var memo = new Dictionary<(int row, int col), long>();

        long timelines = CountTimelines(startRow, startCol, grid, rows, cols, memo);

        return timelines.ToString();
    }

    private static long CountTimelines(
        int row,
        int col,
        List<string> grid,
        int rows,
        int cols,
        Dictionary<(int row, int col), long> memo)
    {
        // Check if we've exited the grid (reached bottom)
        if (row >= rows)
        {
            return 1;
        }

        // Check if out of bounds
        if (col < 0 || col >= cols)
        {
            return 0;
        }

        // Check memoization
        var key = (row, col);
        if (memo.TryGetValue(key, out long cached))
        {
            return cached;
        }

        // Check if we're at the last row (about to exit)
        if (row == rows - 1)
        {
            memo[key] = 1;
            return 1;
        }

        long result;

        // Check what's in the cell below
        char nextCell = grid[row + 1][col];

        if (nextCell == '^')
        {
            // Hit a splitter - particle takes both left and right paths
            // Left path: move to (row+1, col-1)
            // Right path: move to (row+1, col+1)
            long leftTimelines = CountTimelines(row + 1, col - 1, grid, rows, cols, memo);
            long rightTimelines = CountTimelines(row + 1, col + 1, grid, rows, cols, memo);
            result = leftTimelines + rightTimelines;
        }
        else
        {
            // Continue downward
            result = CountTimelines(row + 1, col, grid, rows, cols, memo);
        }

        memo[key] = result;
        return result;
    }
}
