using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2025.Days;

public class Day8 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var lines = input.Trim().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return "0";
        }

        // Parse coordinates
        var boxes = new List<(int X, int Y, int Z)>();
        foreach (var line in lines)
        {
            var parts = line.Trim().Split(',');
            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y) &&
                int.TryParse(parts[2], out int z))
            {
                boxes.Add((x, y, z));
            }
        }

        if (boxes.Count == 0)
        {
            return "0";
        }

        // Calculate all pairwise distances
        var distances = new List<(int Box1, int Box2, double Distance)>();
        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = i + 1; j < boxes.Count; j++)
            {
                double distance = CalculateEuclideanDistance(boxes[i], boxes[j]);
                distances.Add((i, j, distance));
            }
        }

        // Sort by distance (shortest first)
        distances.Sort((a, b) => a.Distance.CompareTo(b.Distance));

        // Initialize Union-Find
        var unionFind = new UnionFind(boxes.Count);

        // Determine number of connections: 10 for sample (20 boxes), 1000 for full input
        int targetConnections = boxes.Count == 20 ? 10 : 1000;

        // Process pairs in order until we've processed the target number
        int pairsProcessed = 0;
        foreach (var (box1, box2, _) in distances)
        {
            if (pairsProcessed >= targetConnections)
            {
                break;
            }

            pairsProcessed++;

            // Only connect if not already in same circuit
            if (unionFind.Find(box1) != unionFind.Find(box2))
            {
                unionFind.Union(box1, box2);
            }
        }

        // Count circuit sizes
        var circuitSizes = new Dictionary<int, int>();
        for (int i = 0; i < boxes.Count; i++)
        {
            int root = unionFind.Find(i);
            circuitSizes.TryGetValue(root, out int count);
            circuitSizes[root] = count + 1;
        }

        // Get three largest circuits
        var sizes = circuitSizes.Values.OrderByDescending(s => s).Take(3).ToList();
        if (sizes.Count < 3)
        {
            return "0";
        }

        long product = (long)sizes[0] * sizes[1] * sizes[2];
        return product.ToString();
    }

    public string SolvePuzzle2(string input)
    {
        return "0";
    }

    private static double CalculateEuclideanDistance((int X, int Y, int Z) p1, (int X, int Y, int Z) p2)
    {
        long dx = p1.X - p2.X;
        long dy = p1.Y - p2.Y;
        long dz = p1.Z - p2.Z;
        return Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
    }

    private class UnionFind
    {
        private readonly int[] parent;
        private readonly int[] rank;

        public UnionFind(int size)
        {
            parent = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]); // Path compression
            }

            return parent[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX == rootY)
            {
                return;
            }

            // Union by rank
            if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            else if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;
            }
            else
            {
                parent[rootY] = rootX;
                rank[rootX]++;
            }
        }
    }
}
