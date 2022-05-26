using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii
public class MaxProfitTwoTimes
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] prices, int expected)
    {
        //act
        var result = new Solution().MaxProfit(prices);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{3,3,5,0,0,3,1,4},
                6},
            new object []{
                new int[]{7,6,4,3,1},
                0}
        };
    }

    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            var outcome = Enumerable.Range(0, 4).Select(_ => int.MinValue).ToArray();
            foreach (var price in prices)
            {
                if (outcome[2] != int.MinValue)
                {
                    outcome[3] = Math.Max(outcome[2] + price, outcome[3]);
                }
                if (outcome[1] != int.MinValue)
                {
                    outcome[2] = Math.Max(outcome[1] - price, outcome[2]);
                }
                if (outcome[0] != int.MinValue)
                {
                    outcome[1] = Math.Max(outcome[0] + price, outcome[1]);
                }
                outcome[0] = Math.Max(-price, outcome[0]);
            }

            return Math.Max(0, outcome.Max());
        }
    }
}