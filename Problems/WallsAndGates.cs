using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/walls-and-gates
public class WallsAndGates
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int[][] rooms, int[][] expected)
    {
        //act
        new Solution().WallsAndGates(rooms);

        //assert
        Assert.Equal(expected, rooms);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int [][]
                {
                    new int[]{int.MaxValue,-1,0,int.MaxValue},
                    new int[]{int.MaxValue,int.MaxValue,int.MaxValue,-1},
                    new int[]{int.MaxValue,-1,int.MaxValue,-1},
                    new int[]{0,-1,int.MaxValue,int.MaxValue}
                },
                new int[] []
                {
                    new int[]{3,-1,0,1},
                    new int[]{2,2,1,-1},
                    new int[]{1,-1,2,-1},
                    new int[]{0,-1,3,4}
                }
            }
        };
    }

    public class Solution
    {
        const int GATE = 0;
        const int EMPTY = int.MaxValue;

        static List<(int h, int v)> _directions = new() { (0, 1), (0, -1), (1, 0), (-1, 0) };

        public void WallsAndGates(int[][] rooms)
        {
            var queue = new Queue<(int row, int col)>();
            for (var i = 0; i < rooms.Length; i++)
            {
                for (var j = 0; j < rooms[i].Length; j++)
                {
                    if (rooms[i][j] == GATE)
                    {
                        queue.Enqueue((i, j));
                    }
                }
            }

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                for (var k = 0; k < _directions.Count; k++)
                {
                    if (0 <= item.row + _directions[k].v && item.row + _directions[k].v < rooms.Length
                        && 0 <= item.col + _directions[k].h && item.col + _directions[k].h < rooms[item.row].Length
                        && rooms[item.row + _directions[k].v][item.col + _directions[k].h] == EMPTY)
                    {
                        rooms[item.row + _directions[k].v][item.col + _directions[k].h] = rooms[item.row][item.col] + 1;
                        queue.Enqueue((item.row + _directions[k].v, item.col + _directions[k].h));
                    }
                }
            }
        }
    }
}