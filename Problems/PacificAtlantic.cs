using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/pacific-atlantic-water-flow
public class PacificAtlantic
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] heights, List<List<int>> expected)
    {
        //act
        var result = new Solution().PacificAtlantic(heights);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int [][]
                {
                    new int[]{1,2,2,3,5},
                    new int[]{3,2,3,4,4},
                    new int[]{2,4,5,3,1},
                    new int[]{6,7,1,4,5},
                    new int[]{5,1,1,2,4}
                },
                new List<List<int>>
                {
                    new List<int>{0,4},
                    new List<int>{1,3},
                    new List<int>{1,4},
                    new List<int>{2,2},
                    new List<int>{3,0},
                    new List<int>{3,1},
                    new List<int>{4,0}
                }
            },
            new object[]{
                new int [][]
                {
                    new int[]{3,3,3,3,3,3},
                    new int[]{3,0,3,3,0,3},
                    new int[]{3,3,3,3,3,3}
                },
                new List<List<int>>
                {
                    new List<int>{0,0},
                    new List<int>{0,1},
                    new List<int>{0,2},
                    new List<int>{0,3},
                    new List<int>{0,4},
                    new List<int>{0,5},
                    new List<int>{1,0},
                    new List<int>{1,2},
                    new List<int>{1,3},
                    new List<int>{1,5},
                    new List<int>{2,0},
                    new List<int>{2,1},
                    new List<int>{2,2},
                    new List<int>{2,3},
                    new List<int>{2,4},
                    new List<int>{2,5}
                }
            }
        };
    }

    public class Solution
    {
        static List<(int v, int h)> _directions = new() { (0, 1), (1, 0), (-1, 0), (0, -1) };
        public IList<IList<int>> PacificAtlantic(int[][] heights)
        {
            var result = new List<IList<int>>();
            if (heights.Length == 0)
            {
                return result;
            }

            var pacificQueue = new Queue<(int row, int col)>();
            var athlancicQueue = new Queue<(int row, int col)>();
            for (var i = 0; i < heights.Length; i++)
            {
                pacificQueue.Enqueue((i, 0));
                athlancicQueue.Enqueue((i, heights[0].Length - 1));
            }
            for (var i = 1; i < heights[0].Length; i++)
            {
                pacificQueue.Enqueue((0, i));
                athlancicQueue.Enqueue((heights.Length - 1, i - 1));
            }

            var pacific = BFS(pacificQueue, heights);
            var athlantic = BFS(athlancicQueue, heights);

            for (var i = 0; i < heights.Length; i++)
            {
                for (var j = 0; j < heights[0].Length; j++)
                {
                    if (pacific[i][j] && athlantic[i][j])
                    {
                        result.Add(new List<int> { i, j });
                    }
                }
            }
            return result;
        }

        private bool[][] BFS(Queue<(int row, int col)> queue, int[][] heights)
        {
            var reach = new bool[heights.Length][];
            for (var i = 0; i < heights.Length; i++)
            {
                reach[i] = new bool[heights[0].Length];
            }

            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                reach[cell.row][cell.col] = true;
                for (var k = 0; k < _directions.Count; k++)
                {
                    var rowIndex = cell.row + _directions[k].v;
                    var colIndex = cell.col + _directions[k].h;

                    if (0 <= rowIndex && rowIndex < heights.Length
                        && 0 <= colIndex && colIndex < heights[0].Length
                        && heights[cell.row][cell.col] <= heights[rowIndex][colIndex]
                        && !reach[rowIndex][colIndex])
                    {
                        queue.Enqueue((rowIndex, colIndex));
                    }
                }
            }
            return reach;
        }
    }
}