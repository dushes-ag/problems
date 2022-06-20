using System;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/minimum-path-sum/
public class MinPathSum
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] grid, int expected)
    {
        //act
        var result = new Solution().MinPathSum(grid);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int[][] {
                    new[]{1,3,1},
                    new[]{1,5,1},
                    new[]{4,2,1}},
                7}
        };
    }

    public class Solution
    {
        public int MinPathSum(int[][] grid)
        {
            var m = grid.Length;
            var n = grid[0].Length;
            var pathSums = new int[n];
            for (var row = m - 1; row >= 0; row--)
            {
                for (var col = n - 1; col >= 0; col--)
                {
                    var prevPath = Math.Min(
                        row == m - 1 ? int.MaxValue : pathSums[col],
                        col == n - 1 ? int.MaxValue : pathSums[col + 1]);
                    pathSums[col] = grid[row][col] + (prevPath == int.MaxValue ? 0 : prevPath);
                }
            }
            return pathSums[0];
        }
    }
}