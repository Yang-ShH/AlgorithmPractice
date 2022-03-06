
public class Solution
{
    public static void Main()
    {
        TwoSum(new int[] { 3, 2, 4 }, 6);
    }
    public static int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> kvs = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            var anotherNum = target - nums[i];
            if(kvs.ContainsKey(anotherNum) && kvs[anotherNum] != i)
            {
                return new int[] { i, kvs[anotherNum] };
            }
            if (!kvs.ContainsKey(anotherNum))
            {
                kvs.Add(nums[i], i);
            }
        }
        return new int[] { 0, 0 };
    }
}
