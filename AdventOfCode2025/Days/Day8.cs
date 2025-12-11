using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2025.Days;

public class Day8 : IPuzzle
{
    public string SolvePuzzle1(string input)
    {
        var boxes = ParseCoordinates(input);
        if (boxes.Count == 0)
        {
            return "0";
        }

        var distances = CalculateAllPairwiseDistances(boxes);
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

        // Get three largest circuits
        var circuitSizes = GetCircuitSizes(unionFind, boxes.Count);
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
        var boxes = ParseCoordinates(input);
        if (boxes.Count == 0)
        {
            return "0";
        }

        var distances = CalculateAllPairwiseDistances(boxes);
        var unionFind = new UnionFind(boxes.Count);

        // Track the last connection made
        int lastBox1 = -1;
        int lastBox2 = -1;

        // Connect pairs until all boxes are in one circuit
        foreach (var (box1, box2, _) in distances)
        {
            // Only connect if not already in same circuit
            if (unionFind.Find(box1) != unionFind.Find(box2))
            {
                unionFind.Union(box1, box2);
                lastBox1 = box1;
                lastBox2 = box2;

                // Check if all boxes are now in one circuit
                if (CountCircuits(unionFind, boxes.Count) == 1)
                {
                    // This is the last connection that completes the single circuit
                    break;
                }
            }
        }

        if (lastBox1 == -1 || lastBox2 == -1)
        {
            return "0";
        }

        long product = (long)boxes[lastBox1].X * boxes[lastBox2].X;
        return product.ToString();
    }

    private static List<(int X, int Y, int Z)> ParseCoordinates(string input)
    {
        var lines = input.Trim().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
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

        return boxes;
    }

    private static List<(int Box1, int Box2, double Distance)> CalculateAllPairwiseDistances(List<(int X, int Y, int Z)> boxes)
    {
        var distances = new List<(int Box1, int Box2, double Distance)>();
        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = i + 1; j < boxes.Count; j++)
            {
                double distance = CalculateEuclideanDistance(boxes[i], boxes[j]);
                distances.Add((i, j, distance));
            }
        }

        distances.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        return distances;
    }

    private static Dictionary<int, int> GetCircuitSizes(UnionFind unionFind, int boxCount)
    {
        var circuitSizes = new Dictionary<int, int>();
        for (int i = 0; i < boxCount; i++)
        {
            int root = unionFind.Find(i);
            circuitSizes.TryGetValue(root, out int count);
            circuitSizes[root] = count + 1;
        }

        return circuitSizes;
    }

    private static int CountCircuits(UnionFind unionFind, int boxCount)
    {
        var roots = new HashSet<int>();
        for (int i = 0; i < boxCount; i++)
        {
            roots.Add(unionFind.Find(i));
        }

        return roots.Count;
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
