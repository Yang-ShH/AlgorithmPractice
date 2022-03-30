public class Solution
{
    public static void Main()
    {
        var result = Convert("abcdefghijklmnopqrst", 5);
    }
    public static string Convert(string s, int numRows)
    {
        if (numRows < 2 || s.Length <= numRows)
        {
            return s;
        }

        var charLists = new List<List<char>>();
        for (int i = 0; i < numRows; i++)
        {
            charLists.Add(new List<char>());
        }

        int times = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (i <= numRows - 2)
            {
                charLists[i].Add(s[i]);
            }
            else
            {
                var remainder = i % (numRows - 1);
                if (remainder == 0)
                {
                    charLists.Reverse();
                    times++;
                }
                charLists[remainder].Add(s[i]);
            }
        }
        if (times % 2 != 0)
        {
            charLists.Reverse();
        }
        string result = "";
        foreach (var charList in charLists)
        {
            var tempStr = string.Join("", charList);
            result = result + tempStr;
        }
        return result;
    }
}