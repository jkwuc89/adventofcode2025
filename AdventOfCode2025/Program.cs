using AdventOfCode2025;
using AdventOfCode2025.Days;

if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: adventofcode2025 <day> <puzzle>");
    Console.Error.WriteLine("  day: 1-25");
    Console.Error.WriteLine("  puzzle: 1 or 2");
    Environment.Exit(1);
}

if (!int.TryParse(args[0], out int day) || day < 1 || day > 25)
{
    Console.Error.WriteLine($"Invalid day: {args[0]}. Day must be between 1 and 25.");
    Environment.Exit(1);
}

if (!int.TryParse(args[1], out int puzzle) || (puzzle != 1 && puzzle != 2))
{
    Console.Error.WriteLine($"Invalid puzzle: {args[1]}. Puzzle must be 1 or 2.");
    Environment.Exit(1);
}

// Get the puzzle instance
IPuzzle? puzzleInstance = GetPuzzleInstance(day);
if (puzzleInstance == null)
{
    Console.Error.WriteLine($"Day {day} is not yet implemented.");
    Environment.Exit(1);
}

// Read input file
string inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "AdventOfCode2025", "input", $"day{day}input.txt");
if (!File.Exists(inputFilePath))
{
    Console.Error.WriteLine($"Input file not found: {inputFilePath}");
    Environment.Exit(1);
}

string input = File.ReadAllText(inputFilePath).TrimEnd();

// Solve the puzzle
string result = puzzle == 1
    ? puzzleInstance.SolvePuzzle1(input)
    : puzzleInstance.SolvePuzzle2(input);

Console.WriteLine(result);

static IPuzzle? GetPuzzleInstance(int day)
{
    return day switch
    {
        1 => new Day1(),
        2 => new Day2(),
        3 => new Day3(),
        4 => new Day4(),
        5 => new Day5(),

        // Add more days as they are implemented
        _ => null
    };
}
