using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/merge-k-sorted-lists/
public class MergeKLists
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(ListNode?[] lists, ListNode expected)
    {
        //act
        var result = new Solution().MergeKLists(lists);

        //assert

        GetValues(result).Should().BeEquivalentTo(GetValues(expected));
        List<int> GetValues(ListNode? node)
        {
            var result = new List<int>();
            while (node != null)
            {
                result.Add(node.val);
                node = node.next;
            }
            return result;
        }
    }

    public static object[] GetCases()
    {
        return new object[]{
            new object[]{
                new ListNode?[]{
                    new ListNode(1, new ListNode(4, new ListNode(5))),
                    new ListNode(1, new ListNode(3, new ListNode(4))),
                    new ListNode(2, new ListNode(6))
                },
                new ListNode(1, new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(4, new ListNode(5, new ListNode(6))))))))},
                new object[]{
                new ListNode?[]{
                    new ListNode(-2, new ListNode(-1, new ListNode(-1))),
                    null
                },
                new ListNode(-2, new ListNode(-1, new ListNode(-1)))}
        };
    }

    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class Solution
    {
        public ListNode? MergeKLists(ListNode?[] lists)
        {
            var queue = new PriorityQueue<ListNode, int>();
            lists = lists.ToArray();
            var hasAnyNode = true;
            while (hasAnyNode)
            {
                hasAnyNode = false;
                for (var i = 0; i < lists.Length; i++)
                {
                    var node = lists[i];
                    if (node != null)
                    {
                        hasAnyNode = true;
                        queue.Enqueue(new ListNode(node.val), node.val);
                        lists[i] = node.next;
                    }
                }
            }
            var listNode = new ListNode();
            var pointer = listNode;
            while (queue.Count > 0)
            {
                pointer.next = queue.Dequeue();
                pointer = pointer.next;
            }
            return listNode.next;
        }
    }
}