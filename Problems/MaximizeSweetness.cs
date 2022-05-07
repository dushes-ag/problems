using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/divide-chocolate/
public class MaximizeSweetness
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void TestMaximizeSweetness(int[] sweetness, int k, int expected)
    {
        //act
        var result = new SolutionMaximizeSweetness().MaximizeSweetness(sweetness, k);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new int[]{19679,20653,68010,3714,54485,548,41366,11201,47138,70768,1050,87246,17114,56157,13235,65363,30444,56929,21969,22308},
                0,
                709377}
        };
    }
}

public class SolutionMaximizeSweetness
{
    public int MaximizeSweetness(int[] sweetness, int k)
    {
        var start = sweetness.Min();
        var end = sweetness.Sum() - k + 1;
        var result = start;
        while (start <= end)
        {
            var maybeSolution = (start + end + 1) / 2;
            if (IsWorkable(sweetness, k, maybeSolution))
            {
                result = maybeSolution;
                start = result + 1;
            }
            else
            {
                end = maybeSolution - 1;
            }
        }
        return result;
    }

    private bool IsWorkable(int[] sweetness, int k, int tryValue)
    {
        var chunksCount = 0;
        var chunkSweetness = 0;
        for (var i = 0; i < sweetness.Length; i++)
        {
            chunkSweetness += sweetness[i];
            if (chunkSweetness >= tryValue)
            {
                chunksCount++;
                chunkSweetness = 0;
            }
            if (chunksCount == k + 1)
            {
                return true;
            }
        }
        return false;
    }
}