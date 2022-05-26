using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/minimum-cost-for-tickets
public class MincostTickets
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] days, int[] costs, int expected)
    {
        //act
        var result = new Solution().MincostTickets(days, costs);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,4,6,7,8,20},
                new int[]{2,7,15},
                11},
            new object []{
                new int[]{1,2,3,4,5,6,7,8,9,10,30,31},
                new int[]{2,7,15},
                17}
        };
    }

    public class Solution
    {
        public int MincostTickets(int[] days, int[] costs)
        {
            var dp = new int[367];
            foreach (var day in days)
            {
                dp[day] = 1;
            }
            for (var i = 365; i > 0; i--)
            {
                if (dp[i] == 0)
                {
                    dp[i] = dp[i + 1];
                    continue;
                }
                dp[i] = new[]
                {
                    dp[i+1] + costs[0],
                    dp[Math.Min(i + 7, 366)] + costs[1],
                    dp[Math.Min(i + 30, 366)] + costs[2]
                }.Min();
            }
            return dp[1];
        }
    }
}