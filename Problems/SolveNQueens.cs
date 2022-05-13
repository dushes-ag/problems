using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/n-queens/
public class SolveNQueens
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int n, string[][] expected)
    {
        //act
        var result = new Solution().SolveNQueens(n);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                4,
                new string[][]{
                    new[]{".Q..","...Q","Q...","..Q."},
                    new[]{"..Q.","Q...","...Q",".Q.."}}},

            new object []{
                1,
                new string[][]{new[]{"Q"}}}
        };
    }

    public class Solution
    {
        private readonly IList<IList<string>> _solutions = new List<IList<string>>();
        private readonly HashSet<int> _verticals = new();
        private readonly HashSet<int> _diagonals = new();
        private readonly HashSet<int> _antiDiagonals = new();
        private int _size;
        private bool[][] _board;
        public IList<IList<string>> SolveNQueens(int n)
        {
            _size = n;
            _board = new bool[n][];
            for (var i = 0; i < n; i++)
            {
                _board[i] = new bool[n];
            }
            Solve(0);
            return _solutions;
        }

        public void Solve(int i)
        {
            if (i == _size)
            {
                AddSolution();
                return;
            }

            for (var j = 0; j < _size; j++)
            {
                if (!IsValid(i, j))
                {
                    continue;
                }
                Push(i, j);
                Solve(i + 1);
                Pull(i, j);
            }
        }

        private void Push(int i, int j)
        {
            _board[i][j] = true;
            _verticals.Add(j);
            _diagonals.Add(GetDiagonale(i, j));
            _antiDiagonals.Add(GetAntidiagonale(i, j));
        }

        private void Pull(int i, int j)
        {
            _board[i][j] = false;
            _verticals.Remove(j);
            _diagonals.Remove(GetDiagonale(i, j));
            _antiDiagonals.Remove(GetAntidiagonale(i, j));
        }

        private void AddSolution()
        {
            _solutions.Add(_board.Select(_ => string.Concat(_.Select(__ => __ ? "Q" : "."))).ToList());
        }

        private bool IsValid(int i, int j)
        {
            return !_verticals.Contains(j)
                && !_diagonals.Contains(GetDiagonale(i, j))
                && !_antiDiagonals.Contains(GetAntidiagonale(i, j));
        }

        private int GetDiagonale(int i, int j) => i - j;

        private int GetAntidiagonale(int i, int j) => i + j;
    }
}