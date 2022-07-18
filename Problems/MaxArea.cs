using System;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/container-with-most-water/
public class MaxArea
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] height, int expected)
    {
        //act
        var result = new Solution().MaxArea(height);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,8,6,2,5,4,8,3,7},
                49}
        };
    }

    public class Solution
    {
        public int MaxArea(int[] height)
        {
            var p1 = 0;
            var p2 = height.Length - 1;
            var volume = 0;
            while (p1 < p2)
            {
                volume = Math.Max(volume, (p2 - p1) * Math.Min(height[p1], height[p2]));
                if (height[p1] < height[p2])
                {
                    p1++;
                }
                else { p2--; }
            }
            return volume;
        }
    }
}