using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/interval-list-intersections/
public class IntervalIntersection
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] firstList, int[][] secondList, int[][] expected)
    {
        //act
        var result = new Solution().IntervalIntersection(firstList, secondList);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int[][] {new[]{0,2}, new[]{5,10}, new[]{13,23}, new[]{24,25}},
                new int[][] {new[]{1,5}, new[]{8,12}, new[]{15,24}, new[]{25,26}},
                new int[][] {new[]{1,2}, new[]{5,5}, new[]{8,10}, new[]{15,23}, new[]{24,24}, new[]{25,25}}
            }
        };
    }

    public class Solution
    {
        public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
        {
            var result = new List<int[]>();
            var ptr1 = 0;
            var ptr2 = 0;
            while (ptr1 < firstList.Length && ptr2 < secondList.Length)
            {
                if (firstList[ptr1][1] < secondList[ptr2][1])
                {
                    if (firstList[ptr1][1] >= secondList[ptr2][0])
                    {
                        result.Add(new int[] { Math.Max(firstList[ptr1][0], secondList[ptr2][0]), Math.Min(firstList[ptr1][1], secondList[ptr2][1]) });

                    }
                    ptr1++;
                    continue;
                }
                if (firstList[ptr1][0] <= secondList[ptr2][1])
                {
                    result.Add(new int[] { Math.Max(firstList[ptr1][0], secondList[ptr2][0]), Math.Min(firstList[ptr1][1], secondList[ptr2][1]) });
                }
                ptr2++;
            }

            return result.ToArray();
        }
    }
}