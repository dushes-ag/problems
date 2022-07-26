using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/shortest-path-in-binary-matrix/
public class ShortestPathBinaryMatrix
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] grid, int expected)
    {
        //act
        var result = new Solution().ShortestPathBinaryMatrix(grid);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int [][]
                {
                    new int[]{0,0,0},
                    new int[]{1,1,0},
                    new int[]{1,1,0}
                },
                4
            }
        };
    }

    public class Solution
    {
        const int NOT_FOUND = -1;
        public int ShortestPathBinaryMatrix(int[][] grid)
        {
            var n = grid.Length;
            if (grid[0][0] != 0 || grid[n - 1][n - 1] != 0)
            {
                return -1;
            }

            var map = new bool[n][];
            for (var i = 0; i < n; i++)
            {
                map[i] = new bool[n];
            }
            var queue = new Queue<(int row, int col, int path)>();
            queue.Enqueue((0, 0, 1));
            while (queue.Count != 0)
            {
                var (row, col, path) = queue.Dequeue();
                if (row == n - 1 && col == n - 1)
                {
                    return path;
                }
                for (var i = 1; i >= -1; i--)
                {
                    for (var j = 1; j >= -1; j--)
                    {
                        if (i == 0 && j == 0
                           || (row + i) < 0 || (row + i) >= n || (col + j) < 0 || (col + j) >= n
                           || grid[row + i][col + j] != 0
                           || map[row + i][col + j])
                        {
                            continue;
                        }
                        queue.Enqueue((row + i, col + j, path + 1));
                        map[row + i][col + j] = true;
                    }
                }
            }
            return NOT_FOUND;
        }
    }
}