public class Solution
{
    public static void Main()
    {
        var result = LengthOfLongestSubstring("abcabcbb");
        var result2 = LengthOfLongestSubstring2("abcabcbb");
        var result3 = LengthOfLongestSubstring3("abcabcbb");
    }
    public static int LengthOfLongestSubstring(string s)
    {
        var result = s.Length == 0 ? 0 : 1;
        for (int i = 0; i < s.Length - 1; i++)
        {
            var strList = new List<char>() { s[i] };
            for (int j = i + 1; j < s.Length; j++)
            {
                if (!strList.Contains(s[j]))
                {
                    strList.Add(s[j]);
                }
                else
                {
                    break;
                }
            }
            if (strList.Count > result) result = strList.Count;
        }

        return result;
    }

    public static int LengthOfLongestSubstring2(string s)
    {
        var charHash = new HashSet<char>();
        int left = 0;
        int right = 0;
        int temp = 0;
        int result = 0;
        while (right < s.Length)
        {
            if (!charHash.Contains(s[right]))
            {
                charHash.Add(s[right]);
                right++;
                temp++;
            }
            else
            {
                charHash.Remove(s[left]);
                left++;
                temp--;
            }
            result = Math.Max(temp, result);
        }

        return result;
    }

    public static int LengthOfLongestSubstring3(string s)
    {
        int result = 0;
        var charQueue = new Queue<char>();
        foreach (var item in s)
        {
            while (charQueue.Contains(item))
            {
                charQueue.Dequeue();
            }
            charQueue.Enqueue(item);
            if (charQueue.Count > result)
            {
                result = charQueue.Count;
            }
        }

        return result;
    }
}