using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/robot-room-cleaner/
public class CleanRoom
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(bool[][] map, int row, int col)
    {
        //arrange
        var robot = new RobotImpl(map, row, col);
        //act
        new Solution().CleanRoom(robot);

        //assert
        var expected = map
            .SelectMany((row, rowIndex) => row.Select((isEmptySlot, colIndex) => (isEmptySlot, colIndex)).Where(_ => _.isEmptySlot).Select(_ => (rowIndex, _.colIndex)))
            .ToList();
        Assert.Equal(expected, robot.GetVisited().OrderBy(_ => _.rowIndex).ThenBy(_ => _.colIndex));
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new bool[][] {
                    new bool[]{true,true,true,true,true,false,true,true},
                    new bool[]{true,true,true,true,true,false,true,true},
                    new bool[]{true,false,true,true,true,true,true,true},
                    new bool[]{false,false,false,true,false,false,false,false},
                    new bool[]{true,true,true,true,true,true,true,true}
                },
                1,
                3
            }
        };
    }

    class Solution
    {
        public static readonly Dictionary<int, (int row, int col)> _moves = new() { { 0, (-1, 0) }, { 1, (0, 1) }, { 2, (1, 0) }, { 3, (0, -1) } };
        private HashSet<(int rowShift, int colShift)> _visited = new();
        private Robot _robot;

        public void CleanRoom(Robot robot)
        {
            _robot = robot;
            Backtrack(0, 0, 0);
        }

        private void Backtrack(int rowShift, int colShift, int direction)
        {
            _visited.Add((rowShift, colShift));
            _robot.Clean();

            for (var i = 0; i < 4; i++)
            {
                var newDirection = (direction + i) % 4;
                var nextRow = rowShift + _moves[newDirection].row;
                var nextCol = colShift + _moves[newDirection].col;
                if (!_visited.Contains((nextRow, nextCol)) && _robot.Move())
                {
                    Backtrack(nextRow, nextCol, newDirection);

                    _robot.TurnRight();
                    _robot.TurnRight();
                    _robot.Move();
                    _robot.TurnRight();
                    _robot.TurnRight();

                }
                _robot.TurnRight();
            }
        }
    }

    class RobotImpl : Robot
    {
        private HashSet<(int row, int col)> _visited = new();
        private readonly bool[][] map;
        private int row;
        private int col;
        private int direction;

        public RobotImpl(bool[][] map, int row, int col)
        {
            this.map = map;
            this.row = row;
            this.col = col;
        }

        public void Clean()
        {
            _visited.Add((row, col));
        }

        public bool Move()
        {
            var nextRow = row + Solution._moves[direction].row;
            var nextCol = col + Solution._moves[direction].col;
            if (nextRow < 0 || nextRow >= map.Length || nextCol < 0 || nextCol >= map[0].Length || !map[nextRow][nextCol])
            {
                return false;
            }

            row = nextRow;
            col = nextCol;
            return true;
        }

        public void TurnLeft()
        {
            direction = (4 + direction - 1) % 4;
        }

        public void TurnRight()
        {

            direction = (4 + direction + 1) % 4;
        }

        internal IEnumerable<(int rowIndex, int colIndex)> GetVisited() => _visited;
    }

    // This is the robot's control interface.
    // You should not implement it, or speculate about its implementation
    interface Robot
    {
        // Returns true if the cell in front is open and robot moves into the cell.
        // Returns false if the cell in front is blocked and robot stays in the current cell.
        public bool Move();

        // Robot will stay in the same cell after calling turnLeft/turnRight.
        // Each turn will be 90 degrees.
        public void TurnLeft();
        public void TurnRight();

        // Clean the current cell.
        public void Clean();
    }
}