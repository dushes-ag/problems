using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/critical-connections-in-a-network
public class CriticalConnections
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int n, IList<IList<int>> connections, int[][] expected)
    {
        //act
        var result = new Solution().CriticalConnections(n, connections);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                4,
                new int[][] {new[]{0,1}, new[]{1,2}, new[]{2,0}, new[]{1,3}},
                new int[][] {new[]{1,3}}
            }
        };
    }

    public class Solution
    {
        record Connection
        {
            public int A { get; set; }
            public int B { get; set; }
            public Connection(int a, int b)
            {
                A = Math.Min(a, b);
                B = Math.Max(a, b);
            }
        }
        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            var map = Enumerable.Range(0, n).Select(_ => new List<int>()).ToList();
            var criticalConnections = new HashSet<Connection>();
            foreach (var connection in connections)
            {
                map[connection[0]].Add(connection[1]);
                map[connection[1]].Add(connection[0]);
                criticalConnections.Add(new(connection[0], connection[1]));
            }

            var nodeRanks = new Dictionary<int, int>();

            DFS(connections[0][0], 0);

            int DFS(int item, int rank)
            {
                if (nodeRanks.ContainsKey(item))
                {
                    return nodeRanks[item];
                }
                nodeRanks[item] = rank;

                var minRank = rank + 1;

                foreach (var _ in map[item])
                {
                    if (nodeRanks.ContainsKey(_) && nodeRanks[_] == rank - 1)
                    {
                        continue;
                    }

                    var result = DFS(_, rank + 1);
                    if (result <= rank)
                    {
                        criticalConnections.Remove(new(item, _));
                    }
                    minRank = Math.Min(minRank, result);
                }
                return minRank;
            }
            return criticalConnections.Select(_ => (IList<int>)new[] { _.A, _.B }.ToList()).ToList();
        }
    }
}