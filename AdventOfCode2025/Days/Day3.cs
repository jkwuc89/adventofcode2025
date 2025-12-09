using System;

namespace AdventOfCode2025.Days;

/// <summary>
/// Day 3: Escalator Power
/// </summary>
public class Day3 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = input.Trim().Split('\n');
        long totalJoltage = 0;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.Length == 0)
                continue;

            var maxJoltage = FindMaxJoltage2Digits(trimmedLine);
            totalJoltage += maxJoltage;
        }

        return totalJoltage.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var lines = input.Trim().Split('\n');
        long totalJoltage = 0;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.Length == 0)
                continue;

            var maxJoltage = FindMaxJoltage12Digits(trimmedLine);
            totalJoltage += maxJoltage;
        }

        return totalJoltage.ToString();
    }

    private static int FindMaxJoltage2Digits(string bank)
    {
        int maxJoltage = 0;

        // Try all pairs of batteries (i, j) where i < j
        for (int i = 0; i < bank.Length; i++)
        {
            for (int j = i + 1; j < bank.Length; j++)
            {
                var firstDigit = bank[i] - '0';
                var secondDigit = bank[j] - '0';
                var joltage = firstDigit * 10 + secondDigit;

                if (joltage > maxJoltage)
                    maxJoltage = joltage;
            }
        }

        return maxJoltage;
    }

    private static long FindMaxJoltage12Digits(string bank)
    {
        const int targetLength = 12;

        if (bank.Length <= targetLength)
        {
            // If bank has 12 or fewer digits, use all of them
            return long.Parse(bank);
        }

        // We need to select exactly 12 digits to form the largest number
        // Use greedy approach: for each position, pick the largest digit possible
        // while ensuring we can still complete the remaining positions

        var result = new char[targetLength];
        int resultIndex = 0;
        int startIndex = 0;

        for (int pos = 0; pos < targetLength; pos++)
        {
            // Calculate the range we can search in for this position
            // We need to leave enough digits for the remaining positions
            int remainingPositions = targetLength - pos - 1;
            int endIndex = bank.Length - remainingPositions - 1;

            // Find the maximum digit in the allowed range
            char maxDigit = '0';
            int maxDigitIndex = startIndex;

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (bank[i] > maxDigit)
                {
                    maxDigit = bank[i];
                    maxDigitIndex = i;
                }
            }

            result[resultIndex++] = maxDigit;
            startIndex = maxDigitIndex + 1;
        }

        return long.Parse(new string(result));
    }
}

