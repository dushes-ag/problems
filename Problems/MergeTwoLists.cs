using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/merge-two-sorted-lists/
public class MergeTwoLists
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(ListNode list1, ListNode list2, ListNode expected)
    {
        //act
        var result = new Solution().MergeTwoLists(list1, list2);

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
                new ListNode(1, new ListNode(2, new ListNode(4))),
                new ListNode(1, new ListNode(3, new ListNode(4))),
                new ListNode(1, new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(4))))))}
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
        public ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
        {
            var result = new ListNode();
            ListNode lastNode = result;

            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val)
                {
                    lastNode.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    lastNode.next = list2;
                    list2 = list1;
                    list1 = lastNode.next.next;
                }
                lastNode = lastNode.next;
            }

            if (list1 != null)
            {
                lastNode.next = list1;
            }
            if (list2 != null)
            {
                lastNode.next = list2;
            }

            return result.next;
        }
    }
}