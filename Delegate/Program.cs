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
}
