using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/minimum-window-substring
public class MinWindow
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string s, string t, string expected)
    {
        //act
        var result = new Solution().MinWindow(s, t);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                "ADOBECODEBANC",
                "ABC",
                "BANC"},
            new object []{
                "a",
                "a",
                "a"},
            new object []{
                "cabwefgewcwaefgcf",
                "cae",
                "cwae"}
        };
    }

    public class Solution
    {
        public string MinWindow(string s, string t)
        {
            if (s.Length == 0 || t.Length == 0 || t.Length > s.Length)
            {
                return string.Empty;
            }

            var result = string.Empty;
            var left = 0;
            var right = -1;

            var tMap = t.GroupBy(_ => _).ToDictionary(_ => _.Key, _ => _.Count());

            var windowMap = new Dictionary<char, int>();
            while (right < s.Length - 1)
            {
                while (right < s.Length -1 && !tMap.All(_ => windowMap.GetValueOrDefault(_.Key) >= _.Value))
                {
                    right++;
                    if (!tMap.ContainsKey(s[right]))
                    {
                        continue;
                    }
                    windowMap[s[right]] = windowMap.GetValueOrDefault(s[right]) + 1;
                }
                while (left <= right - t.Length + 1 && tMap.All(_ => windowMap.GetValueOrDefault(_.Key) >= _.Value))
                {
                    if (result == string.Empty || right - left + 1 < result.Length)
                    {
                        result = s.Substring(left, right - left + 1);
                    }
                    if (windowMap.ContainsKey(s[left]))
                    {
                        windowMap[s[left]] -= 1;
                    }

                    left++;
                }
            }

            return result;
        }
    }
}