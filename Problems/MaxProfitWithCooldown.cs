using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/
public class MaxProfitWithCooldown
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
                new int[]{1,2,3,0,2},
                3}
        };
    }

    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            int? withStock = null;
            int? justSold = null;
            var noStock = 0;
            foreach (var price in prices)
            {
                var prevSold = justSold;

                justSold = withStock.HasValue ? (withStock.Value + price) : justSold;
                withStock = withStock.HasValue ? Math.Max(withStock.Value, noStock - price) : (noStock - price);
                noStock = prevSold.HasValue ? Math.Max(noStock, prevSold.Value) : noStock;
            }

            return justSold.HasValue ? Math.Max(justSold.Value, noStock) : noStock;
        }
    }
}