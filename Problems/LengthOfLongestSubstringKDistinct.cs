using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/longest-substring-with-at-most-k-distinct-characters/
public class LengthOfLongestSubstringKDistinct
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string s, int k, int expected)
    {
        //act
        var result = new Solution().LengthOfLongestSubstringKDistinct(s, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                "eceba",
                2,
                3},
            new object []{
                "ecaa",
                2,
                3},
            new object []{
                "abaccc",
                2,
                4}
        };
    }

    public class Solution
    {
        public int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            if (s.Length == 0 || k == 0)
            {
                return 0;
            }

            var result = 1;
            var left = 0;
            var map = new Dictionary<char, int>();
            for (var i = 0; i < s.Length; i++)
            {
                map[s[i]] = i;
                if (map.Keys.Count == k + 1)
                {
                    var minChar = GetMinChar(map);
                    left = map[minChar] + 1;
                    map.Remove(minChar);
                }

                result = Math.Max(result, i - left + 1);
            }

            return result;
        }

        private char GetMinChar(Dictionary<char, int> map)
        {
            var result = map.First().Key;
            foreach (var tuple in map)
            {
                if (map[tuple.Key] < map[result])
                {
                    result = tuple.Key;
                }
            }
            return result;
        }
    }
}