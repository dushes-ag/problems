using Xunit;

namespace Problems;

///https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/
public class MinRotatedSortedArray
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int expected)
    {
        //act
        var result = new Solution().FindMin(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{4,5,6,7,0,1,2},
                0},
                new object []{
                new int[]{1},
                1}
        };
    }

    public class Solution
    {
        public int FindMin(int[] nums)
        {
            if (nums[0] <= nums[nums.Length - 1])
            {
                return nums[0];
            }

            var start = 0;
            var end = nums.Length - 1;
            while (true)
            {
                var middle = (start + end) / 2;
                if (middle > 0 && nums[middle - 1] > nums[middle])
                {
                    return nums[middle];
                }
                if (middle < nums.Length - 2 && nums[middle] > nums[middle + 1])
                {
                    return nums[middle + 1];
                }

                if (nums[start] > nums[middle])
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
            }
        }
    }
}