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
            return Turn(nums, 0, nums.Length - 1, true) >= 0;
        }

        public int Turn(int[] nums, int start, int finish, bool add)
        {
            if (start == finish)
            {
                return nums[start] * (add ? 1 : -1);
            }

            return Math.Max(
                nums[start] - Turn(nums, start + 1, finish, !add),
                nums[finish] - Turn(nums, start, finish - 1, !add)
                );
        }
    }
}