using System;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/maximum-points-you-can-obtain-from-cards/
public class MaxScore
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] cardPoints, int k, int expected)
    {
        //act
        var result = new Solution().MaxScore(cardPoints, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,2,3,4,5,6,1},
                3,
                12},
            new object []{
                new int[]{9,7,7,9,7,7,9},
                7,
                55}
        };
    }

    public class Solution
    {
        public int MaxScore(int[] cardPoints, int k)
        {
            var rightSum = new int[cardPoints.Length + 1];
            for (var i = 1; i <= cardPoints.Length; i++)
            {
                rightSum[i] = cardPoints[cardPoints.Length - i] + rightSum[i - 1];
            }
            var result = 0;
            var leftSum = 0;
            for (var i = 0; i <= k; i++)
            {
                leftSum += i == 0 ? 0 : cardPoints[i - 1];
                result = Math.Max(result, leftSum + (rightSum[k - i]));
            }
            return result;
        }
    }
}