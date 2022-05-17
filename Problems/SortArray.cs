using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/sort-an-array/
public class SortArray
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int[] expected)
    {
        //act
        var result = new Solution().SortArray(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{5,2,3,1},
                new int[]{1,2,3,5}}
        };
    }

    public class Solution
    {
        public int[] SortArray(int[] nums)
        {
            SplitAndMerge(nums, 0, nums.Length - 1);
            return nums;
        }

        private void SplitAndMerge(int[] nums, int start, int finish)
        {
            if (start == finish)
            {
                return;
            }

            var middle = (start + finish) / 2;

            SplitAndMerge(nums, start, middle);
            SplitAndMerge(nums, middle + 1, finish);
            Merge(nums, (start, middle), (middle + 1, finish));
        }
        private void Merge(int[] nums, (int start, int finish) left, (int start, int finish) right)
        {
            var i = left.start;
            var j = right.start;
            var temp = new List<int>();
            while (i <= left.finish && j <= right.finish)
            {
                if (nums[j] < nums[i])
                {
                    temp.Add(nums[j]);
                    j++;
                }
                else
                {
                    temp.Add(nums[i]);
                    i++;
                }
            }
            while (i <= left.finish)
            {
                temp.Add(nums[i]);
                i++;
            }
            while (j <= right.finish)
            {

                temp.Add(nums[j]);
                j++;
            }

            for (var k = left.start; k <= right.finish; k++)
            {
                nums[k] = temp[k - left.start];
            }
        }
    }
}