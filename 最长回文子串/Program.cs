public class Solution
{

    public static void Main()
    {
        var result = LongestPalindrome("bb");
    }
    public static string LongestPalindrome(string s)
    {
        int n = s.Length;
        bool[,] P = new bool[n, n];
        string result = "";
        //遍历所有的长度
        for (int len = 1; len <= n; len++)
        {
            for (int start = 0; start < n; start++)
            {
                int end = start + len - 1;
                if (end >= n) //下标已经越界，结束本次循环
                    break;
                //长度为 1 和 2 的单独判断下
                P[start, end] = (len == 1 || len == 2 || P[start + 1, end - 1]) && s[start] == s[end];
                if (P[start, end] && len > result.Length)
                {
                    result = s.Substring(start, len);
                }
            }
        }
        return result;
    }

}