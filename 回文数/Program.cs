public class Solution
{
    public static void Main()
    {
        var x = 12321;
        var result = IsPalindrome(x);
        var result2 = IsPalindrome2(x);
    }

    public static bool IsPalindrome(int x)
    {
        if (x < 0 || (x % 10 == 0 && x != 0))
        {
            return false;
        }

        int revertedNumber = 0;
        while (x > revertedNumber)
        {
            revertedNumber = revertedNumber * 10 + x % 10;
            x /= 10;
        }
        
        return x == revertedNumber || x == revertedNumber / 10;
    }

    public static bool IsPalindrome2(int x)
    {
        try
        {
            if (x < 0 || (x % 10 == 0 && x != 0))
            {
                return false;
            }

            var temp = x;
            int revertedNumber = 0;
            while (temp > 0)
            {
                revertedNumber = revertedNumber * 10 + temp % 10;
                temp /= 10;
            }

            return x == revertedNumber;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}