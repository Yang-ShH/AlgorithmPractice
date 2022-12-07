using System.Diagnostics.CodeAnalysis;

public class DelegateTest
{
    public delegate void MyDelegate(int num);

    public static int P = 10;
    public static void Add(int num)
    {
        P += num;
    }

    public static void Multiplication(int num)
    {
        P *= num;
    }

    public static void Main()
    {
        MyDelegate d1 = new MyDelegate(Add);
        d1 += new MyDelegate(Multiplication);
        d1(10);
        Console.WriteLine($"P = {P}");
        Console.ReadLine();
    }


    public void ObjDistinct()
    {
        var list = new List<WaitingMovingRacksInfo>()
        {
            new WaitingMovingRacksInfo()
            {
                RackStorageId = 1,
                RackCode = "123",
                RackType = 258,
                SumPosNum = 96
            },
            new WaitingMovingRacksInfo()
            {
                RackStorageId = 1,
                RackCode = "123",
                RackType = 258,
                SumPosNum = 96
            },
        };
        var list1 = list.Distinct().ToList();
        var list2 = list.Distinct(new WaitingMovingRacksInfoComparer()).ToList();
    }

    public class WaitingMovingRacksInfo
    {
        public int RackStorageId { get; set; }
        public short RackType { get; set; }
        public string RackCode { get; set; }
        public int SumPosNum { get; set; }
    }

    public class WaitingMovingRacksInfoComparer : IEqualityComparer<WaitingMovingRacksInfo>
    {
        public bool Equals(WaitingMovingRacksInfo? x, WaitingMovingRacksInfo? y)
        {
            return x?.RackCode.Equals(y?.RackCode) ?? false;
        }

        public int GetHashCode([DisallowNull] WaitingMovingRacksInfo obj)
        {
            return obj.RackCode.GetHashCode();
        }
    }
}
