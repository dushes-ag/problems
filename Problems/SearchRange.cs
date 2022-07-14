using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/
public class SearchRange
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int target, int[] expected)
    {
        //act
        var result = new Solution().SearchRange(nums, target);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{5,7,7,8,8,10},
                8,
                new int[]{3,4}}
        };
    }

    public class Solution
    {
        public int[] SearchRange(int[] nums, int target)
        {
            var start = 0;
            var end = nums.Length - 1;
            while (start <= end)
            {
                var middle = (start + end) / 2;

                if (nums[middle] == target)
                {
                    return new[]{
                    FindFirst(nums, middle),
                    FindLast(nums, middle)
                };
                }
                else if (nums[middle] > target)
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
            }
            return new[] { -1, -1 };
        }

        private int FindFirst(int[] nums, int index)
        {
            var start = 0;
            var end = index;

            while (start <= end)
            {
                var middle = (start + end) / 2;

                if (nums[middle] == nums[index] && (middle == 0 || nums[middle - 1] < nums[index]))
                {
                    return middle;
                }
                else if (nums[middle] == nums[index])
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
            }
            return index;
        }


        private int FindLast(int[] nums, int index)
        {
            var start = index;
            var end = nums.Length - 1;

            while (start <= end)
            {
                var middle = (start + end) / 2;

                if (nums[middle] == nums[index] && (middle == nums.Length - 1 || nums[middle + 1] > nums[index]))
                {
                    return middle;
                }
                else if (nums[middle] == nums[index])
                {
                    start = middle + 1;
                }
                else
                {
                    end = middle - 1;
                }
            }
            return index;
        }
    }
}