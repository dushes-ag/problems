using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/course-schedule-ii/
public class FindOrder
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int numCourses, int[][] prerequisites, int[] expected)
    {
        //act
        var result = new Solution().FindOrder(numCourses, prerequisites);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                2,
                new int[][] { new int[]{1,0}},
                new int[]{0,1}},
            new object[]{
                2,
                new int[][] { new int[]{1,0}, new int[]{0,1}},
                new int[]{}},
            new object[]{
                4,
                new int[][] { new int[]{1,0}, new int[]{2,0}, new int[]{3,1}, new int[]{3,2}},
                new int[]{0,1,2,3}}
        };
    }

    public class Solution
    {
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            var parentToChild = new Dictionary<int, HashSet<int>>();
            var childToParent = Enumerable.Range(0, numCourses).ToDictionary(_ => _, _ => new HashSet<int>());
            foreach (var pair in prerequisites)
            {
                childToParent[pair[0]].Add(pair[1]);

                parentToChild.TryAdd(pair[1], new());
                parentToChild[pair[1]].Add(pair[0]);
            }

            var parents = childToParent.Where(_ => !_.Value.Any()).Select(_ => _.Key).ToList();
            var result = new List<int>();
            for (var i = 0; i < parents.Count; i++)
            {
                var parent = parents[i];
                result.Add(parent);
                if (!parentToChild.ContainsKey(parent))
                {
                    continue;
                }

                foreach (var childItem in parentToChild[parent])
                {
                    childToParent[childItem].Remove(parent);
                    if (!childToParent[childItem].Any())
                    {
                        parents.Add(childItem);
                    }
                }
            }

            return childToParent.Any(_ => _.Value.Any()) ? new int[] { } : result.ToArray();
        }
    }
}