using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/maximum-size-subarray-sum-equals-k/
public class MaxSumarray
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int k, int expected)
    {
        //act
        var result = new Solution().MaxSubArrayLen(nums, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,-1,5,-2,3},
                3,
                4}
        };
    }

    public class Solution
    {
        public int MaxSubArrayLen(int[] nums, int k)
        {
            var prefixSum = 0;
            var indices = new Dictionary<int, int>();
            var result = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                prefixSum += nums[i];
                if (prefixSum == k)
                {
                    result = i + 1;
                }
                if (indices.ContainsKey(prefixSum - k))
                {
                    result = Math.Max(result, i - indices[prefixSum - k]);
                }

                if (!indices.ContainsKey(prefixSum))
                    indices.Add(prefixSum, i);
            }
            return result;
        }
    }
}