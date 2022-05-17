using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/count-of-smaller-numbers-after-self/
public class CountSmaller
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int[] expected)
    {
        //act
        var result = new Solution().CountSmaller(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{5,2,6,1},
                new int[]{2,1,1,0}},
             new object []{
                 new int[]{-1,-1},
                 new int[]{0,0}}
        };
    }

    public class Solution
    {
        private int[] _nums = null!;
        private int[] _indices = null!;
        private int[] _result = null!;
        public IList<int> CountSmaller(int[] nums)
        {
            _nums = nums;
            _result = new int[nums.Length];
            _indices = new int[nums.Length];
            for (var i = 0; i < nums.Length; i++)
            {
                _indices[i] = i;
            }
            SplitAndMerge(0, nums.Length - 1);
            return _result;
        }

        private void SplitAndMerge(int start, int finish)
        {
            if (start == finish)
            {
                return;
            }

            var middle = (start + finish) / 2;

            SplitAndMerge(start, middle);
            SplitAndMerge(middle + 1, finish);
            Merge((start, middle), (middle + 1, finish));
        }
        private void Merge((int start, int finish) left, (int start, int finish) right)
        {
            var i = left.start;
            var j = right.start;
            var temp = new List<int>();
            while (i <= left.finish && j <= right.finish)
            {
                if (_nums[_indices[i]] <= _nums[_indices[j]])
                {
                     _result[_indices[i]] += j - left.finish - 1;
                    temp.Add(_indices[i]);
                    i++;
                }
                else
                {
                    temp.Add(_indices[j]);
                    j++;
                }
            }
            while (i <= left.finish)
            {
                _result[_indices[i]] += j - left.finish - 1;
                temp.Add(_indices[i]);
                i++;
            }
            while (j <= right.finish)
            {
                temp.Add(_indices[j]);
                j++;
            }

            for (var k = left.start; k <= right.finish; k++)
            {
                _indices[k] = temp[k - left.start];
            }
        }
    }
}