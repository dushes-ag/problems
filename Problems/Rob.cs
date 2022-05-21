using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/house-robber-iii/
public class Rob
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(TreeNode root, int expected)
    {
        //act
        var result = new Solution().Rob(root);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            /*new object[]{
                BuildTree(new int? [] { 3,2,3,null,3,null,1 }),
                7
            },*/
            new object[]{
                BuildTree(new int? [] {79,99,77,null,null,null,69,null,60,53,null,73,11,null,null,null,62,27,62,null,null,98,50,null,null,
                90,48,82,null,null,null,55,64,null,null,73,56,6,47,null,93,null,null,75,44,30,82,null,null,null,null,null,null,57,36,89,42,
                null,null,76,10,null,null,null,null,null,32,4,18,null,null,1,7,null,null,42,64,null,null,39,76,null,null,6,null,66,8,96,91,38,38,
                null,null,null,null,74,42,null,null,null,10,40,5,null,null,null,null,28,8,24,47,null,null,null,17,36,50,19,63,33,89,
                null,null,null,null,null,null,null,null,94,72,null,null,79,25,null,null,51,null,70,84,43,null,64,35,null,null,null,null,
                40,78,null,null,35,42,98,96,null,null,82,26,null,null,null,null,48,91,null,null,35,93,86,42,null,null,null,null,0,61,null,
                null,67,null,53,48,null,null,82,30,null,97,null,null,null,1,null,null }),
                3038
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
            if (items[i] != null)
            {
                parent.left = new TreeNode(items[i].Value);
                queue.Enqueue(parent.left);
            }
            i++;

            if (items[i] != null)
            {
                parent.right = new TreeNode(items[i].Value);
                queue.Enqueue(parent.right);
            }
            i++;
        }
        return root;
    }

    public class Solution
    {
        private readonly Dictionary<(TreeNode node, bool isParentTaken), int> _cache = new();

        public int Rob(TreeNode root)
        {
            return DFS(root, false);
        }

        private int DFS(TreeNode node, bool isParentTaken)
        {
            if (node == null)
            {
                return 0;
            }

            if (!_cache.ContainsKey((node, isParentTaken)))
            {
                return _cache[(node, isParentTaken)] = isParentTaken
                    ? DFS(node.left, false) + DFS(node.right, false)
                    : Math.Max(node.val + DFS(node.left, true) + DFS(node.right, true), DFS(node.left, false) + DFS(node.right, false));
            }
            return _cache[(node, isParentTaken)];
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

}