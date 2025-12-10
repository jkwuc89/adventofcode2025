using System;
using System.Reflection;
using AdventOfCode2025.Days;

namespace AdventOfCode2025;

public static class PuzzleFactory
{
    public static IPuzzle? GetPuzzleInstance(int day)
    {
        // Only consider valid puzzle days
        if (day < 1 || day > 25)
        {
            return null;
        }

        string typeName = $"AdventOfCode2025.Days.Day{day}";
        Type? type = Type.GetType(typeName);

        if (type == null)
        {
            // Try loading from the current assembly if Type.GetType doesn't find it
            type = Assembly.GetExecutingAssembly().GetType(typeName);
        }

        if (type == null || !typeof(IPuzzle).IsAssignableFrom(type))
        {
            return null;
        }

        try
        {
            return Activator.CreateInstance(type) as IPuzzle;
        }
        catch
        {
            return null;
        }
    }
}
