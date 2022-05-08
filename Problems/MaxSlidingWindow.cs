using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/sliding-window-maximum/
public class MaxSlidingWindow
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int k, int[] expected)
    {
        //act
        var result = new Solution().MaxSlidingWindow(nums, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,3,-1,-3,5,3,6,7},
                3,
                new []{3,3,5,5,6,7}}
        };
    }

    public class Solution
    {
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            var leftMax = new int[nums.Length];
            var rightMax = new int[nums.Length];
            var result = new int[nums.Length - k + 1];
            for (var i = 0; i < nums.Length; i += k)
            {
                var jMax = i + k <= nums.Length ? k : nums.Length % k;
                for (var j = 0; j < jMax; j++)
                {
                    leftMax[i + j] = j == 0
                        ? nums[i + j]
                        : Math.Max(leftMax[i + j - 1], nums[i + j]);
                    rightMax[i + jMax - j - 1] = j == 0
                        ? nums[i + jMax - j - 1]
                        : Math.Max(rightMax[i + jMax - j], nums[i + jMax - j - 1]);
                }
            }
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Math.Max(rightMax[i], leftMax[i + k - 1]);
            }
            return result;
        }
    }
}