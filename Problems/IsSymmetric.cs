using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/symmetric-tree/
public class IsSymmetric
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int?[] nodes, bool expected)
    {
        //arrange
        var root = BuildTree(nodes);

        //act
        var result = new Solution().IsSymmetric(root);

        //assert
        Assert.Equal(expected, result);
    }

    private TreeNode FindNode(TreeNode root, int target)
    {
        if (root == null || root.val == target)
        {
            return root;
        }
        return FindNode(root.left, target) ?? FindNode(root.right, target);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int? [] { 1,2,2,3,4,4,3 },
                true
            },
            new object[]{
                new int? [] { 1,2,2,null, 3, null, 3 },
                false
            }
        };
    }

    private static TreeNode BuildTree(int?[] items)
    {
        var root = new TreeNode(items[0].Value);
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        var i = 1;
        while (i < items.Length)
        {
            var parent = queue.Dequeue();
            if (parent != null && items[i] != null)
            {
                parent.left = new TreeNode(items[i].Value);
            }
            i++;
            queue.Enqueue(parent?.left);

            if (parent != null && items[i] != null)
            {
                parent.right = new TreeNode(items[i].Value);
            }
            i++;
            queue.Enqueue(parent?.right);
        }
        return root;
    }

    public class Solution
    {
        public bool IsSymmetric(TreeNode root)
        {
            var queue = new Queue<(TreeNode node, int level)>();
            queue.Enqueue((root, 0));
            var levelNodes = new List<int?>();
            var lastLevel = 0;
            while (true)
            {
                var item = queue.Dequeue();
                if (levelNodes.Count > 0 && lastLevel != item.level)
                {
                    if (!IsSymmetric(levelNodes))
                    {
                        return false;
                    }
                    if (levelNodes.All(_ => _ == null))
                    {
                        return true;
                    }
                    levelNodes = new();
                    lastLevel = item.level;
                }
                levelNodes.Add(item.node?.val);
                queue.Enqueue((item.node?.left, item.level + 1));
                queue.Enqueue((item.node?.right, item.level + 1));
            }
        }
        private bool IsSymmetric(List<int?> list)
        {
            for (var i = 0; i < list.Count / 2; i++)
            {
                if (list[i] != list[list.Count - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int x) { val = x; }
    }
}