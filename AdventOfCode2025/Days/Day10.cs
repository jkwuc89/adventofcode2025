using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2025;

namespace AdventOfCode2025.Days;

public class Day10 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);
        if (lines.Length == 0)
        {
            return "0";
        }

        long totalPresses = 0;
        foreach (var line in lines)
        {
            var machine = ParseMachine(line);
            if (machine.LightTargets.Length > 0 && machine.Buttons.Count > 0)
            {
                int presses = SolveLightMachine(machine.LightTargets, machine.Buttons);
                totalPresses += presses;
            }
        }

        return totalPresses.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);
        if (lines.Length == 0)
        {
            return "0";
        }

        long totalPresses = 0;
        foreach (var line in lines)
        {
            var machine = ParseMachine(line);
            if (machine.JoltageTargets.Length == 0 || machine.Buttons.Count == 0)
            {
                continue;
            }

            long presses = SolveJoltageMachine(machine.JoltageTargets, machine.Buttons);
            if (presses == long.MaxValue)
            {
                throw new InvalidOperationException("No valid solution found for a machine in puzzle 2.");
            }

            totalPresses += presses;
        }

        return totalPresses.ToString();
    }

#pragma warning disable SA1011 // Closing square bracket should be followed by a space
    private static Machine ParseMachine(string line)
#pragma warning restore SA1011
    {
        var machine = new Machine();

        // Extract indicator pattern from [brackets] for puzzle 1
        var patternMatch = Regex.Match(line, @"\[([.#]+)\]");
        if (patternMatch.Success)
        {
            string pattern = patternMatch.Groups[1].Value;
            bool[] target = new bool[pattern.Length];
            for (int i = 0; i < pattern.Length; i++)
            {
                target[i] = pattern[i] == '#';
            }

            machine.LightTargets = target;
        }

        // Extract all button wirings from (parentheses)
        var buttonMatches = Regex.Matches(line, @"\(([^)]+)\)");
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
                machine.Buttons.Add(indices);
            }
        }

        // Extract joltage requirements from {curly braces}
        var joltageMatch = Regex.Match(line, @"\{([^}]+)\}");
        if (joltageMatch.Success)
        {
            int[] jolts = joltageMatch.Groups[1].Value.Split(',')
                .Select(s => s.Trim())
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse)
                .ToArray();

            if (jolts.Length > 0)
            {
                machine.JoltageTargets = jolts;
            }
        }

        return machine;
    }

    private static int SolveLightMachine(bool[] target, List<List<int>> buttons)
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

    private static long SolveJoltageMachine(int[] targets, List<List<int>> buttons)
    {
        int counterCount = targets.Length;
        int buttonCount = buttons.Count;

        if (counterCount == 0)
        {
            return 0;
        }

        if (buttonCount == 0)
        {
            return targets.Any(t => t != 0) ? long.MaxValue : 0;
        }

        var perButtonMax = ComputePerButtonMaximums(targets, buttons);

        var matrix = new Fraction[counterCount, buttonCount + 1];
        for (int row = 0; row < counterCount; row++)
        {
            for (int col = 0; col < buttonCount; col++)
            {
                matrix[row, col] = buttons[col].Contains(row) ? Fraction.One : Fraction.Zero;
            }

            matrix[row, buttonCount] = new Fraction(targets[row], 1);
        }

        var (rref, pivotColumns, inconsistent) = ToReducedRowEchelon(matrix, counterCount, buttonCount);
        if (inconsistent)
        {
            return long.MaxValue;
        }

        var freeColumns = Enumerable.Range(0, buttonCount).Except(pivotColumns).ToList();
        var freeIndex = freeColumns
            .Select((col, idx) => (col, idx))
            .ToDictionary(t => t.col, t => t.idx);

        var pivotEquations = new List<PivotEquation>();
        for (int row = 0; row < counterCount; row++)
        {
            int pivotCol = -1;
            for (int col = 0; col < buttonCount; col++)
            {
                if (rref[row, col].IsOne)
                {
                    pivotCol = col;
                    break;
                }
            }

            if (pivotCol == -1)
            {
                continue;
            }

            var coefficients = new Dictionary<int, Fraction>();
            foreach (int freeCol in freeColumns)
            {
                if (!rref[row, freeCol].IsZero)
                {
                    coefficients.Add(freeIndex[freeCol], rref[row, freeCol]);
                }
            }

            pivotEquations.Add(
                new PivotEquation
                {
                    PivotColumn = pivotCol,
                    Rhs = rref[row, buttonCount],
                    Coefficients = coefficients,
                });
        }

        if (freeColumns.Count == 0)
        {
            return EvaluateSolution(
                Array.Empty<long>(),
                freeColumns,
                pivotEquations,
                perButtonMax,
                buttonCount);
        }

        var freeAssignments = new long[freeColumns.Count];
        long best = long.MaxValue;
        bool found = false;

        void Search(int index, long currentFreeSum)
        {
            if (found && currentFreeSum >= best)
            {
                return;
            }

            if (index == freeColumns.Count)
            {
                long total = EvaluateSolution(
                    freeAssignments,
                    freeColumns,
                    pivotEquations,
                    perButtonMax,
                    buttonCount);

                if (total >= 0 && total < best)
                {
                    best = total;
                    found = true;
                }

                return;
            }

            int freeColumn = freeColumns[index];
            long upper = perButtonMax[freeColumn];
            if (upper < 0)
            {
                return;
            }

            for (long value = 0; value <= upper; value++)
            {
                freeAssignments[index] = value;
                Search(index + 1, currentFreeSum + value);
            }
        }

        Search(0, 0);

        return best;
    }

    private static long EvaluateSolution(
        IReadOnlyList<long> freeAssignments,
        IReadOnlyList<int> freeColumns,
        IReadOnlyList<PivotEquation> pivotEquations,
        IReadOnlyList<long> perButtonMax,
        int buttonCount)
    {
        var buttonPresses = new long[buttonCount];
        for (int i = 0; i < freeColumns.Count; i++)
        {
            buttonPresses[freeColumns[i]] = freeAssignments[i];
            if (buttonPresses[freeColumns[i]] > perButtonMax[freeColumns[i]])
            {
                return -1;
            }
        }

        foreach (var equation in pivotEquations)
        {
            Fraction value = equation.Rhs;
            foreach (var kvp in equation.Coefficients)
            {
                value -= kvp.Value * freeAssignments[kvp.Key];
            }

            if (!value.IsInteger)
            {
                return -1;
            }

            long pivotValue = value.ToLong();
            if (pivotValue < 0 || pivotValue > perButtonMax[equation.PivotColumn])
            {
                return -1;
            }

            buttonPresses[equation.PivotColumn] = pivotValue;
        }

        return buttonPresses.Sum();
    }

    private static (Fraction[,], List<int>, bool Inconsistent) ToReducedRowEchelon(
        Fraction[,] matrix,
        int rowCount,
        int columnCount)
    {
        int pivotRow = 0;
        var pivotColumns = new List<int>();

        for (int col = 0; col < columnCount && pivotRow < rowCount; col++)
        {
            int rowWithValue = -1;
            for (int row = pivotRow; row < rowCount; row++)
            {
                if (!matrix[row, col].IsZero)
                {
                    rowWithValue = row;
                    break;
                }
            }

            if (rowWithValue == -1)
            {
                continue;
            }

            if (rowWithValue != pivotRow)
            {
                SwapRows(matrix, pivotRow, rowWithValue, columnCount + 1);
            }

            Fraction pivot = matrix[pivotRow, col];
            for (int c = col; c <= columnCount; c++)
            {
                matrix[pivotRow, c] /= pivot;
            }

            for (int row = 0; row < rowCount; row++)
            {
                if (row == pivotRow || matrix[row, col].IsZero)
                {
                    continue;
                }

                Fraction factor = matrix[row, col];
                for (int c = col; c <= columnCount; c++)
                {
                    matrix[row, c] -= factor * matrix[pivotRow, c];
                }
            }

            pivotColumns.Add(col);
            pivotRow++;
        }

        bool inconsistent = false;
        for (int row = 0; row < rowCount; row++)
        {
            bool allZero = true;
            for (int col = 0; col < columnCount; col++)
            {
                if (!matrix[row, col].IsZero)
                {
                    allZero = false;
                    break;
                }
            }

            if (allZero && !matrix[row, columnCount].IsZero)
            {
                inconsistent = true;
                break;
            }
        }

        return (matrix, pivotColumns, inconsistent);
    }

    private static long[] ComputePerButtonMaximums(IReadOnlyList<int> targets, IReadOnlyList<List<int>> buttons)
    {
        var perButtonMax = new long[buttons.Count];
        for (int button = 0; button < buttons.Count; button++)
        {
            long minTarget = long.MaxValue;
            foreach (int counter in buttons[button])
            {
                if (counter >= 0 && counter < targets.Count)
                {
                    minTarget = Math.Min(minTarget, targets[counter]);
                }
            }

            perButtonMax[button] = minTarget == long.MaxValue ? 0 : minTarget;
        }

        return perButtonMax;
    }

    private static void SwapRows(Fraction[,] matrix, int rowA, int rowB, int width)
    {
        for (int c = 0; c < width; c++)
        {
            (matrix[rowA, c], matrix[rowB, c]) = (matrix[rowB, c], matrix[rowA, c]);
        }
    }

    private readonly struct Fraction : IEquatable<Fraction>
    {
        public static readonly Fraction Zero = new Fraction(0, 1);

        public static readonly Fraction One = new Fraction(1, 1);

        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            long gcd = Gcd(Math.Abs(numerator), denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        public long Numerator { get; }

        public long Denominator { get; }

        public bool IsZero => Numerator == 0;

        public bool IsOne => Numerator == Denominator;

        public bool IsInteger => Denominator == 1;

        public bool IsNonPositive => Numerator <= 0;

        public static Fraction operator +(Fraction left, Fraction right)
        {
            long numerator = (left.Numerator * right.Denominator) + (right.Numerator * left.Denominator);
            long denominator = left.Denominator * right.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator -(Fraction left, Fraction right)
        {
            long numerator = (left.Numerator * right.Denominator) - (right.Numerator * left.Denominator);
            long denominator = left.Denominator * right.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator *(Fraction left, Fraction right)
        {
            return new Fraction(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
        }

        public static Fraction operator /(Fraction left, Fraction right)
        {
            return new Fraction(left.Numerator * right.Denominator, left.Denominator * right.Numerator);
        }

        public static Fraction operator *(Fraction left, long right)
        {
            return new Fraction(left.Numerator * right, left.Denominator);
        }

        public static Fraction operator *(long left, Fraction right)
        {
            return new Fraction(left * right.Numerator, right.Denominator);
        }

        public Fraction Multiply(long value)
        {
            return new Fraction(Numerator * value, Denominator);
        }

        public long DivideAndFloor(Fraction divisor)
        {
            Fraction result = this / divisor;
            if (result.Numerator >= 0)
            {
                return result.Numerator / result.Denominator;
            }

            return -((-result.Numerator + result.Denominator - 1) / result.Denominator);
        }

        public long ToLong()
        {
            if (Denominator != 1)
            {
                throw new InvalidOperationException("Fraction is not an integer.");
            }

            return Numerator;
        }

        public bool Equals(Fraction other)
        {
            return Numerator == other.Numerator && Denominator == other.Denominator;
        }

        public override bool Equals(object? obj)
        {
            return obj is Fraction fraction && Equals(fraction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        private static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                (a, b) = (b, a % b);
            }

            return a;
        }
    }

    private sealed class Machine
    {
        public bool[] LightTargets { get; set; } = Array.Empty<bool>();

        public int[] JoltageTargets { get; set; } = Array.Empty<int>();

        public List<List<int>> Buttons { get; } = new List<List<int>>();
    }

    private sealed class PivotEquation
    {
        required public int PivotColumn { get; init; }

        required public Fraction Rhs { get; init; }

        required public Dictionary<int, Fraction> Coefficients { get; init; }
    }
}
