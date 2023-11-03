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
<<<<<<< HEAD
            /*
            int n = int.Parse(Console.ReadLine()); // Nacteme cislo n, pro ktere budeme pocitat jeho faktorial a n-ty prvek Fibonacciho posloupnosti.
            int factorial = Factorial(n); // Prvni zavolani pro vypocet faktorialu, ulozeni do promenne factorial.
            int fibonacci = Fibonacci(n); // Prvni zavolani pro vypocet Fibonacciho posloupnosti, ulozeni do promenne fibonacci.
            Console.WriteLine($"Pro cislo {n} je faktorial {factorial} a {n}. prvek Fibonacciho posloupnosti je {fibonacci}"); // Vypsani vysledku uzivateli.
            */
            Files();
=======
            List<string> URLS = new List<string>();
            //int n = int.Parse(Console.ReadLine()); // Nacteme cislo n, pro ktere budeme pocitat jeho faktorial a n-ty prvek Fibonacciho posloupnosti.
            //int factorial = Factorial(n); // Prvni zavolani pro vypocet faktorialu, ulozeni do promenne factorial.
            //int fibonacci = Fibonacci(n); // Prvni zavolani pro vypocet Fibonacciho posloupnosti, ulozeni do promenne fibonacci.
            //Console.WriteLine($"Pro cislo {n} je faktorial {factorial} a {n}. prvek Fibonacciho posloupnosti je {fibonacci}"); // Vypsani vysledku uzivateli.
            string input = "Programming";
            Files(URLS);
            URLS.ForEach(Console.WriteLine);
>>>>>>> 9efad4cbb5470a5258086a22f450a3a22e1d34d4
            Console.ReadKey();
        }

        static int Factorial(int n)
        {
<<<<<<< HEAD
            if(n == 1) { return 1; }
=======
            if (n == 1) { return 1; }
>>>>>>> 9efad4cbb5470a5258086a22f450a3a22e1d34d4
            else { return n * Factorial(n - 1); }
        }
        static int Fibonacci(int n)
        {
<<<<<<< HEAD
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
=======
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
>>>>>>> 9efad4cbb5470a5258086a22f450a3a22e1d34d4
            }
        }
    }
}