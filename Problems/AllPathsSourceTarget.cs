using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/all-paths-from-source-to-target/
public class AllPathsSourceTarget
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] heights, List<IList<int>> expected)
    {
        //act
        var result = new Solution().AllPathsSourceTarget(heights);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int [][]
                {
                    new int[]{1,2},
                    new int[]{3},
                    new int[]{3},
                    new int[]{}
                },
                new List<IList<int>>
                {
                    new List<int>{0,1,3},
                    new List<int>{0,2,3}
                }
            }
        };
    }

    public class Solution
    {
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            var result = new List<IList<int>>();

            DFS(graph, 0, result, new List<int> { 0 });

            return result;
        }

        private void DFS(int[][] graph, int index, IList<IList<int>> result, List<int> currentPath)
        {
            if (index == graph.Length - 1)
            {
                result.Add(currentPath.ToList());
                return;
            }

            foreach (var nextIndex in graph[index])
            {
                currentPath.Add(nextIndex);
                DFS(graph, nextIndex, result, currentPath);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }
}