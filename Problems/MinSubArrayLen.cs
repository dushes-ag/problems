using System;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/minimum-size-subarray-sum/
public class MinSubArrayLen
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int target, int[] nums, int expected)
    {
        //act
        var result = new Solution().MinSubArrayLen(target, nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                7,
                new int[]{2,3,1,2,4,3},
                2}
        };
    }

    public class Solution
    {
        public int MinSubArrayLen(int target, int[] nums)
        {
            var result = int.MaxValue;
            var window = 0;
            var sum = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] >= target)
                {
                    return 1;
                }
                sum += nums[i];
                window++;
                while (sum >= target)
                {
                    result = Math.Min(result, window);
                    sum -= nums[i - window + 1];
                    window--;
                }
            }
            return result == int.MaxValue ? 0 : result;
        }
    }
}