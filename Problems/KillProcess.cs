using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/kill-process/
public class KillProcess
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(IList<int> pid, IList<int> ppid, int kill, int[] expected)
    {
        //act
        var result = new Solution().KillProcess(pid, ppid, kill);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int[] {1,3,10,5},
                new int[] {3,0,5,3},
                5,
                new int[]{5,10}
            }
        };
    }
    public class Solution
    {
        public IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill)
        {
            var result = new List<int>();
            var map = pid.Select((id, index) => new { id, parentId = ppid[index] })
            .GroupBy(_ => _.parentId)
            .ToDictionary(_ => _.Key, _ => _.Select(__ => __.id).ToList());

            DFS(map, kill, result);

            return result;
        }

        private void DFS(Dictionary<int, List<int>> map, int kill, List<int> result)
        {
            result.Add(kill);
            if (!map.ContainsKey(kill))
            {
                return;
            }

            foreach (var child in map[kill])
            {
                DFS(map, child, result);
            }
        }
    }
}