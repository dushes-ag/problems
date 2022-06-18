using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/same-tree/
public class IsSameTree
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(TreeNode? p, TreeNode? q, bool expected)
    {
        //act
        var result = new Solution().IsSameTree(p, q);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                BuildTree(new int? [] { 1,2,3 }),
                BuildTree(new int? [] { 1,2,3 }),
                true
            },
            new object[]{
                BuildTree(new int? [] { 1,2,3 }),
                BuildTree(new int? [] { 1,2,4 }),
                false
            },
            new object[]{
                BuildTree(new int? [] { 1,2,3 }),
                BuildTree(new int? [] { 1,2,null }),
                false
            }
        };
    }

    private static TreeNode BuildTree(int?[] items)
    {
        var root = new TreeNode(items[0].Value);
        var queue = new Queue<TreeNode?>();
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
        public bool IsSameTree(TreeNode? p, TreeNode? q)
        {
            if (p == null && q == null)
            {
                return true;
            }
            if (p == null && q != null || p != null && q == null)
            {
                return false;
            }
            return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

}