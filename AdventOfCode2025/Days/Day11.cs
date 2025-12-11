using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2025;

namespace AdventOfCode2025.Days;

public class Day11 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);

        if (lines.Length == 0)
        {
            return "0";
        }

        var graph = ParseGraph(lines);
        long paths = CountPaths("you", "out", graph);
        return paths.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        var lines = DayUtils.SplitNonEmptyLines(input);

        if (lines.Length == 0)
        {
            return "0";
        }

        var graph = ParseGraph(lines);
        long paths = CountPathsWithRequiredNodes("svr", "out", graph, "dac", "fft");
        return paths.ToString();
    }

    private static Dictionary<string, List<string>> ParseGraph(IEnumerable<string> lines)
    {
        var graph = new Dictionary<string, List<string>>(StringComparer.Ordinal);

        foreach (var raw in lines)
        {
            var line = raw.Trim();
            if (line.Length == 0)
            {
                continue;
            }

            int colonIndex = line.IndexOf(':');
            if (colonIndex < 0)
            {
                continue;
            }

            string source = line[..colonIndex].Trim();
            string rest = line.Substring(colonIndex + 1).Trim();

            var destinations = rest.Length == 0
                ? Array.Empty<string>()
                : rest.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (!graph.TryGetValue(source, out var list))
            {
                list = new List<string>();
                graph[source] = list;
            }

            list.AddRange(destinations);
        }

        return graph;
    }

    private static long CountPaths(
        string start,
        string target,
        IReadOnlyDictionary<string, List<string>> graph)
    {
        var memo = new Dictionary<string, long>(StringComparer.Ordinal);
        var visiting = new HashSet<string>(StringComparer.Ordinal);

        long Dfs(string node)
        {
            if (memo.TryGetValue(node, out long cached))
            {
                return cached;
            }

            if (node.Equals(target, StringComparison.Ordinal))
            {
                return 1;
            }

            if (!graph.TryGetValue(node, out var neighbors) || neighbors.Count == 0)
            {
                memo[node] = 0;
                return 0;
            }

            if (visiting.Contains(node))
            {
                memo[node] = 0;
                return 0;
            }

            visiting.Add(node);

            long total = 0;
            foreach (var next in neighbors)
            {
                total += Dfs(next);
            }

            visiting.Remove(node);
            memo[node] = total;
            return total;
        }

        return Dfs(start);
    }

    private static long CountPathsWithRequiredNodes(
        string start,
        string target,
        IReadOnlyDictionary<string, List<string>> graph,
        string requiredA,
        string requiredB)
    {
        int requiredMask = 0;
        const int MaskA = 1;
        const int MaskB = 2;
        requiredMask |= MaskA | MaskB;

        var memo = new Dictionary<(string Node, int Mask), long>();
        var visiting = new HashSet<(string Node, int Mask)>();

        long Dfs(string node, int mask)
        {
            if (node.Equals(requiredA, StringComparison.Ordinal))
            {
                mask |= MaskA;
            }
            else if (node.Equals(requiredB, StringComparison.Ordinal))
            {
                mask |= MaskB;
            }

            if (node.Equals(target, StringComparison.Ordinal))
            {
                return mask == requiredMask ? 1 : 0;
            }

            if (memo.TryGetValue((node, mask), out long cached))
            {
                return cached;
            }

            if (!graph.TryGetValue(node, out var neighbors) || neighbors.Count == 0)
            {
                memo[(node, mask)] = 0;
                return 0;
            }

            if (visiting.Contains((node, mask)))
            {
                memo[(node, mask)] = 0;
                return 0;
            }

            visiting.Add((node, mask));

            long total = 0;
            foreach (var next in neighbors)
            {
                total += Dfs(next, mask);
            }

            visiting.Remove((node, mask));
            memo[(node, mask)] = total;
            return total;
        }

        return Dfs(start, 0);
    }
}
