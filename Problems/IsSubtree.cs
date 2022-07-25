using System.Collections.Generic;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/subtree-of-another-tree/
public class IsSubtree
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(TreeNode? root, TreeNode? subRoot, bool expected)
    {
        //act
        var result = new Solution().IsSubtree(root, subRoot);

        //assert
        Assert.Equal(expected, result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                BuildTree(new int? [] { 1,null,1,null,1,null,1,null,1,null,1,null,1,null,1,null,1,null,1,null,1,2 }),
                BuildTree(new int? [] { 1,null,1,null,1,null,1,null,1,null,1,2 }),
                true
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
        public bool IsSubtree(TreeNode root, TreeNode subRoot)
        {
            return IsEqual(root, subRoot)
                || root.left != null && IsSubtree(root.left, subRoot)
                || root.right != null && IsSubtree(root.right, subRoot);
        }
        private bool IsEqual(TreeNode? node1, TreeNode? node2)
            => node1 == null && node2 == null
            || node1?.val == node2?.val && IsEqual(node1.left, node2.left) && IsEqual(node1.right, node2.right);
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