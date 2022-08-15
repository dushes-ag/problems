using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/populating-next-right-pointers-in-each-node/
public class PopulateRightPtrs
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int?[] nodes, int?[] expected)
    {
        //arrange
        var root = BuildTree(nodes);

        //act
        new Solution().Connect(root);
        var result = Iterate(root);

        //assert
        Assert.Equal(expected.ToList(), result);
    }

    private int?[] Iterate(Node? root)
    {
        var queue = new Queue<(Node? node, int level)>();
        var map = new SortedDictionary<int, Node>();
        queue.Enqueue((root, 0));
        while (queue.Count != 0)
        {
            var (node, level) = queue.Dequeue();
            if (node == null)
            {
                continue;
            }
            if (!map.ContainsKey(level))
            {
                map.Add(level, node);
            }
            queue.Enqueue((node.left, level + 1));
            queue.Enqueue((node.right, level + 1));
        }
        var result = new List<int?>();
        foreach (var kvp in map)
        {
            var node = kvp.Value;
            if (result.Count != 0)
            {
                result.Add(null);
            }
            while (node != null)
            {
                result.Add(node.val);
                node = node.next;
            }
        }
        return result.ToArray();
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new int? [] { 1,2,3,4,5,6,7 },
                new int? [] { 1,null,2,3,null,4,5,6,7 }
            }
        };
    }

    private static Node? BuildTree(int?[] items)
    {
        if (items.Length == 0)
        {
            return null;
        }
        var root = new Node(items[0]!.Value);
        var queue = new Queue<Node?>();
        queue.Enqueue(root);

        var i = 1;
        while (i < items.Length)
        {
            var parent = queue.Dequeue();
            if (parent != null && items[i] != null)
            {
                parent.left = new Node(items[i]!.Value);
            }
            i++;
            queue.Enqueue(parent?.left);

            if (parent != null && items[i] != null)
            {
                parent.right = new Node(items[i]!.Value);
            }
            i++;
            queue.Enqueue(parent?.right);
        }
        return root;
    }


    public class Node
    {
        public int val;
        public Node? left;
        public Node? right;
        public Node? next;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }

    public class Solution
    {
        public Node? Connect(Node? root)
        {
            if (root == null)
            {
                return root;
            }

            var ptr = root;
            while (ptr != null)
            {
                var nextLevelRoot = new Node();
                var nextLevelLastNode = nextLevelRoot;

                while (ptr != null)
                {
                    if (ptr.left != null)
                    {
                        nextLevelLastNode.next = ptr.left;
                        nextLevelLastNode = ptr.left;
                    }
                    if (ptr.right != null)
                    {
                        nextLevelLastNode.next = ptr.right;
                        nextLevelLastNode = ptr.right;
                    }
                    ptr = ptr.next;
                }
                ptr = nextLevelRoot.next;
            }
            return root;
        }
    }
}