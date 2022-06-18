using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Problems;

///https://leetcode.com/problems/rotate-list/
public class RotateRight
{
    [Theory]
    [MemberData(nameof(GetCases))]
    public void Test(ListNode list, int k, ListNode expected)
    {
        //act
        var result = new Solution().RotateRight(list, k);

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
                new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5))))),
                2,
                new ListNode(4, new ListNode(5, new ListNode(1, new ListNode(2, new ListNode(3)))))}
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
        public ListNode? RotateRight(ListNode? head, int k)
        {
            if (head?.next == null)
            {
                return head;
            }
            var length = 1;
            var current = head;
            var last = head;
            while (current.next != null)
            {
                current = current.next;
                length++;
            }

            k = k % length;
            if (k == 0)
            {
                return head;
            }
            k = length - k;

            current.next = head;
            while (k-- > 0)
            {
                current = current.next;
            }
            var newHead = current.next;
            current.next = null;
            return newHead;
        }
    }
}