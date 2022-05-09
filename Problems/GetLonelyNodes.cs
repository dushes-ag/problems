using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/find-all-the-lonely-nodes/
public class GetLonelyNodes
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(TreeNode root, int[] expected)
    {
        //act
        var result = new Solution().GetLonelyNodes(root);

        //assert
        Assert.Equal(expected.ToList(), result);
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                BuildTree(new int? [] { 1,2,3,null,4 }),
                new int[] { 4 }
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
        public IList<int> GetLonelyNodes(TreeNode root)
        {
            var result = new List<int>();
            DFS(root, result);

            return result;
        }
        private void DFS(TreeNode node, List<int> lonelyNodes)
        {
            if (node.left == null && node.right != null)
            {
                lonelyNodes.Add(node.right.val);
            }
            if (node.left != null && node.right == null)
            {
                lonelyNodes.Add(node.left.val);
            }

            if (node.left != null)
            {
                DFS(node.left, lonelyNodes);
            }

            if (node.right != null)
            {
                DFS(node.right, lonelyNodes);
            }
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