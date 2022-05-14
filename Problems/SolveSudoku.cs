using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/sudoku-solver/
public class SolveSudoku
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(char[][] board, char[][] expected)
    {
        //act
        new Solution().SolveSudoku(board);

        //assert
        Assert.Equal(expected, board);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new char[][]{
                    new []{'5','3','.','.','7','.','.','.','.'},
                    new []{'6','.','.','1','9','5','.','.','.'},
                    new []{'.','9','8','.','.','.','.','6','.'},
                    new []{'8','.','.','.','6','.','.','.','3'},
                    new []{'4','.','.','8','.','3','.','.','1'},
                    new []{'7','.','.','.','2','.','.','.','6'},
                    new []{'.','6','.','.','.','.','2','8','.'},
                    new []{'.','.','.','4','1','9','.','.','5'},
                    new []{'.','.','.','.','8','.','.','7','9'}
                },
                new char[][]{
                    new []{'5','3','4','6','7','8','9','1','2'},
                    new []{'6','7','2','1','9','5','3','4','8'},
                    new []{'1','9','8','3','4','2','5','6','7'},
                    new []{'8','5','9','7','6','1','4','2','3'},
                    new []{'4','2','6','8','5','3','7','9','1'},
                    new []{'7','1','3','9','2','4','8','5','6'},
                    new []{'9','6','1','5','3','7','2','8','4'},
                    new []{'2','8','7','4','1','9','6','3','5'},
                    new []{'3','4','5','2','8','6','1','7','9'}}}
        };
    }

    public class Solution
    {
        private readonly List<HashSet<char>> _horisontals = Enumerable.Range(0, 9).Select(_ => new HashSet<char>()).ToList();
        private readonly List<HashSet<char>> _verticals = Enumerable.Range(0, 9).Select(_ => new HashSet<char>()).ToList();
        private readonly List<HashSet<char>> _boxes = Enumerable.Range(0, 9).Select(_ => new HashSet<char>()).ToList();
        private Stack<(int row, int col)> _toFill = new();
        private char[][] _board;
        public void SolveSudoku(char[][] board)
        {
            _board = board;
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[0].Length; j++)
                {
                    if (board[i][j] == '.')
                    {
                        _toFill.Push((i, j));
                    }
                    else
                    {
                        Push(i, j, board[i][j]);
                    }
                }
            }
            Solve();
        }

        public bool Solve()
        {
            if (_toFill.Count == 0)
            {
                return true;
            }

            var current = _toFill.Pop();
            for (var i = 0; i < 9; i++)
            {
                var number = (char)(49 + i);
                if (!IsValid(current.row, current.col, number))
                {
                    continue;
                }
                Push(current.row, current.col, number);
                if (Solve())
                {
                    return true;
                }
                Pull(current.row, current.col, number);
            }
            _toFill.Push(current);
            return false;
        }

        private void Push(int i, int j, char number)
        {
            _board[i][j] = number;
            _horisontals[i].Add(number);
            _verticals[j].Add(number);
            _boxes[GetBox(i, j)].Add(number);
        }

        private void Pull(int i, int j, char number)
        {
            _board[i][j] = '.';
            _horisontals[i].Remove(number);
            _verticals[j].Remove(number);
            _boxes[GetBox(i, j)].Remove(number);
        }

        private bool IsValid(int row, int col, char number)
        {
            return !_verticals[col].Contains(number)
                && !_horisontals[row].Contains(number)
                && !_boxes[GetBox(row, col)].Contains(number);
        }

        private int GetBox(int row, int col) => 3 * (row / 3) + col / 3;
    }
}