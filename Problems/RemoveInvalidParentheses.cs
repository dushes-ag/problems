using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/remove-invalid-parentheses/
public class RemoveInvalidParentheses
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string s, string[] expected)
    {
        //act
        var result = new Solution().RemoveInvalidParentheses(s);

        //assert
        Assert.Equal(expected.OrderBy(_ => _), result.OrderBy(_ => _));
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                "()())()",
                new[]{"(())()","()()()"}
            },
            new object[]{
                "(a)())()",
                new[]{"(a())()","(a)()()"}
            },
            new object[]{
                ")(",
                new[]{""}
            },
            new object[]{
                "(",
                new[]{""}
            },
            new object[]{
                "(()",
                new[]{"()"}
            },
            new object[]{
                ")()(",
                new[]{"()"}
            },
            new object[]{
                "((",
                new[]{""}
            },
            new object[]{
                "(((k()((",
                new[]{"k()","(k)"}
            }
        };
    }

    public class Solution
    {
        private HashSet<string> _result = new();
        public IList<string> RemoveInvalidParentheses(string s)
        {
            var openToRemove = 0;
            var closedToRemove = 0;
            foreach (var c in s)
            {
                if (c == '(') { openToRemove++; }
                if (c == ')')
                {
                    if (openToRemove > 0) { openToRemove--; }
                    else { closedToRemove++; }
                }
            }
            DFS(s, 0, openToRemove, closedToRemove, 0, 0);
            return _result.ToList();
        }

        private void DFS(string s, int index, int openToRemove, int closeToRemove, int open, int close)
        {
            if (index == s.Length)
            {
                if (openToRemove == 0 && closeToRemove == 0 && open == close)
                {
                    _result.Add(s);
                }
                return;
            }

            var c = s[index];
            if (c == '(')
            {
                DFS(s, index + 1, openToRemove, closeToRemove, open + 1, close);
                if (openToRemove > 0)
                {
                    DFS(s.Remove(index, 1), index, openToRemove - 1, closeToRemove, open, close);
                }
            }
            else if (c == ')')
            {
                if (open > close)
                {
                    DFS(s, index + 1, openToRemove, closeToRemove, open, close + 1);
                }
                if (closeToRemove > 0)
                {
                    DFS(s.Remove(index, 1), index, openToRemove, closeToRemove - 1, open, close);
                }
            }
            else
            {
                DFS(s, index + 1, openToRemove, closeToRemove, open, close);
            }
        }
    }
}