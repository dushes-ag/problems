using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/minimum-number-of-k-consecutive-bit-flips/
public class MinKBitFlips
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int k, int expected)
    {
        //act
        var result = new Solution().MinKBitFlips(nums, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{0,1,0},
                1,
                2},
            new object []{
                new int[]{1,1,0},
                2,
                -1}
        };
    }

    public class Solution
    {
        public int MinKBitFlips(int[] nums, int k)
        {
            var totalFlips = 0;
            var isFlipped = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (i >= k)
                {
                    isFlipped ^= nums[i - k] > 1 ? 1 : 0;
                    nums[i - k] %= 2;
                }

                if ((nums[i] ^ isFlipped) != 1)
                {
                    if (i > nums.Length - k)
                    {
                        return -1;
                    }
                    nums[i] += 2;
                    isFlipped ^= 1;
                    totalFlips++;
                }
            }
            return totalFlips;
        }
    }
}