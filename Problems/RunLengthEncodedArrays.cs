using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/product-of-two-run-length-encoded-arrays/
public class RunLengthEncodedArrays
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] encoded1, int[][] encoded2, int[][] expected)
    {
        //act
        var result = new Solution().FindRLEArray(encoded1, encoded2);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[][]{new int[]{1,3}, new int[]{2,3}},
                new int[][]{new int[]{6,3}, new int[]{3,3}},
                new int[][]{new int[]{6,6}}}
        };
    }

    public class Solution
    {
        public IList<IList<int>> FindRLEArray(int[][] encoded1, int[][] encoded2)
        {
            var result = new List<IList<int>>();
            if (encoded1.Length == 0)
            {
                return result;
            }

            var ptr1 = 0;
            var ptr2 = 0;
            var ptrInner1 = 0;
            var ptrInner2 = 0;
            while (ptr1 < encoded1.Length || ptr2 < encoded2.Length)
            {
                var tuple1 = encoded1[ptr1];
                var tuple2 = encoded2[ptr2];
                var number = tuple1[0] * tuple2[0];
                var count = Math.Min(tuple1[1] - ptrInner1, tuple2[1] - ptrInner2);
                if (result.Count != 0 && result[result.Count - 1][0] == number)
                {
                    result[result.Count - 1][1] += count;
                }
                else
                {
                    result.Add(new List<int> { number, count });
                }
                ptrInner1 += count;
                ptrInner2 += count;
                if (ptrInner1 == tuple1[1])
                {
                    ptrInner1 = 0;
                    ptr1++;
                }
                if (ptrInner2 == tuple2[1])
                {
                    ptrInner2 = 0;
                    ptr2++;
                }
            }

            return result;
        }
    }
}