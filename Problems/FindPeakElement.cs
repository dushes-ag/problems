using Xunit;

namespace Problems;

///https://leetcode.com/problems/find-peak-element/
public class FindPeakElement
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int expected)
    {
        //act
        var result = new Solution().FindPeakElement(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,2,3,1},
                2}
        };
    }

    public class Solution
    {
        public int FindPeakElement(int[] nums)
        {
            var start = 0;
            var end = nums.Length - 1;
            while (true)
            {
                var middle = (start + end) / 2;
                if ((middle == 0 || nums[middle - 1] < nums[middle]) && (middle == nums.Length - 1 || nums[middle] > nums[middle + 1]))
                {
                    return middle;
                }
                if (middle == 0 || nums[middle - 1] < nums[middle])
                {
                    start = middle + 1;
                }
                else
                {
                    end = middle - 1;
                }
            }
        }
    }
}