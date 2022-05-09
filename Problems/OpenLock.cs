using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/open-the-lock/
public class OpenLock
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string[] deadends, string target, int expected)
    {
        //act
        var result = new Solution().OpenLock(deadends, target);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new string[] {"0201","0101","0102","1212","2002"},
                "0202",
                6
            },
            new object[]{
                new string[] {"8888"},
                "0009",
                1
            }
        };
    }
    public class Solution
    {
        public int OpenLock(string[] deadends, string target)
        {
            const int NOT_FOUND = -1;
            if (deadends.Contains(target))
            {
                return NOT_FOUND;
            }

            var checkedItems = new HashSet<string>();
            var queue = new Queue<(Key key, int step)>();
            queue.Enqueue((new Key(), 0));
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (state.key == target)
                {
                    return state.step;
                }
                if (deadends.Contains(state.key))
                {
                    continue;
                }
                if (checkedItems.Contains(state.key))
                {
                    continue;
                }

                checkedItems.Add(state.key);
                foreach (var move in _moves)
                {
                    queue.Enqueue((state.key + move, state.step + 1));
                }
            }

            return NOT_FOUND;
        }

        private static Key[] _moves = new Key[]
        {
            new(1,0,0,0),
            new(-1,0,0,0),
            new(0,1,0,0),
            new(0,-1,0,0),
            new(0,0,1,0),
            new(0,0,-1,0),
            new(0,0,0,1),
            new(0,0,0,-1)
        };

        record Key(int _1 = 0, int _2 = 0, int _3 = 0, int _4 = 0)
        {
            public static Key operator +(Key a, Key b) => new(
                CalculateKeyPart(a._1, b._1),
                CalculateKeyPart(a._2, b._2),
                CalculateKeyPart(a._3, b._3),
                CalculateKeyPart(a._4, b._4));
            private static int CalculateKeyPart(int a, int b) => (a + b + 10) % 10;
            public static implicit operator string(Key _) => $"{_._1}{_._2}{_._3}{_._4}";
        }
    }
}