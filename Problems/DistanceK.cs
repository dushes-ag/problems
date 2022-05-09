using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/find-all-the-lonely-nodes/
public class DistanceK
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(int?[] nodes, int target, int k, int[] expected)
    {
        //arrange
        var root = BuildTree(nodes);
        var targetNode = FindNode(root, target);

        //act
        var result = new Solution().DistanceK(root, targetNode, k);

        //assert
        Assert.Equal(expected.ToList(), result);
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
                new int? [] { 3,5,1,6,2,0,8,null,null,7,4 },
                5,
                2,
                 new int[]{7,4,1}
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
        public IList<int> DistanceK(TreeNode root, TreeNode target, int k)
        {
            var result = new List<int>();

            var parentsMap = new Dictionary<TreeNode, TreeNode>();
            BuildParentsMap(root, parentsMap);



            DFS(target, result, k);

            var parent = target;
            while (k > 0)
            {
                var nextParent = parentsMap.GetValueOrDefault(parent);
                if (nextParent == null)
                {
                    break;
                }
                k--;
                DFS(nextParent, result, k, parent);
                parent = nextParent;
            }

            return result;
        }

        private void BuildParentsMap(TreeNode node, Dictionary<TreeNode, TreeNode> parentsMap)
        {
            if (node.left != null)
            {
                parentsMap[node.left] = node;
                BuildParentsMap(node.left, parentsMap);
            }
            if (node.right != null)
            {
                parentsMap[node.right] = node;
                BuildParentsMap(node.right, parentsMap);
            }
        }

        private void DFS(TreeNode node, List<int> result, int k, TreeNode skipNode = null)
        {
            if (k == 0)
            {
                result.Add(node.val);
                return;
            }

            if (node.left != null && node.left != skipNode)
            {
                DFS(node.left, result, k - 1, skipNode);
            }

            if (node.right != null && node.right != skipNode)
            {
                DFS(node.right, result, k - 1, skipNode);
            }
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
}