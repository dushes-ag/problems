using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/regular-expression-matching/
public class IsMatch
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string s, string p, bool expected)
    {
        //act
        var result = new Solution().IsMatch(s, p);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                "aa",
                "a",
                false
            },
            new object[]{
                "aa",
                "a*",
                true
            },
            new object[]{
                "ab",
                ".*",
                true
            },
            new object[]{
                "a",
                ".*",
                true
            }
        };
    }

    public class Solution
    {
        private string _s;
        private string _p;
        public bool IsMatch(string s, string p)
        {
            _s = s;
            _p = p;

            return IsMatch(0, 0);
        }

        private bool IsMatch(int i, int j)
        {
            if (j == _p.Length)
            {
                return i == _s.Length;
            }

            var isFirstMatched = ((_s.Length - i) > 0 && (_s[i] == _p[j] || _p[j] == '.'));
            if ((_p.Length - j) >= 2 && _p[j + 1] == '*')
            {
                return isFirstMatched && IsMatch(i + 1, j) || IsMatch(i, j + 2);
            }
            else
            {
                return isFirstMatched && IsMatch(i + 1, j + 1);
            }
        }
    }
}