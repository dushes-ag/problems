using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/palindrome-partitioning
public class Partition
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string expression, string[][] expected)
    {
        //act
        var result = new Solution().Partition(expression);

        //assert
        Assert.Equal(expected.OrderBy(_ => _.Length), result.OrderBy(_ => _.Count));
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                "aab",
                new[]{new[]{"a", "a", "b"},new[]{"aa","b"}}
            },
            new object[]{
                "a",
                new[]{new[]{"a"}}
            },
            new object[]{
                "fff",
                new[]{new[]{"f","f","f"},new[]{"f","ff"},new[]{"ff","f"},new[]{"fff"}}
            }
        };
    }

    public class Solution
    {
        public IList<IList<string>> Partition(string s)
        {
            var result = new List<IList<string>>();

            result.Add(new List<string>());

            for (var i = 0; i < s.Length; i++)
            {
                var copies = result.Select(_ => _.ToList()).ToList();

                result.ForEach(_ => _.Add(s[i].ToString()));

                var hash = new HashSet<string>();
                for (var j = 0; j < copies.Count; j++)
                {
                    result.AddRange(BuildPalindroms(copies[j], s[i].ToString(), hash));
                }

            }

            return result;
        }

        private IEnumerable<IList<string>> BuildPalindroms(List<string> palindromes, string v, HashSet<string> hash)
        {
            var result = new List<IList<string>>();

            for (var i = palindromes.Count - 1; i >= 0; i--)
            {
                v = palindromes[i] + v;
                if (!IsPalindrom(v))
                {
                    continue;
                }
                var newPalindromes = palindromes.Take(i).ToList();
                newPalindromes.Add(v);
                string key = GetKey(newPalindromes);
                if (!hash.Contains(key))
                {
                    hash.Add(key);
                    result.Add(newPalindromes);
                }
            }

            return result;
        }

        private string GetKey(IList<string> strs)
        {
            return strs.Aggregate("", (acc, item) => acc + item.Length);
        }

        private bool IsPalindrom(string s)
        {
            for (var i = 0; i < s.Length / 2; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}