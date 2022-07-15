using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/
public class DeleteDuplicates
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(ListNode list, ListNode expected)
    {
        //act
        var result = new Solution().DeleteDuplicates(list);

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
                new ListNode(1, new ListNode(1, new ListNode(1, new ListNode(2, new ListNode(3))))),
                new ListNode(2, new ListNode(3))},
                new object[]{
                new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(3, new ListNode(5))))),
                new ListNode(1, new ListNode(2, new ListNode(5)))}
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
        public ListNode? DeleteDuplicates(ListNode? head)
        {
            var p1 = head;
            head = new ListNode(int.MinValue, head);
            var last = head;
            while (p1 != null)
            {
                var p2 = p1;
                while (p2.val == p2.next?.val)
                {
                    p2 = p2.next;
                }
                if (p1 == p2)
                {
                    last.next = p2;
                    last = p2;
                }
                else
                {
                    last.next = p2.next;
                }
                p1 = p2.next;
            }
            return head.next;
        }
    }
}