public class Solution
{
    public static void Main()
    {
        var result = FindMedianSortedArrays(new int[] {1,1}, new int[] {1,2});
    }

    public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var nums = nums1.Concat(nums2).OrderBy(e => e).ToArray();
        var len = nums.Length;
        if (len % 2 == 0)
        {
            return (double)(nums[len / 2] + nums[(len / 2) - 1]) / 2;
        }
        else
        {
            return (double)nums[(len - 1) / 2];
        }
    }
}