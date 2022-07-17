using Xunit;

namespace Problems;

///https://leetcode.com/problems/backspace-string-compare/
public class BackspaceCompare
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string s, string t, bool expected)
    {
        //act
        var result = new Solution().BackspaceCompare(s, t);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                "ab#c",
                "ad#c",
                true
                }
        };
    }

    public class Solution
    {
        public bool BackspaceCompare(string s, string t)
        {
            var ptr1 = s.Length - 1;
            var ptr2 = t.Length - 1;
            while (true)
            {
                ptr1 = ShiftPtr(s, ptr1);
                ptr2 = ShiftPtr(t, ptr2);

                if (ptr1 == -1 && ptr2 == -1)
                {
                    return true;
                }
                if ((ptr1 >= 0 ? s[ptr1] : null) != (ptr2 >= 0 ? t[ptr2] : null))
                {
                    return false;
                }
                if (ptr1 >= 0) ptr1--;
                if (ptr2 >= 0) ptr2--;
            }
        }
        private int ShiftPtr(string s, int ptr)
        {
            int skip = 0;
            while (ptr >= 0)
            {
                if (s[ptr] == '#')
                {
                    skip++;
                }
                else if (skip > 0)
                {
                    skip--;
                }
                else
                {
                    return ptr;
                }
                ptr--;
            }
            return ptr;
        }
    }
}