using System;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/maximal-rectangle/
public class MaximalRectangle
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(char[][] matrix, int expected)
    {
        //act
        var result = new Solution().MaximalRectangle(matrix);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new char[][] {
                    new[]{'1','0','1','0','0'},
                    new[]{'1','0','1','1','1'},
                    new[]{'1','1','1','1','1'},
                    new[]{'1','0','0','1','0'},
                    },
                6
            }
        };
    }

    class Solution {

    public int MaximalRectangle(char[][] matrix) {
        if (matrix.Length == 0)
            return 0;
        int m = matrix.Length;
        int n = matrix[0].Length;

        int[] left = new int[n]; // initialize left as the leftmost boundary possible
        int[] right = new int[n];
        int[] height = new int[n];

        Array.Fill(right, n); // initialize right as the rightmost boundary possible

        int result = 0;
        for (int i = 0; i < m; i++) {
            int cur_left = 0, cur_right = n;
            // update height
            for (int j = 0; j < n; j++) {
                if (matrix[i][j] == '1')
                    height[j]++;
                else
                    height[j] = 0;
            }
            // update left
            for (int j = 0; j < n; j++) {
                if (matrix[i][j] == '1')
                    left[j] = Math.Max(left[j], cur_left);
                else {
                    left[j] = 0;
                    cur_left = j + 1;
                }
            }
            // update right
            for (int j = n - 1; j >= 0; j--) {
                if (matrix[i][j] == '1')
                    right[j] = Math.Min(right[j], cur_right);
                else {
                    right[j] = n;
                    cur_right = j;
                }
            }
            // update area
            for (int j = 0; j < n; j++) {
                result = Math.Max(result, (right[j] - left[j]) * height[j]);
            }
        }
        return result;
    }
}
}