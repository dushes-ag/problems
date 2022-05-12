using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/factor-combinations/
public class GetFactors
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int n, int[][] expected)
    {
        //act
        var result = new Solution().GetFactors(n);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                1,
                new int[][]{}},
            new object []{
                12,
                new int[][]{new int[]{2,6}, new int []{2,2,3}, new int[]{3,4}}}
        };
    }

    public class Solution
    {
        public IList<IList<int>> GetFactors(int n)
        {
            return Impl(n, 2);
        }

        private List<IList<int>> Impl(int n, int min)
        {
            var result = new List<IList<int>>();

            for (var i = min; i <= (int)Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    result.Add(new List<int> { i, n / i });
                    foreach (var sub in Impl(n / i, i))
                    {
                        var res = new List<int> { i };
                        res.AddRange(sub);
                        result.Add(res);
                    }
                }
            }
            return result;
        }
    }
}