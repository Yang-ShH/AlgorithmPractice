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

public class Solution
{
    public static void Main()
    {
        var l1 = new ListNode() { val = 2, next = new ListNode { val = 4, next = new ListNode { val = 3 } } };
        var l2 = new ListNode() { val = 5, next = new ListNode { val = 6, next = new ListNode { val = 4 } } };
        var result = AddTwoNumbers(l1, l2);
    }
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var result = new ListNode();
        var pre = result;

        int t = 0;
        while (l1 != null || l2 != null || t != 0)
        {
            if (l1 != null)
            {
                t += l1.val;
                l1 = l1.next;
            }
            if (l2 != null)
            {
                t += l2.val;
                l2 = l2.next;
            }
            pre.next = new ListNode(t % 10);
            pre = pre.next;
            t /= 10;
        }
        return result.next;
    }
}