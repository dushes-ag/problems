using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/decode-string/
public class DecodeString
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string s, string expected)
    {
        //act
        var result = new Solution().DecodeString(s);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                "3[a]2[bc]",
                "aaabcbc"
                },
            new object []{
                "3[a2[c]]",
                "accaccacc"
                }
        };
    }

    public class Solution
    {
        public string DecodeString(string s)
        {
            return DecodeFrom(s, 0).result;
        }

        private (string result, int processedIndex) DecodeFrom(string s, int index)
        {
            var result = string.Empty;
            int count = 0;
            while (index < s.Length)
            {
                if (Char.IsLetter(s[index]))
                {
                    result += s[index];
                    index++;
                    continue;
                }
                if (char.IsDigit(s[index]))
                {
                    count = count * 10 + (int)Char.GetNumericValue(s, index);
                    index++;
                    continue;
                }
                if (s[index] == '[')
                {
                    var (str, processedIndex) = DecodeFrom(s, index + 1);
                    while (count > 0)
                    {
                        result += str;
                        count--;
                    }
                    index = processedIndex + 1;
                    continue;
                }
                break;
            }

            return (result, index);
        }
    }
}