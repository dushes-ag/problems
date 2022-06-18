using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/merge-intervals/
public class MergeIntervals
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] intervals, int[][] expected)
    {
        //act
        var result = new Solution().Merge(intervals);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[][]{new int[]{1,3},new int[]{2,6},new int[]{8,10},new int[]{15,18}},
                new int[][]{new int[]{1,6},new int[]{8,10},new int[]{15,18}}
            }};
    }

    public class Solution
    {
        public int[][] Merge(int[][] intervals)
        {
            Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
            var result = new List<int[]>();
            foreach (var interval in intervals)
            {
                if (result.Count == 0 || result[result.Count - 1][1] < interval[0])
                {
                    result.Add(interval);
                }
                else
                {
                    result[result.Count - 1][1] = Math.Max(result[result.Count - 1][1], interval[1]);
                }
            }
            return result.ToArray();
        }
    }
}