public class Solution
{
    public static void Main()
    {
        var result = MyAtoi("-12345");
    }

    public static int MyAtoi(string s)
    {
        // 为空直接返回
        if (s.Length == 0) return 0;
        // 正负号
        int plus = 1;
        // 结果
        int result = 0;
        // 去空格
        if (s[0] == ' ')
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    s = s[i..s.Length];
                    break;
                }
            }
        }
        // 判断正负号
        if (s[0] == '-')
        {
            plus = -1;
            s = s[1..s.Length];
        }
        else if (s[0] == '+') s = s[1..s.Length];
        // 逐位判断
        for (int i = 0; i < s.Length; i++)
        {
            // 数字
            if (s[i] >= '0' && s[i] <= '9')
            {
                // 判断是否溢出
                if (plus == 1)
                {
                    if (result > int.MaxValue / 10) return int.MaxValue;
                    if (result == int.MaxValue / 10 && s[i] >= '7') return int.MaxValue;
                }
                else
                {
                    if (result > int.MaxValue / 10) return int.MinValue;
                    if (result == int.MaxValue / 10 && s[i] >= '8') return int.MinValue;
                }
                // 更新结果
                result = result * 10 + (s[i] - '0');
            }
            // 非数字，直接返回结果
            else return result * plus;
        }
        // 判断完毕，返回结果
        return result * plus;
    }
}