public class Solution
{
    public static void Main()
    {
        var result = Reverse(123);
    }

    public static int Reverse(int x)
    {
        try
        {
            var flag = false;
            string str = string.Empty;
            if (x > 0)
            {
                str = x.ToString();
            }
            else if (x < 0)
            {
                flag = true;
                str = (-x).ToString();
            }
            else
            {
                return x;
            }

            var queue = str.ToArray();
            Array.Reverse(queue);
            var result = new string(queue);
            if (flag)
            {
                result = "-" + result;
            }
            return Convert.ToInt32(result);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int Reverse2(int x)
    {
        int rev = 0;
        while (x != 0)
        {
            if (rev < int.MinValue / 10 || rev > int.MaxValue / 10)
            {
                return 0;
            }
            int digit = x % 10;
            x /= 10;
            rev = rev * 10 + digit;
        }
        return rev;
    }
}
