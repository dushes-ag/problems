using Xunit;

namespace Problems;

///https://leetcode.com/problems/subarray-product-less-than-k/
public class NumSubarrayProductLessThanK
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int k, int expected)
    {
        //act
        var result = new Solution().NumSubarrayProductLessThanK(nums, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{10, 5, 2, 6},
                100,
                8}
        };
    }

    public class Solution
    {
        public int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            var result = 0;
            if (k == 0)
            {
                return result;
            }

            var prod = 1;
            var window = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                window++;
                prod *= nums[i];
                while (prod >= k && window > 0)
                {
                    result += window - 1;
                    window--;
                    prod /= nums[i - window];
                }
            }
            result += window * (window + 1) / 2;

            return result;
        }
    }
}