using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/predict-the-winner/
public class PredictTheWinner
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, bool expected)
    {
        //act
        var result = new Solution().PredictTheWinner(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,5,2},
                false},
            new object []{
                new int[]{1,5,233,7},
                true}
        };
    }

    public class Solution
    {
        public bool PredictTheWinner(int[] nums)
        {
            var dp = new int[nums.Length];

            for (var i = nums.Length - 1; i >= 0; i--)
            {
                for (var j = i; j < nums.Length; j++)
                {
                    dp[j] = i == j
                    ? nums[i]
                    : Math.Max(nums[j] - dp[j - 1], nums[i] - dp[j]);
                }
            }

            return dp[nums.Length - 1] >= 0;
        }
    }
}