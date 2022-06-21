using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/cherry-pickup-ii/
public class CherryPickupII
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] grid, int expected)
    {
        //act
        var result = new Solution().CherryPickup(grid);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int[][] {new[]{3,1,1}, new[]{2,5,1}, new[]{1,5,5}, new[]{2,1,1}},
                24
            }
        };
    }

    public class Solution
    {
        public int CherryPickup(int[][] grid)
        {
            return Dp(grid, 0, 0, grid[0].Length - 1);
        }
        private Dictionary<State, int> _cache = new();
        private Step[] _steps = new Step[]
        {
            new(-1, -1),
            new(-1, 0),
            new(-1, 1),
            new(0, -1),
            new(0, 0),
            new(0, 1),
            new(1, -1),
            new(1, 0),
            new(1, 1)
        };
        private int Dp(int[][] grid, int row, int col1, int col2)
        {
            var key = new State(row, Math.Min(col1, col2), Math.Max(col1, col2));
            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }
            _cache[key] = col1 != col2 ? grid[row][col1] + grid[row][col2] : grid[row][col1];

            if (row == grid.Length - 1)
            {
                return _cache[key];
            }

            _cache[key] += _steps.Select(_ => new State(row, col1 + _.Col1, col2 + _.Col2))
                .Where(_ => _.Col1 >= 0 && _.Col2 >= 0 && _.Col1 < grid[0].Length && _.Col2 < grid[0].Length)
                .Max(_ => Dp(grid, row + 1, _.Col1, _.Col2));

            return _cache[key];
        }
        record State(int Row, int Col1, int Col2);
        record Step(int Col1, int Col2);
    }
}