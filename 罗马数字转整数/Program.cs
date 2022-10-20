public class Solution
{
    static Dictionary<char, int> romanValue = new Dictionary<char, int>
    {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };

    public static void Main()
    {
        var result = RomanToInt("XLIX");
    }

    public static int RomanToInt(string s)
    {
        int result = 0;
        int n = s.Length;
        for (int i = 0; i < n; i++)
        {
            int value = romanValue[s[i]];
            if (i < n-1 && value < romanValue[s[i+1]])
            {
                result -= value;
            }
            else
            {
                result += value;
            }
        }
        return result;
    }
}