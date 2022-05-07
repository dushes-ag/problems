using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/max-consecutive-ones-iii/
public class LongestOnes
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int k, int expected)
    {
        //act
        var result = new Solution().LongestOnes(nums, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,1,1,0,0,0,1,1,1,1,0},
                2,
                6},
            new object []{
                new int[]{0,0,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,1},
                3,
                10}
        };
    }

    public class Solution
    {
        public int LongestOnes(int[] nums, int k)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            var zero = 0;
            var result = 0;
            var zeroesIndexes = new List<int>();
            var left = 0;

            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] == zero)
                {
                    zeroesIndexes.Add(i);
                }
                if (zeroesIndexes.Count > k)
                {
                    left = zeroesIndexes[0] + 1;
                    zeroesIndexes.RemoveAt(0);
                }
                result = Math.Max(result, i - left + 1);
            }

            return result;
        }
    }
}