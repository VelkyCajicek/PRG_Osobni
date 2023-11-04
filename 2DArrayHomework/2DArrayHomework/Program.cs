using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
                    Console.WriteLine("All arrays created");
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
                if (Regex.IsMatch(input, @"^create\[[0-9]+x[0-9]+\]$") == true) // Create[5x6]
                {
                    int[,] Array0 = new int[0, 0];
                    char xChar = input[7]; char yChar = input[9];
                    int x = int.Parse(xChar.ToString()); int y = int.Parse(yChar.ToString());
                    Array0 = new int[y, x];
                    Console.WriteLine("You are now editing a {0} x {1} array, for autofill type 'Fill[max value]', for manual fill type 'Manual'", x, y);
                    string inputCreate = Console.ReadLine();
                    inputCreate = inputCreate.Replace(" ", "").ToLower();
                    if (Regex.IsMatch(inputCreate, @"^fill\[[0-9]+\]$") == true) // Fill[9]
                    {
                        inputCreate = Regex.Replace(inputCreate, "[^.0-9]", "");
                        for (int i = 0; i < Array0.GetLength(0); i++)
                        {
                            Console.Write("{");
                            for (int j = 0; j < Array0.GetLength(1); j++)
                            {
                                Array0[i, j] = rnd.Next(int.Parse(inputCreate)); ;
                                Console.Write(" " + Array0[i, j] + " ");
                                Thread.Sleep(100);
                            }
                            Console.Write("}\n");
                        }
                        Console.WriteLine("Array filled with numbers from 0 to {0}", inputCreate);
                    }
                    else
                    {
                        Console.WriteLine("Now type numbers you want added to array");
                        for (int i = 0; i < Array0.GetLength(0); i++)
                        {
                            for (int j = 0; j < Array0.GetLength(1); j++)
                            {
                                string Builder = Console.ReadLine();
                                Array0[i, j] = int.Parse(Builder);
                            }
                        }
                        Console.WriteLine("Array is full");
                    }
                    Console.WriteLine("Which array do you want to save it to? (E.g. Ary1, Ary2, Ary3, Ary4)");
                    string inputSave = Console.ReadLine();
                    inputSave = inputSave.ToLower();
                    switch (inputSave)
                    {
                        case "ary1": Ary1 = Array0; Console.WriteLine("Array saved to Ary1"); break;
                        case "ary2": Ary2 = Array0; Console.WriteLine("Array saved to Ary2"); break;
                        case "ary3": Ary3 = Array0; Console.WriteLine("Array saved to Ary3"); break;
                        case "ary4": Ary4 = Array0; Console.WriteLine("Array saved to Ary4"); break;
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
                        case "ary1": Calculation(Ary1, z, num); WriteArray(Ary1); break;
                        case "ary2": Calculation(Ary2, z, num); WriteArray(Ary2); break;
                        case "ary3": Calculation(Ary3, z, num); WriteArray(Ary3); break;
                        case "ary4": Calculation(Ary4, z, num); WriteArray(Ary4); break;
                    }
                }
                if(Regex.IsMatch(input, @"^displayall$"))
                {
                    WriteArray(Ary1); Console.WriteLine("Ary1 has the parameteres: " + Ary1.GetLength(1) + " X " + Ary1.GetLength(0));
                    Console.WriteLine("\n");
                    WriteArray(Ary2); Console.WriteLine("Ary2 has the parameteres: " + Ary2.GetLength(1) + " X " + Ary2.GetLength(0));
                    Console.WriteLine("\n");
                    WriteArray(Ary3); Console.WriteLine("Ary3 has the parameteres: " + Ary3.GetLength(1) + " X " + Ary3.GetLength(0));
                    Console.WriteLine("\n");
                    WriteArray(Ary4); Console.WriteLine("Ary4 has the parameteres: " + Ary4.GetLength(1) + " X " + Ary4.GetLength(0));
                }
                if (Regex.IsMatch(input, @"^displayary[0-9]+$"))
                {
                    switch (input)
                    {
                        case "displayary1":
                            WriteArray(Ary1); Console.WriteLine("Ary1 has the parameteres: " + Ary1.GetLength(1) + " X " + Ary1.GetLength(0)); break;
                        case "displayary2":
                            WriteArray(Ary2); Console.WriteLine("Ary2 has the parameteres: " + Ary2.GetLength(1) + " X " + Ary2.GetLength(0)); break;
                        case "displayary3":
                            WriteArray(Ary3); Console.WriteLine("Ary3 has the parameteres: " + Ary3.GetLength(1) + " X " + Ary3.GetLength(0)); break;
                        case "displayary4":
                            WriteArray(Ary4); Console.WriteLine("Ary4 has the parameteres: " + Ary4.GetLength(1) + " X " + Ary4.GetLength(0)); break;
                    }
                }
                if(Regex.IsMatch(input, @"^trn\[ary[0-9]+\]$"))
                {
                    switch(input)
                    {
                        case ("trn[ary1]"): Ary1 = Transpose(Ary1); break;
                        case ("trn[ary2]"): Ary2 = Transpose(Ary2); break;
                        case ("trn[ary3]"): Ary3 = Transpose(Ary3); break;
                        case ("trn[ary4]"): Ary4 = Transpose(Ary4); break;
                    }
                    Console.WriteLine("Array transposed along main diagonal");
                }
                if( Regex.IsMatch(input, @"^1111$"))
                {
                    Ary4 = ArrayAddition(Ary1, Ary2);
                }
            }
            Console.ReadKey();
        }
        static int[,] Transpose(int[,] array)
        {
            int[,] newArray = new int[array.GetLength(0), array.GetLength(1)];
            for (int j = 0; j < array.GetLength(0); j++)
                for (int r = 0; r < array.GetLength(1); r++)
                    newArray[j, r] = array[r, j];
            return newArray;
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
            Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Command list:\n");
            Console.WriteLine("Creating array:                                                    Displaying arrays:                                          \n");
            Console.ResetColor();
            Console.WriteLine("cfa - Creates all arrays and fill them randomly                    DisplayAll - Show all arrays");
            Console.WriteLine("Create[a x b] - Prepares a array with parameters a and b           DisplayAry(1-4) - Displays specified array\n");
            Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Array manipulation:                                                Math: (Code is finished but not yet implemented :/)                                                      \n");
            Console.ResetColor();
            Console.WriteLine("Ary(1-4) [x, y][m, n] - Switch position of number in array         Operations (+,-,*) between two arrays E.g. Ary1 * Ary2");
            Console.WriteLine("Trn[NameOfArray] - Transposes the array along the main diagonal    Operations (+,-,*) between nums and array E.g. Ary1 + 1");
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
        static int[,] ArrayMultiplication(int[,] array1, int[,] array2)
        {
            int result = 0; int[,] matrixResult = new int[array1.GetLength(0), array2.GetLength(1)];
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array2.GetLength(1); j++)
                {
                    result = 0;
                    for (int k = 0; k < array1.GetLength(1); k++)
                    {
                        result += array1[i, k] * array2[k, j];
                    }
                    matrixResult[i, j] = result;
                }
            }
            return matrixResult;
        }
        static int[,] ArraySubtraction(int[,] array1, int[,] array2)
        {
            int[,] matrixResult = new int[array1.GetLength(0), array2.GetLength(1)];
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    matrixResult[i,j] = array1[i,j] - array2[i,j];
                }
            }
            return matrixResult;
        }
        static int[,] ArrayAddition(int[,] array1, int[,] array2)
        {
            int[,] matrixResult = new int[array1.GetLength(0), array2.GetLength(1)];
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    matrixResult[i, j] = array1[i, j] + array2[i, j];
                }
            }
            return matrixResult;
        }
    }
}
