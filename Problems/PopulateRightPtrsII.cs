using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii/
public class PopulateRightPtrsII
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
                new int? [] { 1,2,3,4,5,null,7 },
                new int? [] { 1,null,2,3,null,4,5,7 }
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
            return BFS(root);
            //return SpaceO1(root);
        }

        private Node? SpaceO1(Node? root)
        {
            if (root == null)
            {
                return root;
            }

            var ptr = root;
            while (ptr != null)
            {
                var nextLevelHead = new Node();
                var nextLevelPtr = nextLevelHead;
                var currentLevelPtr = ptr;
                while (currentLevelPtr != null)
                {
                    if (currentLevelPtr.left != null)
                    {
                        nextLevelPtr.next = currentLevelPtr.left;
                        nextLevelPtr = nextLevelPtr.next;
                    }
                    if (currentLevelPtr.right != null)
                    {
                        nextLevelPtr.next = currentLevelPtr.right;
                        nextLevelPtr = nextLevelPtr.next;
                    }
                    currentLevelPtr = currentLevelPtr.next;
                }
                ptr = nextLevelHead.next;
            }

            return root;
        }

        private Node? BFS(Node? root)
        {
            var queue = new Queue<(Node? node, int level)>();
            queue.Enqueue((root, 0));
            var lastLevel = -1;
            Node? lastNode = null;
            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();
                if (node == null)
                {
                    continue;
                }
                if (level == lastLevel)
                {
                    node.next = lastNode;
                }
                lastNode = node;
                lastLevel = level;

                queue.Enqueue((node.right, level + 1));
                queue.Enqueue((node.left, level + 1));
            }
            return root;
        }
    }
}