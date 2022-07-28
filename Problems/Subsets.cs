using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/subsets/
public class Subsets
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, List<IList<int>> expected)
    {
        //act
        var result = new Solution().Subsets(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int []
                {
                    1,2,3
                },
                new List<IList<int>>
                {
                    new List<int>{1},
                    new List<int>{1,2},
                    new List<int>{2},
                    new List<int>{1,3},
                    new List<int>{1,2,3},
                    new List<int>{2,3},
                    new List<int>{3},
                    new List<int>{}
                }
            }
        };
    }

    public class Solution
    {
        public IList<IList<int>> Subsets(int[] nums)
        {
            var result = new List<IList<int>>();

            for (var i = 0; i < nums.Length; i++)
            {
                result.AddRange(result.Select(_ =>
                    {
                        var newList = new List<int>(_);
                        newList.Add(nums[i]);
                        return newList;
                    }).ToList());
                result.Add(new List<int> { nums[i] });
            }

            result.Add(new List<int>());

            return result;
        }
    }
}