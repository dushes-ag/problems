using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/surrounded-regions/
public class SurroundedRegions
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(char[][] grid, char[][] expected)
    {
        //act
        new Solution().Solve(grid);

        //assert
        Assert.Equal(grid, expected);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new char[][] {
                    new[]{'X','X','X','X'},
                    new[]{'X','O','O','X'},
                    new[]{'X','X','O','X'},
                    new[]{'X','O','X','X'},
                    },
                 new char[][] {
                    new[]{'X','X','X','X'},
                    new[]{'X','X','X','X'},
                    new[]{'X','X','X','X'},
                    new[]{'X','O','X','X'},
                    }
            }
        };
    }

    public class Solution
    {
        private (int x, int y)[] _steps = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        private char _o = 'O';
        private char _x = 'X';
        private char _q = '?';

        public void Solve(char[][] board)
        {
            var m = board.Length;
            var n = board[0].Length;
            if (n == 1 || m == 1)
            {
                return;
            }
            for (var i = 1; i < m - 1; i++)
            {
                for (var j = 1; j < n - 1; j++)
                {
                    if (board[i][j] == _o)
                    {
                        board[i][j] = _q;
                    }
                }
            }

            for (var i = 0; i < m; i++)
            {
                DFS(board, i, 0);
                DFS(board, i, n - 1);
            }

            for (var j = 0; j < n; j++)
            {
                DFS(board, 0, j);
                DFS(board, m - 1, j);
            }

            for (var i = 1; i < m - 1; i++)
            {
                for (var j = 1; j < n - 1; j++)
                {
                    if (board[i][j] == _q)
                    {
                        board[i][j] = _x;
                    }
                }
            }
        }

        private void DFS(char[][] board, int i, int j)
        {
            if (board[i][j] == _x)
            {
                return;
            }

            var m = board.Length;
            var n = board[0].Length;

            foreach (var (x, y) in _steps)
            {
                var row = i + y;
                var col = j + x;
                if (row < 0 || row > m - 1 || col < 0 || col > n - 1)
                {
                    continue;
                }
                if (board[row][col] == _q)
                {
                    board[row][col] = _o;
                    DFS(board, row, col);
                }
            }
        }
    }
}