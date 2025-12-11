using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2025.Days;

public class Day10 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = input.Trim().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return "0";
        }

        long totalPresses = 0;
        foreach (var line in lines)
        {
            var (target, buttons) = ParseMachine(line);
            if (target != null && buttons != null)
            {
                int presses = SolveMachine(target, buttons);
                totalPresses += presses;
            }
        }

        return totalPresses.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        return "0";
    }

#pragma warning disable SA1011 // Closing square bracket should be followed by a space
    private static (bool[]? Target, List<List<int>>? Buttons) ParseMachine(string line)
#pragma warning restore SA1011
    {
        // Extract indicator pattern from [brackets]
        var patternMatch = Regex.Match(line, @"\[([.#]+)\]");
        if (!patternMatch.Success)
        {
            return (null, null);
        }

        string pattern = patternMatch.Groups[1].Value;
        bool[] target = new bool[pattern.Length];
        for (int i = 0; i < pattern.Length; i++)
        {
            target[i] = pattern[i] == '#';
        }

        // Extract all button wirings from (parentheses)
        var buttonMatches = Regex.Matches(line, @"\(([^)]+)\)");
        var buttons = new List<List<int>>();
        foreach (Match match in buttonMatches)
        {
            string buttonStr = match.Groups[1].Value;
            var indices = buttonStr.Split(',')
                .Select(s => s.Trim())
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse)
                .ToList();
            if (indices.Count > 0)
            {
                buttons.Add(indices);
            }
        }

        return (target, buttons);
    }

    private static int SolveMachine(bool[] target, List<List<int>> buttons)
    {
        int numLights = target.Length;
        int numButtons = buttons.Count;

        if (numButtons == 0)
        {
            // No buttons, check if target is all off
            return target.Any(b => b) ? int.MaxValue : 0;
        }

        // Build augmented matrix for Gaussian elimination over GF(2)
        // Each row represents one light's equation
        // Each column represents one button
        // Last column is the target state (right-hand side)
        var matrix = new List<List<bool>>();
        for (int light = 0; light < numLights; light++)
        {
            var row = new List<bool>();
            for (int button = 0; button < numButtons; button++)
            {
                // Button affects this light if light index is in button's list
                bool affects = buttons[button].Contains(light);
                row.Add(affects);
            }

            // Add target state as right-hand side
            row.Add(target[light]);
            matrix.Add(row);
        }

        // Perform Gaussian elimination over GF(2)
        var (solution, nullSpace) = GaussianEliminationWithNullSpace(matrix, numButtons);
        if (solution == null)
        {
            // System is inconsistent (shouldn't happen for valid inputs)
            return int.MaxValue;
        }

        // Minimize the solution by trying to reduce it using null space vectors
        solution = MinimizeSolution(solution, nullSpace);

        // Count how many buttons need to be pressed (solution[i] == true means press button i)
        return solution.Count(b => b);
    }

#pragma warning disable SA1011 // Closing square bracket should be followed by a space
    private static (bool[]? Solution, List<bool[]> NullSpace) GaussianEliminationWithNullSpace(List<List<bool>> matrix, int numButtons)
#pragma warning restore SA1011
    {
        int numRows = matrix.Count;
        int numCols = numButtons + 1; // +1 for augmented column

        // Forward elimination
        int pivotRow = 0;
        var pivotColumns = new List<int>();
        for (int col = 0; col < numButtons && pivotRow < numRows; col++)
        {
            // Find a row with 1 in current column
            int rowWithOne = -1;
            for (int row = pivotRow; row < numRows; row++)
            {
                if (matrix[row][col])
                {
                    rowWithOne = row;
                    break;
                }
            }

            if (rowWithOne == -1)
            {
                // No pivot in this column, this is a free variable
                continue;
            }

            // Swap rows
            if (rowWithOne != pivotRow)
            {
                (matrix[pivotRow], matrix[rowWithOne]) = (matrix[rowWithOne], matrix[pivotRow]);
            }

            pivotColumns.Add(col);

            // Eliminate this column in rows below
            for (int row = pivotRow + 1; row < numRows; row++)
            {
                if (matrix[row][col])
                {
                    // XOR row with pivot row
                    for (int c = 0; c < numCols; c++)
                    {
                        matrix[row][c] = matrix[row][c] ^ matrix[pivotRow][c];
                    }
                }
            }

            pivotRow++;
        }

        // Check for inconsistency (row with all zeros in coefficient columns but non-zero RHS)
        for (int row = pivotRow; row < numRows; row++)
        {
            bool allZeros = true;
            for (int col = 0; col < numButtons; col++)
            {
                if (matrix[row][col])
                {
                    allZeros = false;
                    break;
                }
            }

            if (allZeros && matrix[row][numButtons])
            {
                // Inconsistent system
                return (null, new List<bool[]>());
            }
        }

        // Back substitution to find one solution
        bool[] solution = new bool[numButtons];
        for (int row = pivotRow - 1; row >= 0; row--)
        {
            // Find the pivot column for this row
            int pivotCol = -1;
            for (int col = 0; col < numButtons; col++)
            {
                if (matrix[row][col])
                {
                    pivotCol = col;
                    break;
                }
            }

            if (pivotCol == -1)
            {
                continue;
            }

            // Calculate solution for this variable
            bool value = matrix[row][numButtons]; // RHS
            for (int col = pivotCol + 1; col < numButtons; col++)
            {
                if (matrix[row][col] && solution[col])
                {
                    value = value ^ true; // XOR
                }
            }

            solution[pivotCol] = value;
        }

        // Find null space basis (free variables)
        var nullSpace = new List<bool[]>();
        var pivotSet = new HashSet<int>(pivotColumns);
        for (int col = 0; col < numButtons; col++)
        {
            if (!pivotSet.Contains(col))
            {
                // This is a free variable, create a null space vector
                bool[] nullVector = new bool[numButtons];
                nullVector[col] = true; // Set free variable to 1

                // Back substitute to find the rest
                for (int row = pivotRow - 1; row >= 0; row--)
                {
                    int pivotCol = -1;
                    for (int c = 0; c < numButtons; c++)
                    {
                        if (matrix[row][c])
                        {
                            pivotCol = c;
                            break;
                        }
                    }

                    if (pivotCol == -1)
                    {
                        continue;
                    }

                    // Calculate what this pivot should be to satisfy the homogeneous system
                    bool value = false;
                    for (int c = pivotCol + 1; c < numButtons; c++)
                    {
                        if (matrix[row][c] && nullVector[c])
                        {
                            value = value ^ true;
                        }
                    }

                    nullVector[pivotCol] = value;
                }

                nullSpace.Add(nullVector);
            }
        }

        return (solution, nullSpace);
    }

    private static bool[] MinimizeSolution(bool[] solution, List<bool[]> nullSpace)
    {
        if (nullSpace.Count == 0)
        {
            return solution;
        }

        // Try to reduce the solution by adding null space vectors
        bool[] bestSolution = (bool[])solution.Clone();
        int bestCount = solution.Count(b => b);

        // Try all combinations of null space vectors (up to a reasonable limit)
        int maxCombinations = Math.Min(1 << nullSpace.Count, 1000);
        for (int mask = 1; mask < maxCombinations; mask++)
        {
            bool[] candidate = (bool[])solution.Clone();
            for (int i = 0; i < nullSpace.Count; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    // Add this null space vector
                    for (int j = 0; j < candidate.Length; j++)
                    {
                        candidate[j] = candidate[j] ^ nullSpace[i][j];
                    }
                }
            }

            int count = candidate.Count(b => b);
            if (count < bestCount)
            {
                bestCount = count;
                bestSolution = candidate;
            }
        }

        return bestSolution;
    }
}
