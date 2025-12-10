namespace AdventOfCode2025.Days;

public class Day1 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = input.Trim().Split('\n');
        var position = 50;
        var timesAt0 = 0;

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line.AsSpan(1));

            if (direction == 'L')
            {
                position = (position - distance) % 100;
                if (position < 0)
                {
                    position += 100;
                }
            }
            else
            {
                // 'R'
                position = (position + distance) % 100;
            }

            if (position == 0)
            {
                timesAt0++;
            }
        }

        return timesAt0.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var lines = input.Trim().Split('\n');
        var position = 50;
        var timesAt0 = 0;

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line.AsSpan(1));

            if (direction == 'L')
            {
                // Check positions during rotation (excluding start and end)
                // From (position - 1) down to (position - distance + 1) mod 100
                for (int i = 1; i < distance; i++)
                {
                    var currentPos = (position - i) % 100;
                    if (currentPos < 0)
                    {
                        currentPos += 100;
                    }

                    // Passed through 0
                    if (currentPos == 0)
                    {
                        timesAt0++;
                    }
                }

                // Check if end position is 0
                var endPos = (position - distance) % 100;
                if (endPos < 0)
                {
                    endPos += 100;
                }

                if (endPos == 0)
                {
                    timesAt0++;
                }

                position = endPos;
            }
            else
            {
                // 'R'
                // Check positions during rotation (excluding start and end)
                // From (position + 1) up to (position + distance - 1) mod 100
                for (int i = 1; i < distance; i++)
                {
                    var currentPos = (position + i) % 100;

                    // Passed through 0
                    if (currentPos == 0)
                    {
                        timesAt0++;
                    }
                }

                // Check if end position is 0
                var endPos = (position + distance) % 100;
                if (endPos == 0)
                {
                    timesAt0++;
                }

                position = endPos;
            }
        }

        return timesAt0.ToString();
    }
}
