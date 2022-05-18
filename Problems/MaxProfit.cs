using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-transaction-fee/
public class MaxProfit
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] prices, int fee, int expected)
    {
        //act
        var result = new Solution().MaxProfit(prices, fee);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,3,2,8,4,9},
                2,
                8}
        };
    }

    public class Solution
    {
        public int MaxProfit(int[] prices, int fee)
        {
            int? hasStock = null;
            var noStock = 0;
            foreach (var price in prices)
            {
                noStock = hasStock.HasValue ? Math.Max(noStock, hasStock.Value + price - fee) : noStock;
                hasStock = hasStock.HasValue ? Math.Max(hasStock.Value, noStock - price) : -price;
            }

            return noStock;
        }
    }
}