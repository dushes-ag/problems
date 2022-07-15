using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/search-a-2d-matrix
public class SearchMatrix
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] intervals, int target, bool expected)
    {
        //act
        var result = new Solution().SearchMatrix(intervals, target);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[][]{new int[]{1,3,5,7},new int[]{10,11,16,20},new int[]{23,30,34,60}},
                3,
                true
            },
            new object []{
                new int[][]{new int[]{1,3,5,7},new int[]{10,11,16,20},new int[]{23,30,34,60}},
                4,
                false
            }};
    }

    public class Solution
    {
        public bool SearchMatrix(int[][] matrix, int target)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;
            if (matrix[0][0] > target || matrix[m - 1][n - 1] < target)
            {
                return false;
            }
            var start = 0;
            var end = m * n - 1;

            while (start <= end)
            {
                var middle = (start + end) / 2;
                var (row, col) = ToIndexes(middle, n);
                if (matrix[row][col] == target)
                {
                    return true;
                }
                if (matrix[row][col] < target)
                {
                    start = middle + 1;
                }
                else
                {
                    end = middle - 1;
                }
            }
            return false;
        }

        private (int row, int col) ToIndexes(int index, int n) => (index / n, index % n);
    }
}