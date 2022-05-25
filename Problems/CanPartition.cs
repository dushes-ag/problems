using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/partition-equal-subset-sum
public class CanPartition
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, bool expected)
    {
        //act
        var result = new Solution().CanPartition(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,5,11,5},
                true},
            new object []{
                new int[]{11,2,3,5},
                false},
            new object []{
                new int[]{1,2,5},
                false}
        };
    }

    public class Solution
    {
        public bool CanPartition(int[] nums)
        {
            var sum = nums.Sum();
            if (sum % 2 == 1)
            {
                return false;
            }

            var dp = new bool[sum / 2 + 1];
            dp[0] = true;
            foreach (var num in nums)
            {
                for (var j = dp.Length - 1; j >= num; j--)
                {
                    dp[j] |= dp[j - num];
                }
            }
            return dp[dp.Length - 1];
        }
    }
}