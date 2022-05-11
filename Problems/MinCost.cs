using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/number-of-operations-to-make-network-connected/
public class MinCost
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] grid, int expected)
    {
        //act
        var result = new Solution().MinCost(grid);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int[][] {new[]{1,1,1,1}, new[]{2,2,2,2}, new[]{1,1,1,1}, new[]{2,2,2,2}},
                3
            },
            new object[]{
                new int[][] { new[]{2,2,2},  new[]{2,2,2}},
                3
            }
        };
    }

    public class Solution
    {
        private static readonly Dictionary<int, Coordinate> _steps = new() { { 1, new(0, 1) }, { 2, new(0, -1) }, { 3, new(1, 0) }, { 4, new(-1, 0) } };
        record Coordinate(int Row, int Col)
        {
            public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.Row + b.Row, a.Col + b.Col);
            public bool IsValid(int[][] grid) => Row >= 0 && Row < grid.Length && Col >= 0 & Col < grid[0].Length;
            public Coordinate GetNext(int[][] grid) => this + _steps[grid[Row][Col]];
        }

        public int MinCost(int[][] grid)
        {
            var targetCell = new Coordinate(grid.Length - 1, grid[0].Length - 1);

            var queue = new PriorityQueue<(Coordinate, int), int>();
            queue.Enqueue((new Coordinate(0, 0), 0), 0);
            var visitedCells = new HashSet<Coordinate>();
            while (queue.Count > 0)
            {
                var (currentCell, weight) = queue.Dequeue();
                if (currentCell.Equals(targetCell))
                {
                    return weight;
                }
                if (visitedCells.Contains(currentCell))
                {
                    continue;
                }

                visitedCells.Add(currentCell);
                foreach (var step in _steps)
                {
                    var nextStep = currentCell + step.Value;
                    if (!nextStep.IsValid(grid))
                    {
                        continue;
                    }
                    var nextWeight = weight + (step.Key == grid[currentCell.Row][currentCell.Col] ? 0 : 1);
                    queue.Enqueue((nextStep, nextWeight), nextWeight);
                }
            }

            return -1;
        }
    }
}