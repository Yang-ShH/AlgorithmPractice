public class Solution
{
    public static void Main()
    {
        _ = MaxArea(new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 });
    }

    public static int MaxArea(int[] height)
    {
        var r = height.Length - 1;
        var l = 0;
        var ans = 0;
        while (l < r)
        {
            var s = Math.Min(height[l], height[r]) * (r - l);
            ans = Math.Max(ans, s);
            if (height[l] > height[r])
            {
                r--;
            }
            else
            {
                l++;
            }
        }
        return ans;
    }
}