using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/find-all-anagrams-in-a-string/
public class FindAnagrams
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string s, string p, IList<int> expected)
    {
        //act
        var result = new Solution().FindAnagrams(s, p);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                "cbaebabacd",
                "abc",
                new List<int>{0,6}
                }
        };
    }

    public class Solution
    {
        public IList<int> FindAnagrams(string s, string p)
        {
            var result = new List<int>();
            if (p.Length > s.Length)
            {
                return result;
            }

            var search = new Map();
            var window = new Map();
            for (var i = 0; i < p.Length; i++)
            {
                search.Add(p[i]);
                window.Add(s[i]);
            }

            window.Remove(s[p.Length - 1]);
            for (var i = p.Length - 1; i < s.Length; i++)
            {
                var c = s[i];
                window.Add(s[i]);
                if (search.Equals(window))
                {
                    result.Add(i - p.Length + 1);
                }
                window.Remove(s[i - p.Length + 1]);

            }

            return result;
        }

        class Map
        {
            private int[] _chars = new int[26];
            public void Add(char c) { _chars[GetIndex(c)] += 1; }
            public void Remove(char c) { _chars[GetIndex(c)] -= 1; }
            public int GetIndex(char c) => (int)c - (int)'a';
            public override bool Equals(object? obj)
            {
                if (obj is not Map map)
                {
                    return false;
                }
                for (var i = 0; i < _chars.Length; i++)
                {
                    if (_chars[i] != map._chars[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}