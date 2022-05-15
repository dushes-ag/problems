using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/maximum-subarray/
public class MaxSubArray
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int expected)
    {
        //act
        var result = new Solution().MaxSubArray(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,2,-1,-2,3,1,-2,1},
                4}
        };
    }

    public class Solution
    {
        public int MaxSubArray(int[] nums)
        {
            var sum = nums[0];
            var maxSum = nums[0];
            for (var i = 1; i < nums.Length; i++)
            {
                sum = Math.Max(nums[i], nums[i] + sum);
                maxSum = Math.Max(maxSum, sum);
            }

            return maxSum;
        }
    }
}