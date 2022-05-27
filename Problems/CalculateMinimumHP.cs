using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/dungeon-game
public class CalculateMinimumHP
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] dungeon, int expected)
    {
        //act
        var result = new Solution().CalculateMinimumHP(dungeon);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int[][] {
                    new[]{-2,-3,3},
                    new[]{-5,-10,1},
                    new[]{10,30,-5}},
                7},
               new object[]{
                new int[][] {
                    new[]{100}},
                1}
        };
    }

    public class Solution
    {
        public int CalculateMinimumHP(int[][] dungeon)
        {
            var dp = Enumerable.Range(0, dungeon[0].Length + 1).Select(_ => int.MaxValue).ToArray();
            for (var i = dungeon.Length - 1; i >= 0; i--)
            {
                for (var j = dungeon[0].Length - 1; j >= 0; j--)
                {
                    var prev = Math.Min(dp[j], dp[j + 1]);
                    if (prev == int.MaxValue)
                    {
                        prev = 1;
                    }
                    dp[j] = (1 + dungeon[i][j] > prev) ? 1 : (prev - dungeon[i][j]);
                }
            }

            return dp[0];
        }
    }
}