using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace RecursionPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            int n = int.Parse(Console.ReadLine()); // Nacteme cislo n, pro ktere budeme pocitat jeho faktorial a n-ty prvek Fibonacciho posloupnosti.
            int factorial = Factorial(n); // Prvni zavolani pro vypocet faktorialu, ulozeni do promenne factorial.
            int fibonacci = Fibonacci(n); // Prvni zavolani pro vypocet Fibonacciho posloupnosti, ulozeni do promenne fibonacci.
            Console.WriteLine($"Pro cislo {n} je faktorial {factorial} a {n}. prvek Fibonacciho posloupnosti je {fibonacci}"); // Vypsani vysledku uzivateli.
            */
            Files();
            Console.ReadKey();
        }

        static int Factorial(int n)
        {
            if(n == 1) { return 1; }
            else { return n * Factorial(n - 1); }
        }
        static int Fibonacci(int n)
        {
            if(n == 1) { return 1; };
            if(n == 0) {  return 0; }
            return Fibonacci(n - 1) + Fibonacci(n-2);
        }
        static void Files()
        {
            string path = @"E:\\Tobias";
            foreach(string folder in Directory.GetDirectories(path))
            {
                Console.WriteLine(folder);
            }
            foreach (string file in Directory.GetFiles(path))
            {
                Console.WriteLine(file);
            }
        }
    }
}
