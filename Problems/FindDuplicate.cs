using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/find-the-duplicate-number/
public class FindDuplicate
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int expected)
    {
        //act
        var result = new Solution().FindDuplicate(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,2,1},
                1},

            new object []{
                new int[]{1, 3, 4, 2, 3},
                3}
        };
    }

    public class Solution
    {
        public int FindDuplicate(int[] nums)
        {
            var min = 1;
            var max = nums.Length - 1;
            var result = -1;

            while (min <= max)
            {
                var point = (min + max) / 2;
                if (nums.Count(_ => _ <= point) > point)
                {
                    result = point;
                    max = point - 1;
                }
                else
                {
                    min = point + 1;
                }
            }
            return result;
        }
    }
}