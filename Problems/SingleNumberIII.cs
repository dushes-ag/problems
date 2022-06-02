using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/single-number-iii/
public class SingleNumberIII
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int[] expected)
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
                new int[]{1,2,1,3,2,5},
                new int []{5,3}},
            new object[]{
                new int[]{1,0},
                new int[]{0,1}},
        };
    }

    public class Solution
    {
        public int[] SingleNumber(int[] nums)
        {
            var xor = 0;
            foreach (var num in nums)
            {
                xor ^= num;
            }
            var tmp = xor;
            var toCheck = 1;
            while ((tmp & toCheck) != toCheck)
            {
                toCheck <<= 1;
            }
            foreach (var num in nums)
            {
                if ((num & toCheck) == toCheck)
                {
                    xor ^= num;
                }
            }

            return new int[] { xor, xor ^ tmp };
        }
    }
}