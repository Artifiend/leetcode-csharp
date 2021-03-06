
using System;
using System.Linq;

public class ListNode
{
    public int val;
    public ListNode next;
    
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}

public partial class Solution
{
    /*
     * 2. Add Two Numbers
     * https://leetcode.com/problems/add-two-numbers/
     */
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2, int carry = 0)
    {
        if(l1 is null && l2 is null && carry == 0) {
            return null;
        }
        
        int sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
        
        var (quot, rem) = Math.DivRem(sum, 10);
        
        return new ListNode(rem, AddTwoNumbers(l1?.next, l2?.next, quot));
    }
    
    /*
     * 19. Remove Nth Node From End of List
     * https://leetcode.com/problems/remove-nth-node-from-end-of-list/
     */
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        var dummy = new ListNode(0, head);
        Remove(dummy, dummy, n);
        return dummy.next;
        
        void Remove(ListNode fast, ListNode slow, int diff)
        {
            if(fast is null) {
                slow.next = slow.next.next;
                return;
            }
            
            if(diff < 0) {
                slow = slow.next;
            }
            
            Remove(fast.next, slow, diff - 1);
        }
    }
    
    /*
     * 21. Merge Two Sorted Lists
     * https://leetcode.com/problems/merge-two-sorted-lists/
     */
    public ListNode MergeTwoLists(ListNode left, ListNode right)
    {
        if(left is null || right is null) {
            return left ?? right;
        }
        
        var (min, max) =
            left.val <= right.val
            ? (left, right)
            : (right, left);
        
        min.next = MergeTwoLists(min.next, max);
        return min;
    }
    
    /*
     * 23. Merge k Sorted Lists
     * https://leetcode.com/problems/merge-k-sorted-lists/
     */
    public ListNode MergeKLists(ListNode[] lists)
    {
        var min = lists.MinBy(l => l?.val);
        
        if(min != null) {
            var minIdx = Array.IndexOf(lists, min);
            lists[minIdx] = lists[minIdx].next;
            min.next = MergeKLists(lists);
        }
        
        return min;
    }
    
    /*
     * 24. Swap Nodes in Pairs
     * https://leetcode.com/problems/swap-nodes-in-pairs/
     */
    public ListNode SwapPairs(ListNode head)
    {
        if(head?.next is null) {
            return head;
        }
        
        var newHead = head.next;
        head.next = SwapPairs(newHead.next);
        newHead.next = head;
        return newHead;
    }
    
    /*
     * 25. Reverse Nodes in k-Group
     * https://leetcode.com/problems/reverse-nodes-in-k-group/
     */
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        var lastOfGroup = AdvanceKSteps(head, k - 1);
        
        if(lastOfGroup != null) {
            ReverseK(head, ReverseKGroup(lastOfGroup.next, k), k);
        }
        
        return lastOfGroup ?? head;
        
        ListNode AdvanceKSteps(ListNode head, int k) =>
            head is null || k == 0 ? head : AdvanceKSteps(head.next, k - 1);
        
        void ReverseK(ListNode head, ListNode prev, int k)
        {
            if(k == 0) {
                return;
            }

            ReverseK(head.next, head, k - 1);
            head.next = prev;
        }
    }
    
    /*
     * 61. Rotate List
     * https://leetcode.com/problems/rotate-list/
     */
    public ListNode RotateRight(ListNode head, int k)
    {
        int len = Length(head);
        if(len == 0 || k == 0 || k % len == 0) {
            return head;
        }
        
        var newHead = CutAt(head, len - (k % len));
        Append(newHead, head);
        return newHead;
        
        ListNode Append(ListNode head, ListNode tail) =>
            head.next is null ? head.next = tail : Append(head.next, tail);
        
        ListNode CutAt(ListNode head, int k)
        {
            if(k <= 1) {
                var res = head.next;
                head.next = null;
                return res;
            }
            
            return CutAt(head.next, k - 1);
        }
        
        int Length(ListNode head) =>
            head is null ? 0 : 1 + Length(head.next);
    }
    
    /*
     * 82. Remove Duplicates from Sorted List II
     * https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/
     */
    public ListNode DeleteDuplicates(ListNode head)
    {
        if(head is null) {
            return null;
        }
        
        if(head.val == head.next?.val) {
            return DeleteDuplicates(DropVal(head.next, head.val));
        } else {
            head.next = DeleteDuplicates(head.next);
            return head;
        }
        
        ListNode DropVal(ListNode head, int target) =>
            head is null || head.val != target ? head : DropVal(head.next, target);
    }
    
    /*
     * 83. Remove Duplicates from Sorted List
     * https://leetcode.com/problems/remove-duplicates-from-sorted-list/
     */
    public ListNode DeleteDuplicates_(ListNode head)
    {
        if(head?.next is null) {
            return head;
        }
        
        head.next = DeleteDuplicates_(head.next);
        
        return head.val == head.next.val ? head.next : head;
    }
    
    /*
     * 206. Reverse Linked List
     * https://leetcode.com/problems/reverse-linked-list/
     */
    public ListNode ReverseList(ListNode head, ListNode prev = null)
    {
        if(head is null) {
            return prev;
        }
        
        var newHead = ReverseList(head.next, head);
        head.next = prev;
        return newHead;
    }
}
