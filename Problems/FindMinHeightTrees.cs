using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/minimum-height-trees/
public class FindMinHeightTrees
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int n, int[][] edges, int[] expected)
    {
        //act
        var result = new Solution().FindMinHeightTrees(n, edges);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                4,
                new int[][] { new int[]{1,0}, new int[]{1,2}, new int[]{1,3}},
                new int[]{1}},
            new object[]{
                6,
                new int[][] { new int[]{3,0}, new int[]{3,1}, new int[]{3,2}, new int[]{3,4}, new int[]{5,4}},
                new int[]{3,4}}
        };
    }

    public class Solution
    {
        public IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            var relations = Enumerable.Range(0, n).ToDictionary(_ => _, _ => new HashSet<int>());
            foreach (var edge in edges)
            {
                relations[edge[0]].Add(edge[1]);
                relations[edge[1]].Add(edge[0]);
            }

            var leaves = new Queue<int>();
            for (var i = 0; i < n; i++)
            {
                if (IsLeaf(relations, i))
                {
                    leaves.Enqueue(i);
                }
            }
            while (relations.Count > 2)
            {
                var layerSize = leaves.Count();
                for (var i = 0; i < layerSize; i++)
                {
                    var item = leaves.Dequeue();
                    var parent = relations[item].Single();
                    relations.Remove(item);
                    relations[parent].Remove(item);
                    if (IsLeaf(relations, parent))
                    {
                        leaves.Enqueue(parent);
                    }
                }
            }

            return relations.Keys.ToList();
        }

        private static bool IsLeaf(Dictionary<int, HashSet<int>> relations, int i)
        {
            return relations[i].Count == 1;
        }
    }
}