namespace AdventOfCode2025;

/// <summary>
/// Interface for puzzle solutions. Each day's puzzle class should implement this interface.
/// </summary>
public interface IPuzzle
{
    /// <summary>
    /// Solves puzzle 1 for the given input.
    /// </summary>
    /// <param name="input">The puzzle input.</param>
    /// <returns>The solution as a string.</returns>
    string SolvePuzzle1(string input);

    /// <summary>
    /// Solves puzzle 2 for the given input.
    /// </summary>
    /// <param name="input">The puzzle input.</param>
    /// <returns>The solution as a string.</returns>
    string SolvePuzzle2(string input);
}
