using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/number-of-operations-to-make-network-connected/
public class MakeConnected
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int n, int[][] connections, int expected)
    {
        //act
        var result = new Solution().MakeConnected(n, connections);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                4,
                new int[][] {new[]{0,1}, new[]{0,2}, new[]{1,2}},
                1
            }
        };
    }

    public class Solution
    {
        public int MakeConnected(int n, int[][] connections)
        {
            if (connections.Length < n - 1)
            {
                return -1;
            }

            var map = Enumerable.Range(0, n).Select(_ => new List<int>()).ToList();
            foreach (var connection in connections)
            {
                map[connection[0]].Add(connection[1]);
                map[connection[1]].Add(connection[0]);
            }

            var notVisitedItems = Enumerable.Range(0, n).ToList();
            int result = -1;
            while (notVisitedItems.Count > 0)
            {
                result++;
                DFS(notVisitedItems.First(), notVisitedItems, map);
            }
            return result;
        }
        private void DFS(int item, List<int> notVisitedItems, List<List<int>> map)
        {
            if (!notVisitedItems.Contains(item))
            {
                return;
            }

            notVisitedItems.Remove(item);
            foreach (var _ in map[item])
            {
                DFS(_, notVisitedItems, map);
            }
        }
    }
}