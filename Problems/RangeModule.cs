using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/range-module/
public class RangeModuleTest
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(string[] actions, int[][] args, bool?[] expected)
    {
        //act
        var obj = new RangeModule();
        var result = new List<bool?>();
        for (var i = 0; i < actions.Length; i++)
        {
            if (actions[i] == "addRange")
            {
                obj.AddRange(args[i][0], args[i][1]);
                result.Add(null);
            }
            else if (actions[i] == "removeRange")
            {
                obj.RemoveRange(args[i][0], args[i][1]);
                result.Add(null);
            }
            else if (actions[i] == "queryRange")
            {
                result.Add(obj.QueryRange(args[i][0], args[i][1]));
            }
        }

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object []{
                new string[]{"addRange", "removeRange", "queryRange", "queryRange", "queryRange"},
                new int[][]{new int[]{10,20},new int[]{14,16},new int[]{10,14},new int[]{13,15},new int[]{16,17}},
                new bool?[]{null, null, true, false, true}}
        };
    }

    public class RangeModule
    {
        private SegmentTreeNode _root = new SegmentTreeNode { Left = 0, Right = int.MaxValue };

        public RangeModule()
        {
        }

        public void AddRange(int left, int right)
        {
            Update(left, right, _root, true);
        }

        public bool QueryRange(int left, int right)
        {
            return Search(left, right, _root);
        }

        public void RemoveRange(int left, int right)
        {
            Update(left, right, _root, false);
        }

        private void Update(int left, int right, SegmentTreeNode node, bool add)
        {
            if (node.Left == left && node.Right == right)
            {
                node.IsFull = add;
                node.LeftNode = null;
                node.RightNode = null;
                return;
            }
            if (node.LeftNode == null || node.RightNode == null)
            {
                var middle = node.Left + (node.Right - node.Left) / 2;
                node.LeftNode = new SegmentTreeNode { Left = node.Left, Right = middle, IsFull = node.IsFull };
                node.RightNode = new SegmentTreeNode { Left = middle, Right = node.Right, IsFull = node.IsFull };
            }

            if (right <= node.LeftNode.Right)
            {
                Update(left, right, node.LeftNode, add);
            }
            else if (left >= node.RightNode.Left)
            {
                Update(left, right, node.RightNode, add);
            }
            else
            {
                Update(left, node.LeftNode.Right, node.LeftNode, add);
                Update(node.RightNode.Left, right, node.RightNode, add);
            }
            node.IsFull = node.LeftNode.IsFull && node.RightNode.IsFull;
        }

        private bool Search(int left, int right, SegmentTreeNode node)
        {
            if (node.LeftNode == null || node.RightNode == null || node.Left == left && node.Right == right)
            {
                return node.IsFull;
            }

            if (right <= node.LeftNode.Right)
            {
                return Search(left, right, node.LeftNode);
            }
            if (left >= node.RightNode.Left)
            {
                return Search(left, right, node.RightNode);
            }
            return Search(left, node.LeftNode.Right, node.LeftNode) && Search(node.RightNode.Left, right, node.RightNode);
        }

        class SegmentTreeNode
        {
            public int Left { get; init; }
            public int Right { get; init; }
            public bool IsFull { get; set; }
            public SegmentTreeNode? LeftNode { get; set; }
            public SegmentTreeNode? RightNode { get; set; }
        }
    }
}