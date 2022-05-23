using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/maximal-square/
public class MaximalSquare
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(char[][] matrix, int expected)
    {
        //act
        var result = new Solution().MaximalSquare(matrix);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new char[][] {new[]{'1','0','1','0','0'}, new[]{'1','0','1','1','1'}, new[]{'1','1','1','1','1'}, new[]{'1','0','0','1','0'}},
                4
            }
        };
    }

    public class Solution
    {
        private char[][] _matrix = null!;
        private int _rowsNum = 0;
        private int _colsNum = 0;
        private Dictionary<(int row, int col), int> _cache = new();

        public int MaximalSquare(char[][] matrix)
        {
            _matrix = matrix;
            _rowsNum = matrix.Length;
            _colsNum = matrix[0].Length;

            var result = 0;
            for (var i = 0; i < _rowsNum; i++)
            {
                for (var j = 0; j < _colsNum; j++)
                {
                    result = Math.Max(result, MaximalSquare(i, j));
                }
            }
            return result * result;
        }

        private int MaximalSquare(int row, int col)
        {
            if (_cache.ContainsKey((row, col)))
            {
                return _cache[(row, col)];
            }

            var result = 0;
            if (row == _rowsNum - 1 || col == _colsNum - 1 || _matrix[row][col] == '0')
            {
                result = _matrix[row][col] == '0' ? 0 : 1;
            }
            else
            {
                result = new int[] { MaximalSquare(row, col + 1), MaximalSquare(row + 1, col), MaximalSquare(row + 1, col + 1) }.Min() + 1;
            }

            _cache[(row, col)] = result;
            return result;
        }
    }
}