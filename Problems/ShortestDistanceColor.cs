using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/shortest-distance-to-target-color/
public class ShortestDistanceColor
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void TestShortestDistanceColor(int[] colors, int[][] queries, int[] expected)
    {
        //act
        var result = new SolutionhortestDistanceColor().ShortestDistanceColor(colors, queries);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,1,2,1,3,2,2,3,3},
                new int[][]{new int[]{1,3},new int[]{2,2},new int[]{6,1}},
                new int[]{3,0,3}},
            // new object []{
            //     new int[]{3,1,1,2,3, 3,2,1,2,3, 1,1,3,2,3, 1, 1,1,1,2,2,1,2,2,2,1,1,1,1,2,3,3,3,1,3,2,1,1,2,2,1,3,1,2,1,1,2,2,1,2},
            //     new int[][]{new int[]{15,1}},
            //     new int[]{0}},
            // new object []{
            //     new int[]{2,1,2,2,1},
            //     new int[][]{new int[]{2,1}},
            //     new int[]{1}}
        };
    }
}

public class SolutionhortestDistanceColor
{
    public IList<int> ShortestDistanceColor(int[] colors, int[][] queries)
    {
        var c1 = new List<int>();
        var c2 = new List<int>();
        var c3 = new List<int>();
        var map = new Dictionary<int, List<int>> { { 1, c1 }, { 2, c2 }, { 3, c3 } };
        for (var i = 0; i < colors.Length; i++)
        {
            foreach (var tuple in map)
            {
                if (tuple.Key == colors[i])
                {
                    tuple.Value.Add(i);
                    break;
                }
            }
        }

        return queries.Select(_ => ShortestDistanceColor(map, _[0], _[1])).ToList();
    }

    public int ShortestDistanceColor(Dictionary<int, List<int>> map, int index, int color)
    {
        var colorIdxs = map[color];
        if (!colorIdxs.Any())
            return -1;
        var start = 0;
        var end = colorIdxs.Count - 1;
        while (start <= end)
        {
            var pivot = (start + end) / 2;
            if (colorIdxs[pivot] == index)
                return 0;
            if (colorIdxs[pivot] < index)
            {
                start = pivot + 1;
            }
            else
            {
                end = pivot - 1;
            }
        }
        return Math.Min(GetDistance(colorIdxs[start >= colorIdxs.Count ? colorIdxs.Count - 1 : start], index), GetDistance(colorIdxs[end >= 0 ? end : 0], index));
    }
    private int GetDistance(int index1, int index2) { return Math.Abs(index1 - index2); }
}