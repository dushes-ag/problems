using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/meeting-scheduler/
public class MeetingScheduler
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] slots1, int[][] slots2, int duration, int[] expected)
    {
        //act
        var result = new Solution().MinAvailableDuration(slots1, slots2, duration);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[][]{new int[]{10,50},new int[]{60,120},new int[]{140,210}},
                new int[][]{new int[]{0,15},new int[]{60,70}},
                8,
                new int[]{60,68}},

                new object []{
                new int[][]{new int[]{10,50},new int[]{60,120},new int[]{140,210}},
                new int[][]{new int[]{0,15},new int[]{120,130}},
                10,
                new int[]{}}
        };
    }

    public class Solution
    {
        public IList<int> MinAvailableDuration(int[][] slots1, int[][] slots2, int duration)
        {
            Array.Sort(slots1, (a, b) => a[0].CompareTo(b[0]));
            Array.Sort(slots2, (a, b) => a[0].CompareTo(b[0]));

            int pointer1 = 0, pointer2 = 0;
            while (pointer1 < slots1.Length && pointer2 < slots2.Length)
            {
                var maxStart = Math.Max(slots1[pointer1][0], slots2[pointer2][0]);
                var minFinish = Math.Min(slots1[pointer1][1], slots2[pointer2][1]);
                if (minFinish - maxStart >= duration)
                {
                    return new[] { maxStart, maxStart + duration };
                }

                if (slots1[pointer1][1] < slots2[pointer2][1])
                {
                    pointer1++;
                }
                else { pointer2++; }

            }

            return new int[] { };
        }
    }
}