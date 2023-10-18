using System;
using System.Collections.Generic;
using System.IO;
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
            List<string> URLS = new List<string>();
            //int n = int.Parse(Console.ReadLine()); // Nacteme cislo n, pro ktere budeme pocitat jeho faktorial a n-ty prvek Fibonacciho posloupnosti.
            //int factorial = Factorial(n); // Prvni zavolani pro vypocet faktorialu, ulozeni do promenne factorial.
            //int fibonacci = Fibonacci(n); // Prvni zavolani pro vypocet Fibonacciho posloupnosti, ulozeni do promenne fibonacci.
            //Console.WriteLine($"Pro cislo {n} je faktorial {factorial} a {n}. prvek Fibonacciho posloupnosti je {fibonacci}"); // Vypsani vysledku uzivateli.
            string input = "Programming";
            Files(URLS);
            URLS.ForEach(Console.WriteLine);
            Console.ReadKey();
        }

        static int Factorial(int n)
        {
            if (n == 1) { return 1; }
            else { return n * Factorial(n - 1); }
        }
        static int Fibonacci(int n)
        {
            if (n == 0) { return 0; }
            if (n == 1) { return 1; }
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
        static void Files(List<string> URLS)
        {
            string path = @"C:\\Users\\Klaudia Peichlova\\Documents";
            foreach(string folder in Directory.GetDirectories(path)) // Funguje
            {
                URLS.Add(folder);
            }
        }
    }
}