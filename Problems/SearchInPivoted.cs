using Xunit;

namespace Problems;

///https://leetcode.com/problems/search-in-rotated-sorted-array/
public class SearchInPivoted
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int target, int expected)
    {
        //act
        var result = new Solution().Search(nums, target);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{4,5,6,7,8,1,2,3},
                8,
                4},
                new object []{
                new int[]{3,5,1},
                3,
                0}
        };
    }

    public class Solution
    {
        const int NOT_FOUND = -1;
        public int Search(int[] nums, int target)
        {
            var start = 0;
            var finish = nums.Length - 1;
            while (start <= finish)
            {
                var middle = (start + finish) / 2;
                if (nums[middle] == target)
                {
                    return middle;
                }
                var leftRotated = nums[start] > nums[middle];
                var rightRotated = nums[middle] > nums[finish];

                if (!leftRotated)
                {
                    if (nums[start] > target || nums[middle] < target)
                    {
                        start = middle + 1;
                    }
                    else if (nums[start] <= target)
                    {
                        finish = middle - 1;
                    }
                }
                if (!rightRotated)
                {
                    if (nums[finish] < target || target < nums[middle])
                    {
                        finish = middle - 1;
                    }
                    else if (nums[finish] >= target)
                    {
                        start = middle + 1;
                    }
                }
            }
            return NOT_FOUND;
        }
    }
}