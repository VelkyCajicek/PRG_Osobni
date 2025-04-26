using System;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            int[] arr = { 0, 1, 2, 3, 5 };
            Console.WriteLine(FactorialR(n));
            Console.WriteLine(FactorialF(n));
            Console.WriteLine(FactorialW(n));
            Console.ReadKey();
        }
        static int FactorialR(int n)
        {
            return n == 0 ? 1 : n * FactorialR(n - 1);
        }
        static int FactorialF(int n)
        {
            int value = 1;
            for (int i = 1; i < n + 1; i++)
            {
                value *= i;
            }
            return value;
        }
        static int FactorialW(int n)
        {
            int value = 1;
            int i = 0;
            while (i < n)
            {
                i++;
                value *= i;
            }
            return value;
        }
    }
}
