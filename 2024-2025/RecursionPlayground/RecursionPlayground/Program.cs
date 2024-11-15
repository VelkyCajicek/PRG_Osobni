using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursionPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int result = Factorial(3);
            int resultFib = Fibonacci(6);
            Console.WriteLine(result);
            Console.WriteLine(resultFib);
            Console.ReadKey();
        }
        static int Factorial(int n)
        {
            if(n == 1)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }
        static int Fibonacci(int n)
        {
            if(n == 1)
            {
                return 1;
            }
            if(n == 0)
            {
                return 0;
            }
            else
            {
                return Fibonacci(n - 1) + Fibonacci(n-2);
            }
        }
    }
}
