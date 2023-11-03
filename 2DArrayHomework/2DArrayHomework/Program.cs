using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2DArrayHomework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] Ary1 = new int[0, 0]; int[,] Ary2 = new int[0, 0]; int[,] Ary3 = new int[0, 0]; int[,] Ary4 = new int[0, 0];
            Instructions();
            while (true)
            {
                Random rnd = new Random();
                string input = Console.ReadLine();
                input = input.Replace(" ", "").ToLower();
                if (Regex.IsMatch(input, @"^cfa$") == true)
                {
                    Ary1 = new int[rnd.Next(1, 5), rnd.Next(1, 5)];
                    Ary2 = new int[rnd.Next(1, 5), rnd.Next(1, 5)];
                    Ary3 = new int[rnd.Next(1, 5), rnd.Next(1, 5)];
                    Ary4 = new int[rnd.Next(1, 5), rnd.Next(1, 5)];
                    Ary1 = FillArray(Ary1); Ary2 = FillArray(Ary2); Ary3 = FillArray(Ary3); Ary4 = FillArray(Ary4);
                    WriteArray(Ary1); Console.WriteLine("Ary1 has the parameteres: " + Ary1.GetLength(1) + " X " + Ary1.GetLength(0));
                    Console.WriteLine("\n");
                    WriteArray(Ary2); Console.WriteLine("Ary2 has the parameteres: " + Ary2.GetLength(1) + " X " + Ary2.GetLength(0));
                    Console.WriteLine("\n");
                    WriteArray(Ary3); Console.WriteLine("Ary3 has the parameteres: " + Ary3.GetLength(1) + " X " + Ary3.GetLength(0));
                    Console.WriteLine("\n");
                    WriteArray(Ary4); Console.WriteLine("Ary4 has the parameteres: " + Ary4.GetLength(1) + " X " + Ary4.GetLength(0));
                }
                if (Regex.IsMatch(input, @"^ary[1-4]\[[0-9]+,[0-9]+\]\[[0-9]+,[0-9]+\]$") == true) //ary1[1,0][4,2]
                {
                    string inputCheck = input.Substring(3, 1);
                    switch (inputCheck)
                    {
                        case "1":
                            Switch(Ary1, input);
                            WriteArray(Ary1);
                            break;
                        case "2":
                            Switch(Ary2, input);
                            WriteArray(Ary2);
                            break;
                        case "3":
                            Switch(Ary3, input);
                            WriteArray(Ary3);
                            break;
                        case "4":
                            Switch(Ary4, input);
                            WriteArray(Ary4);
                            break;
                    }
                }
                if (input.Contains("+") || input.Contains("-") || input.Contains("*"))
                {
                    string inputReserve = input.Substring(0, 4);
                    string num = input.Substring(5, input.Length - 5);
                    input = Regex.Replace(input, @"[A-Za-z0-9]+", "");
                    int z = 0;
                    switch (input)
                    {
                        case "+": z = 0; break;
                        case "-": z = 1; break;
                        case "*": z = 2; break;
                    }
                    switch (inputReserve)
                    {
                        case "ary1":
                            Calculation(Ary1, z, num); WriteArray(Ary1); break;
                        case "ary2":
                            Calculation(Ary2, z, num); WriteArray(Ary2); break;
                        case "ary3":
                            Calculation(Ary3, z, num); WriteArray(Ary3); break;
                        case "ary4":
                            Calculation(Ary4, z, num); WriteArray(Ary4); break;
                    }
                }
            }
            Console.ReadKey();
        }
        static int[,] FillArray(int[,] array)
        {
            Random rnd = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = rnd.Next(0, 9);
                }
            }
            return array;
        }
        static void WriteArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write("{");
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(" " + array[i, j] + " ");
                }
                Console.Write("}\n");
            }
        }
        static void Switch(int[,] array, string input)
        {
            string numbers = input.Substring(4, input.Length - 4);
            String[] seperators = { "[", "," };
            String[] strlist = numbers.Split(seperators, 50, StringSplitOptions.RemoveEmptyEntries);
            int[] numbersToInt = { };
            for (int i = 0; i < strlist.Length; i++)
            {
                strlist[i] = Regex.Replace(strlist[i], "[^.0-9]", "");
            }
            int a = array[int.Parse(strlist[0]), int.Parse(strlist[1])];
            int b = array[int.Parse(strlist[2]), int.Parse(strlist[3])];
            array[int.Parse(strlist[0]), int.Parse(strlist[1])] = b;
            array[int.Parse(strlist[2]), int.Parse(strlist[3])] = a;
        }
        static void Instructions()
        {
            Console.WriteLine("Command list: \n");
            Console.WriteLine("Create[a x b] - Create 1 new array");
            Console.WriteLine("CFA - Creates and fills all arrays");
            Console.WriteLine("Fill[min, max] - Autofill array with numbers between min and max value");
            Console.WriteLine("Ary(1-4) [x, y][m, n] - Switch position of number in array");
            Console.WriteLine();
        }
        static void Calculation(int[,] array, int z, string num)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    switch (z)
                    {
                        case 0:
                            int s = array[i, j];
                            array[i, j] = s + int.Parse(num);
                            break;
                        case 1:
                            s = array[i, j];
                            array[i, j] = s - int.Parse(num);
                            break;
                        case 2:
                            s = array[i, j];
                            array[i, j] = s * int.Parse(num);
                            break;

                    }
                }
            }
        }
    }
}
