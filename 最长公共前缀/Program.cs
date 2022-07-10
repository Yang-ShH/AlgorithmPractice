public class Solution
{
    public static void Main()
    {
        var result = LongestCommonPrefix(new string[] { "flower", "flow", "flight" });
        Console.WriteLine(result);
    }
    public static string LongestCommonPrefix(string[] strs)
    {
        var minStrLen = strs.Min(e => e.Length);
        var minStr = strs.First(e => e.Length == minStrLen);
        var publicStr = string.Empty;
        for (int i = 0; i < minStr?.Length; i++)
        {
            if (strs.All(e => e[i] == minStr[i]))
            {
                publicStr += minStr[i];
            }
            else
            {
                break;
            }
        }
        return publicStr;
    }
}