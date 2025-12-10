using System;
using System.Collections.Generic;

namespace AdventOfCode2025.Days;

public class Day4 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = input.Trim().Split('\n');
        var grid = new char[lines.Length][];

        // Parse grid
        for (int i = 0; i < lines.Length; i++)
        {
            var trimmedLine = lines[i].Trim();
            grid[i] = trimmedLine.ToCharArray();
        }

        int accessibleCount = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        // Check each position in the grid
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row][col] == '@')
                {
                    // Count adjacent @ symbols in 8 directions
                    int adjacentCount = CountAdjacentRolls(grid, row, col, rows, cols);

                    // If fewer than 4 adjacent rolls, it's accessible
                    if (adjacentCount < 4)
                    {
                        accessibleCount++;
                    }
                }
            }
        }

        return accessibleCount.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var lines = input.Trim().Split('\n');
        var grid = new char[lines.Length][];

        // Parse grid
        for (int i = 0; i < lines.Length; i++)
        {
            var trimmedLine = lines[i].Trim();
            grid[i] = trimmedLine.ToCharArray();
        }

        int totalRemoved = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        // Iteratively remove accessible rolls until no more can be removed
        while (true)
        {
            // Find all accessible rolls in current state
            var accessiblePositions = new List<(int row, int col)>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (grid[row][col] == '@')
                    {
                        // Count adjacent @ symbols in 8 directions
                        int adjacentCount = CountAdjacentRolls(grid, row, col, rows, cols);

                        // If fewer than 4 adjacent rolls, it's accessible
                        if (adjacentCount < 4)
                        {
                            accessiblePositions.Add((row, col));
                        }
                    }
                }
            }

            // If no accessible rolls found, we're done
            if (accessiblePositions.Count == 0)
            {
                break;
            }

            // Remove all accessible rolls at once
            foreach (var (row, col) in accessiblePositions)
            {
                grid[row][col] = '.';
            }

            // Add to total count
            totalRemoved += accessiblePositions.Count;
        }

        return totalRemoved.ToString();
    }

    private static int CountAdjacentRolls(char[][] grid, int row, int col, int rows, int cols)
    {
        int count = 0;

        // Check all 8 adjacent positions
        for (int dr = -1; dr <= 1; dr++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                // Skip the center position (the roll itself)
                if (dr == 0 && dc == 0)
                {
                    continue;
                }

                int newRow = row + dr;
                int newCol = col + dc;

                // Check bounds
                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                {
                    if (grid[newRow][newCol] == '@')
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
}
