using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RecursionPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); // Nacteme cislo n, pro ktere budeme pocitat jeho faktorial a n-ty prvek Fibonacciho posloupnosti.
            int factorial = Factorial(n); // Prvni zavolani pro vypocet faktorialu, ulozeni do promenne factorial.
            int fibonacci = Fibonacci(n); // Prvni zavolani pro vypocet Fibonacciho posloupnosti, ulozeni do promenne fibonacci.
            Console.WriteLine($"Pro cislo {n} je faktorial {factorial} a {n}. prvek Fibonacciho posloupnosti je {fibonacci}"); // Vypsani vysledku uzivateli.
            Console.ReadKey();
        }

        static int Factorial(int n)
        {
            int resultF = 1;
            for (int i = 1; i < n+1; i++) { resultF *= i; }
            return resultF;
        }
        static int Fibonacci(int n)
        {
            double Nd = double.Parse(n.ToString());
            double resultD = (int)Math.Pow(1 + Math.Sqrt(5), Nd) / (int)(Math.Pow(2, n) * Math.Sqrt(5));
            int resultI = (int)Math.Round(resultD);
            return resultI;
        }
    }
}
