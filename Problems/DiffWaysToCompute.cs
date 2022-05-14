using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/different-ways-to-add-parentheses/
public class DiffWaysToCompute
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string expression, int[] expected)
    {
        //act
        var result = new Solution().DiffWaysToCompute(expression);

        //assert
        Assert.Equal(expected.OrderBy(_ => _), result.OrderBy(_ => _));
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                "2-1-1",
                new[]{0,2}
            },
            new object[]{
                "2*3-4*5",
                new[]{-34,-14,-10,-10,10}
            }
        };
    }

    public class Solution
    {
        public IList<int> DiffWaysToCompute(string expression)
        {

            var operations = expression.Split(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, StringSplitOptions.RemoveEmptyEntries).Select(_ => _[0]).ToArray();
            var numbers = expression.Split(new[] { '+', '-', '*' }).Select(_ => int.Parse(_)).ToArray();

            return Dfs(new Span<int>(numbers), new Span<char>(operations));
        }

        private List<int> Dfs(Span<int> numbers, Span<char> operations)
        {
            if (numbers.Length == 0)
            {
                return new();
            }
            if (numbers.Length == 1)
            {
                return new() { numbers[0] };
            }

            var results = new List<int>();
            for (var i = 0; i < operations.Length; i++)
            {
                var left = Dfs(numbers.Slice(0, i + 1), operations.Slice(0, i));
                var right = Dfs(numbers.Slice(i + 1), operations.Slice(i + 1));
                foreach (var l in left)
                {
                    foreach (var r in right)
                    {
                        var operation = operations[i];
                        switch (operation)
                        {
                            case '-': results.Add(l - r); break;
                            case '+': results.Add(l + r); break;
                            case '*': results.Add(l * r); break;
                            default: throw new Exception();
                        }
                    }
                }
            }

            return results;
        }
    }
}