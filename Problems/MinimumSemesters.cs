using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/parallel-courses/
public class MinimumSemesters
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int n, int[][] relations, int expected)
    {
        //act
        var result = new Solution().MinimumSemesters(n, relations);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                3,
                new int[][] { new int[]{1,3},new int[]{2,3}},
                2},
            new object[]{
                3,
                new int[][] { new int[]{1,2}, new int[]{2,3}, new int[]{3,1}},
                -1}
        };
    }

    public class Solution
    {
        public int MinimumSemesters(int n, int[][] relations)
        {
            var nextCourses = new Dictionary<int, HashSet<int>>();
            var prevCourses = Enumerable.Range(1, n).ToDictionary(_ => _, _ => new HashSet<int>());
            foreach (var pair in relations)
            {
                prevCourses[pair[1]].Add(pair[0]);

                nextCourses.TryAdd(pair[0], new());
                nextCourses[pair[0]].Add(pair[1]);
            }

            var queue = new Queue<int>();
            foreach (var (id, _) in prevCourses.Where(_ => !_.Value.Any()))
            {
                queue.Enqueue(id);
            }
            var result = 0;
            var itemsCount = queue.Count;
            while (itemsCount > 0)
            {
                result++;
                for (var i = 0; i < itemsCount; i++)
                {
                    var item = queue.Dequeue();
                    if (!nextCourses.ContainsKey(item))
                    {
                        continue;
                    }
                    foreach (var next in nextCourses[item])
                    {
                        prevCourses[next].Remove(item);
                        if (prevCourses[next].Count == 0)
                        {
                            queue.Enqueue(next);
                        }
                    }
                }
                itemsCount = queue.Count;
            }

            return prevCourses.Any(_ => prevCourses[_.Key].Any()) ? -1 : result;
        }
    }
}