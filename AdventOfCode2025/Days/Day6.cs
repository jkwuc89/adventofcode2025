using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2025.Days;

public class Day6 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = input.Trim().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return "0";
        }

        // Determine grid width (max line length)
        int width = lines.Max(line => line.Length);

        // Pad all lines to same width
        var grid = new List<string>();
        foreach (var line in lines)
        {
            grid.Add(line.PadRight(width));
        }

        // Find separator columns (columns that are all spaces)
        var separatorColumns = new HashSet<int>();
        for (int col = 0; col < width; col++)
        {
            bool allSpaces = true;
            foreach (var row in grid)
            {
                if (col < row.Length && row[col] != ' ')
                {
                    allSpaces = false;
                    break;
                }
            }

            if (allSpaces)
            {
                separatorColumns.Add(col);
            }
        }

        // Group columns into problems (between separators)
        var problems = new List<List<int>>();
        var currentProblem = new List<int>();

        for (int col = 0; col < width; col++)
        {
            if (separatorColumns.Contains(col))
            {
                if (currentProblem.Count > 0)
                {
                    problems.Add(new List<int>(currentProblem));
                    currentProblem.Clear();
                }
            }
            else
            {
                currentProblem.Add(col);
            }
        }

        if (currentProblem.Count > 0)
        {
            problems.Add(currentProblem);
        }

        // Solve each problem
        long grandTotal = 0;
        int operatorRow = grid.Count - 1;

        foreach (var problemColumns in problems)
        {
            // Extract numbers from each row (except operator row)
            var numbers = new List<long>();
            for (int row = 0; row < operatorRow; row++)
            {
                var numberStr = ExtractNumberFromColumns(grid[row], problemColumns);
                if (!string.IsNullOrWhiteSpace(numberStr))
                {
                    if (long.TryParse(numberStr.Trim(), out long num))
                    {
                        numbers.Add(num);
                    }
                }
            }

            // Extract operator from last row
            var operatorStr = ExtractOperatorFromColumns(grid[operatorRow], problemColumns);
            if (numbers.Count == 0 || string.IsNullOrWhiteSpace(operatorStr))
            {
                continue;
            }

            // Calculate result
            long result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                if (operatorStr.Trim() == "*")
                {
                    result *= numbers[i];
                }
                else if (operatorStr.Trim() == "+")
                {
                    result += numbers[i];
                }
            }

            grandTotal += result;
        }

        return grandTotal.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var lines = input.Trim().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return "0";
        }

        // Determine grid width (max line length)
        int width = lines.Max(line => line.Length);

        // Pad all lines to same width
        var grid = new List<string>();
        foreach (var line in lines)
        {
            grid.Add(line.PadRight(width));
        }

        // Find separator columns (columns that are all spaces)
        var separatorColumns = new HashSet<int>();
        for (int col = 0; col < width; col++)
        {
            bool allSpaces = true;
            foreach (var row in grid)
            {
                if (col < row.Length && row[col] != ' ')
                {
                    allSpaces = false;
                    break;
                }
            }

            if (allSpaces)
            {
                separatorColumns.Add(col);
            }
        }

        // Group columns into problems (between separators)
        var problems = new List<List<int>>();
        var currentProblem = new List<int>();

        for (int col = 0; col < width; col++)
        {
            if (separatorColumns.Contains(col))
            {
                if (currentProblem.Count > 0)
                {
                    problems.Add(new List<int>(currentProblem));
                    currentProblem.Clear();
                }
            }
            else
            {
                currentProblem.Add(col);
            }
        }

        if (currentProblem.Count > 0)
        {
            problems.Add(currentProblem);
        }

        // Solve each problem
        long grandTotal = 0;
        int operatorRow = grid.Count - 1;

        foreach (var problemColumns in problems)
        {
            // Extract operator from last row
            var operatorStr = ExtractOperatorFromColumns(grid[operatorRow], problemColumns);
            if (string.IsNullOrWhiteSpace(operatorStr))
            {
                continue;
            }

            char op = operatorStr.Trim()[0];

            // Read numbers right-to-left, column by column
            // Each column represents one number, with digits stacked vertically (most significant at top)
            // Process columns from right to left
            var numbers = new List<long>();

            for (int i = problemColumns.Count - 1; i >= 0; i--)
            {
                int col = problemColumns[i];

                // Read digits from this column top-to-bottom (excluding operator row)
                var digits = new List<char>();
                for (int row = 0; row < operatorRow; row++)
                {
                    if (col < grid[row].Length)
                    {
                        char c = grid[row][col];
                        if (char.IsDigit(c))
                        {
                            digits.Add(c);
                        }
                    }
                }

                // If column has digits, it represents one number
                if (digits.Count > 0)
                {
                    var numberStr = new string(digits.ToArray());
                    if (long.TryParse(numberStr, out long num))
                    {
                        numbers.Add(num);
                    }
                }
            }

            if (numbers.Count == 0)
            {
                continue;
            }

            // Calculate result
            long result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                if (op == '*')
                {
                    result *= numbers[i];
                }
                else if (op == '+')
                {
                    result += numbers[i];
                }
            }

            grandTotal += result;
        }

        return grandTotal.ToString();
    }

    private static string ExtractNumberFromColumns(string row, List<int> columns)
    {
        var chars = new List<char>();
        foreach (var col in columns)
        {
            if (col < row.Length)
            {
                chars.Add(row[col]);
            }
        }

        return new string(chars.ToArray());
    }

    private static string ExtractOperatorFromColumns(string row, List<int> columns)
    {
        var chars = new List<char>();
        foreach (var col in columns)
        {
            if (col < row.Length)
            {
                char c = row[col];
                if (c == '*' || c == '+')
                {
                    chars.Add(c);
                }
            }
        }

        return new string(chars.ToArray());
    }
}
