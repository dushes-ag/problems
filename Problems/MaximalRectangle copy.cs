using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/number-of-islands
public class NumIslands
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(char[][] grid, int expected)
    {
        //act
        var result = new Solution().NumIslands(grid);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new char[][] {
                    new[]{'1','1','1','1','0'},
                    new[]{'1','1','0','1','0'},
                    new[]{'1','1','0','0','0'},
                    new[]{'0','0','0','0','0'},
                    },
                1
            },
            new object[]{
                new char[][] {
                    new[]{'1','1','0','0','0'},
                    new[]{'1','1','0','0','0'},
                    new[]{'0','0','1','0','0'},
                    new[]{'1','1','0','0','0'}
                    },
                3
            }
        };
    }

    public class Solution
    {
        const char _land = '1';
        public int NumIslands(char[][] grid)
        {
            var result = 0;
            var map = new bool[grid.Length][];
            for (var i = 0; i < grid.Length; i++)
            {
                map[i] = new bool[grid[0].Length];
            }
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[0].Length; j++)
                {
                    if (map[i][j])
                    {
                        continue;
                    }
                    if (grid[i][j] != _land)
                    {
                        map[i][j] = true;
                        continue;
                    }
                    BFS(grid, map, i, j);
                    result++;
                }
            }
            return result;
        }
        private List<(int x, int y)> _moves = new() { (0, 1), (0, -1), (1, 0), (-1, 0) };
        private void BFS(char[][] grid, bool[][] map, int i, int j)
        {
            var queue = new Queue<(int row, int col)>();
            queue.Enqueue((i, j));
            map[i][j] = true;
            while (queue.Count > 0)
            {
                var (row, col) = queue.Dequeue();
                foreach (var (x, y) in _moves)
                {
                    if (0 <= row + y && row + y < grid.Length
                       && 0 <= col + x && col + x < grid[0].Length
                      && !map[row + y][col + x]
                      && grid[row + y][col + x] == _land)
                    {
                        queue.Enqueue((row + y, col + x));
                        map[row + y][col + x] = true;

                    }
                }
            }

        }
    }
}