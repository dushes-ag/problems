using Xunit;

namespace Problems;

///https://leetcode.com/problems/trapping-rain-water/
public class Trap
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums, int expected)
    {
        //act
        var result = new Solution().Trap(nums);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{0,1,0,2,1,0,1,3,2,1,2,1},
                6},
                new object []{
                new int[]{4,2,3},
                1}
        };
    }

    public class Solution
    {
        public int Trap(int[] height)
        {
            var result = 0;
            var leftPointer = 0;
            var rightPointer = height.Length - 1;
            var maxLeft = 0;
            var maxRight = 0;
            while (leftPointer < rightPointer)
            {
                if (height[leftPointer] < height[rightPointer])
                {
                    if (height[leftPointer] < maxLeft)
                    {
                        result += maxLeft - height[leftPointer];
                    }
                    else
                    {
                        maxLeft = height[leftPointer];
                    }
                    leftPointer++;
                }
                else
                {
                    if (height[rightPointer] < maxRight)
                    {
                        result += maxRight - height[rightPointer];
                    }
                    else
                    {
                        maxRight = height[rightPointer];
                    }
                    rightPointer--;
                }
            }
            return result;
        }
    }
}