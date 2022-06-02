using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/single-number
public class SingleNumber
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int expected)
    {
        //act
        var result = new Solution().SingleNumber(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int[]{2,2,1},
                1},
            new object[]{
                new int[]{1},
                1},
        };
    }

    public class Solution
    {
        public int SingleNumber(int[] nums)
        {
            var result = 0;
            foreach (var num in nums)
            {
                result ^= num;
            }
            return result;
        }
    }
}