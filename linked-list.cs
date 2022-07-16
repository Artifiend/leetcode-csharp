
using System;

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
     * 21. Merge Two Sorted Lists
     * https://leetcode.com/problems/merge-two-sorted-lists/
     */
    public ListNode MergeTwoLists(ListNode left, ListNode right)
    {
        if(left is null || right is null) {
            return left ?? right;
        }
        
        if(left.val <= right.val) {
            left.next = MergeTwoLists(left.next, right);
            return left;
        } else {
            right.next = MergeTwoLists(left, right.next);
            return right;
        }
    }
}
