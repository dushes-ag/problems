using System;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/median-of-two-sorted-arrays/
public class FindMedianSortedArrays
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[] nums1, int[] nums2, double expected)
    {
        //act
        var result = new Solution().FindMedianSortedArrays(nums1, nums2);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{1,2},
                new int[]{3,4},
                2.5},
            new object []{
                new int[]{1,3},
                new int[]{2},
                2},
            new object []{
                new int[]{},
                new int[]{2,3},
                2.5},
            new object []{
                new int[]{1},
                new int[]{2,3,4},
                2.5}
        };
    }

    public class Solution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var longerArray = nums2.Length > nums1.Length ? nums2 : nums1;
            var shorterArray = longerArray == nums1 ? nums2 : nums1;

            var left = 0;
            var right = shorterArray.Length;
            while (true)
            {
                var takeFromShorter = (left + right) / 2;
                var leftShorterValue = takeFromShorter - 1 >= 0 ? shorterArray[takeFromShorter - 1] : int.MinValue;
                var rightShorterValue = takeFromShorter < shorterArray.Length ? shorterArray[takeFromShorter] : int.MaxValue;
                var takeFromLonger = (nums1.Length + nums2.Length) / 2 - takeFromShorter;
                var leftLongerValue = takeFromLonger - 1 >= 0 ? longerArray[takeFromLonger - 1] : int.MinValue;
                var rightLongerValue = takeFromLonger < longerArray.Length ? longerArray[takeFromLonger] : int.MaxValue;
                if (leftShorterValue <= rightLongerValue && rightShorterValue >= leftLongerValue)
                {
                    return (nums1.Length + nums2.Length) % 2 == 1
                        ? Math.Min(rightShorterValue, rightLongerValue)
                        : (double)(Math.Max(leftShorterValue, leftLongerValue) + Math.Min(rightShorterValue, rightLongerValue)) / 2;
                }

                if (leftShorterValue > rightLongerValue)
                {
                    right = takeFromShorter - 1;
                }
                else
                {
                    left = takeFromShorter + 1;
                }
            }
        }
    }
}