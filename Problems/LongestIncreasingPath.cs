using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/longest-increasing-path-in-a-matrix/
public class LongestIncreasingPath
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] matrix, int expected)
    {
        //act
        var result = new Solution().LongestIncreasingPath(matrix);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int [][]
                {
                    new int[]{9,9,4},
                    new int[]{6,6,8},
                    new int[]{2,1,1}
                },
                4
            }
        };
    }

    public class Solution
    {
        record Coordinate(int Row, int Col)
        {
            public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.Row + b.Row, a.Col + b.Col);
            public bool IsValid(int[][] grid) => Row >= 0 && Row < grid.Length && Col >= 0 & Col < grid[0].Length;
            public int GetValue(int[][] grid) => grid[Row][Col];
        }

        private static readonly Coordinate[] _steps = new Coordinate[] { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) };
        public int LongestIncreasingPath(int[][] matrix)
        {
            var vertices = new int[matrix.Length][];
            for (var row = 0; row < matrix.Length; row++)
            {
                vertices[row] = new int[matrix[row].Length];
                for (var col = 0; col < matrix[0].Length; col++)
                {
                    var current = new Coordinate(row, col);
                    foreach (var step in _steps)
                    {
                        var prev = current + step;
                        if (prev.IsValid(matrix) && current.GetValue(matrix) > prev.GetValue(matrix))
                        {
                            vertices[row][col]++;
                        }
                    }
                }
            }

            var queue = new Queue<Coordinate>();
            for (var row = 0; row < vertices.Length; row++)
            {
                for (var col = 0; col < vertices[0].Length; col++)
                {
                    if (IsLeaf(vertices, row, col))
                    {
                        queue.Enqueue(new(row, col));
                    }
                }
            }

            var result = 0;
            while (queue.Count > 0)
            {
                result++;
                var layerCount = queue.Count;
                for (var i = 0; i < layerCount; i++)
                {
                    var current = queue.Dequeue();
                    foreach (var step in _steps)
                    {
                        var next = current + step;
                        if (next.IsValid(matrix) && next.GetValue(matrix) > current.GetValue(matrix))
                        {
                            vertices[next.Row][next.Col]--;
                            if (vertices[next.Row][next.Col] == 0)
                            {
                                queue.Enqueue(next);
                            }
                        }
                    }

                }
            }
            return result;
        }

        private bool IsLeaf(int[][] vertices, int row, int col) => vertices[row][col] == 0;
    }
}