using System;
using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/number-of-provinces/
public class FindCircleNum
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] isConnected, int expected)
    {
        //act
        var result = new Solution().FindCircleNum(isConnected);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[][]{new int[]{1,1,0}, new int[]{1,1,0}, new int[]{0,0,1}},
                2}
        };
    }

    public class Solution
    {
        public int FindCircleNum(int[][] isConnected)
        {
            var result = 0;
            var map = new bool[isConnected.Length];
            for (var i = 0; i < isConnected.Length; i++)
            {
                if (!map[i])
                {
                    result++;
                    BFS(isConnected, map, i);
                }
            }
            return result;
        }
        private void BFS(int[][] isConnected, bool[] map, int i)
        {
            var queue = new Queue<int>();
            queue.Enqueue(i);
            map[i] = true;
            while (queue.Count > 0)
            {
                i = queue.Dequeue();
                for (var j = 0; j < isConnected.Length; j++)
                {
                    if (!map[j] && isConnected[i][j] == 1)
                    {
                        queue.Enqueue(j);
                        map[j] = true;
                    }
                }
            }
        }
    }
}