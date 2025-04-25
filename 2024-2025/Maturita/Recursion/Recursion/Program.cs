using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Recursion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int testValue = 5;
            //Console.WriteLine(Factorial(testValue));
            //Console.WriteLine(Fibonnaci(testValue));
            Console.WriteLine(Countdown(testValue));
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
        static int Fibonnaci(int n)
        {
            if(n == 0)
            {
                return 0;
            }
            if(n == 1)
            {
                return 1;
            }
            else
            {
                return Fibonnaci(n - 1) + Fibonnaci(n - 2);
            }
        }
        static int Countdown(int n)
        {
            if(n == 0)
            {
                return 0;
            }
            else
            {
                Console.WriteLine(n);
                return Countdown(n - 1);
            }
        }
    }
}
