using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/shortest-path-to-get-all-keys/
public class ShortestPathAllKeys
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string[] grid, int expected)
    {
        //act
        var result = new Solution().ShortestPathAllKeys(grid);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new string[] {
                    "@.a..","###.#","b.A.B"
                },
                8
            },
            new object[]{
                new string[] {
                    "@Aa"
                },
                -1
            }
        };
    }

    public class Solution
    {
        private static readonly Coordinate[] _steps = new Coordinate[] { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) };

        public int ShortestPathAllKeys(string[] grid)
        {
            var (maxKeyBits, start) = Init(grid);
            var queue = new Queue<(Coordinate coord, int currentKeyBits, int steps)>();
            queue.Enqueue((start, 0, 0));

            var visited = new HashSet<(Coordinate coordinate, int steps)>();
            while (queue.Count > 0)
            {
                var (coord, currentKeyBits, steps) = queue.Dequeue();
                if (visited.Contains((coord, currentKeyBits)))
                {
                    continue;
                }

                visited.Add((coord, currentKeyBits));

                var val = grid[coord.Row][coord.Col];
                if (IsKey(val))
                {
                    currentKeyBits = AddKey(currentKeyBits, val);
                    if (currentKeyBits == maxKeyBits)
                    {
                        return steps;
                    }
                }

                foreach (var delta in _steps)
                {
                    var nextStep = coord + delta;
                    if (nextStep.IsValid(grid, currentKeyBits))
                    {
                        queue.Enqueue((nextStep, currentKeyBits, steps + 1));
                    }
                }

            }

            return -1;
        }

        private static (int maxKeyBits, Coordinate start) Init(string[] grid)
        {
            var maxKeyBits = 0;
            Coordinate start = new(0, 0);
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[0].Length; j++)
                {
                    var ch = grid[i][j];
                    if (IsKey(ch))
                    {
                        maxKeyBits = AddKey(maxKeyBits, ch);
                    }
                    else if (IsStart(ch))
                    {
                        start = new(i, j);
                    }
                }
            }

            return (maxKeyBits, start);
        }

        public static int AddKey(int keyBits, char _) => keyBits | GetKeyBit(_);
        public static bool HasKey(int keyBits, char _) => AddKey(keyBits, _) == keyBits;
        private static int GetKeyBit(char _) => (1 << ((int)char.ToLowerInvariant(_) - (int)'a'));

        private static bool IsKey(char val) => char.IsLower(val);
        private static bool IsWall(char val) => val == '#';
        private static bool IsStart(char val) => val == '@';
        private static bool IsEmpty(char val) => val == '.';

        record Coordinate(int Row, int Col)
        {
            public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.Row + b.Row, a.Col + b.Col);
            public bool IsValid(string[] grid, int keyBits)
            {
                if (!(Row >= 0 && Row < grid.Length && Col >= 0 & Col < grid[0].Length))
                {
                    return false;
                }
                var val = grid[Row][Col];
                return !IsWall(val) && (IsStart(val) || IsEmpty(val) || IsKey(val) || HasKey(keyBits, val));
            }
        }
    }
}