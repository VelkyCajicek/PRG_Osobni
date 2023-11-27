using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                if (Regex.IsMatch(input, @"^ary[1-4]\[[0-9]+,[0-9]+\]\[[0-9]+,[0-9]+\]$")) //ary1[1,0][4,2]
                {
                    string inputCheck = input.Substring(3, 1);
                    switch (inputCheck)
                    {
                        case "1":Switch(Ary1, input); WriteArray(Ary1); break;
                        case "2":Switch(Ary2, input); WriteArray(Ary2); break;
                        case "3":Switch(Ary3, input); WriteArray(Ary3); break;
                        case "4":Switch(Ary4, input); WriteArray(Ary4); break;
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
                    input = Regex.Replace(input, "[^0-9]", "");
                    int ArrayChooser = int.Parse(input[0].ToString());
                    switch (ArrayChooser)
                    {
                        case 1: Ary1 = Array0; Console.WriteLine("Array saved to Ary1"); break;
                        case 2: Ary2 = Array0; Console.WriteLine("Array saved to Ary2"); break;
                        case 3: Ary3 = Array0; Console.WriteLine("Array saved to Ary3"); break;
                        case 4: Ary4 = Array0; Console.WriteLine("Array saved to Ary4"); break;
                    }
                }
                if (Regex.IsMatch(input, @"^switchrowary[0-9]+\[[0-9]+,[0-9]+\]$")) { SwitchingRow(input, Ary1, Ary2, Ary3, Ary4); }
                if (Regex.IsMatch(input, @"^switchcolary[0-9]+\[[0-9]+,[0-9]+\]$")) { SwitchingColumn(input, Ary1, Ary2, Ary3, Ary4); }
                if (input.Contains("+") || input.Contains("-") || input.Contains("*"))
                {
                    if (input.Contains("+"))
                    {
                        int position = input.IndexOf("+");
                        Ary4 = ArrayAddition(AddSubMult1(input, position, Ary1, Ary2, Ary3, Ary4), AddSubMult2(input, position, Ary1, Ary2, Ary3, Ary4));
                    }
                    if (input.Contains("-"))
                    {
                        int position = input.IndexOf("-");
                        Ary4 = ArraySubtraction(AddSubMult1(input, position, Ary1, Ary2, Ary3, Ary4), AddSubMult2(input, position, Ary1, Ary2, Ary3, Ary4));
                    }
                    if (input.Contains("*"))
                    {
                        int position = input.IndexOf("*");
                        Ary4 = ArrayMultiplication(AddSubMult1(input, position, Ary1, Ary2, Ary3, Ary4), AddSubMult2(input, position, Ary1, Ary2, Ary3, Ary4));
                    }
                    Console.WriteLine("Result saved to Ary4");
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
                if(Regex.IsMatch(input, @"^clearall$")) { Ary1 = new int[0, 0]; Ary2 = new int[0, 0]; Ary3 = new int[0, 0]; Ary4 = new int[0, 0]; Console.WriteLine("Cleared all"); }
                if(Regex.IsMatch(input, @"^2222$"))
                {
                    Ary1 = new int[3, 3]; Ary2 = new int[3, 3];
                    Ary1 = FillArray(Ary1); Ary2 = FillArray(Ary2);
                    Console.WriteLine("Done");
                }
            }
            Console.ReadKey();
        }
        static int[,] Transpose(int[,] array)
        {
            int[,] newArray = new int[array.GetLength(0), array.GetLength(1)];
            for (int j = 0; j < array.GetLength(0); j++)
                for (int r = 0; r < array.GetLength(1); r++) newArray[j, r] = array[r, j];
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
        static int[,] SwitchRow(int[,] array, int nRowSwap, int mRowSwap)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                int temp = array[nRowSwap, i];
                array[nRowSwap, i] = array[mRowSwap, i];
                array[mRowSwap, i] = temp;
            }
            return array;
        }
        static int[,] SwitchColumn(int[,] array, int nRowSwap, int mRowSwap)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int temp = array[i, nRowSwap];
                array[i, nRowSwap] = array[i, mRowSwap];
                array[i, mRowSwap] = temp;
            }
            return array;
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
            Console.WriteLine("Creating array:                                                    Displaying arrays:                                  \n");
            Console.ResetColor();
            Console.WriteLine("cfa - Creates all arrays and fill them randomly                    DisplayAll - Show all arrays");
            Console.WriteLine("Create[a x b] - Prepares a array with parameters a and b           DisplayAry1-4 - Displays specified array            \n");
            Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Array manipulation:                                                Math:                                               \n");
            Console.ResetColor();
            Console.WriteLine("Ary(1-4) [x, y][m, n] - Switch position of number in array         Operations (+,-,*) between two arrays E.g. Ary1 * Ary2");
            Console.WriteLine("Trn[Ary1-4] - Transposes the array along the main diagonal       Operations (+,-,*) between nums and array E.g. Ary1 + 1");
            Console.WriteLine("ClearAll - Clears all arrays");
            Console.WriteLine("SwitchColAry1-4[n,m] - Swaps coloumn n with m in specified array");
            Console.WriteLine("SwitchRowAry1-4[n,m] - Swaps row n with m in specified array");
            Console.WriteLine();
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
        static void SwitchingColumn(string input, int[,] Ary1, int[,] Ary2, int[,] Ary3, int[,] Ary4)
        {
            input = Regex.Replace(input, "[^0-9]", "");
            int ArrayChooser = int.Parse(input[0].ToString());
            int nRowSwap = int.Parse(input[1].ToString());
            int mRowSwap = int.Parse(input[2].ToString());
            switch (ArrayChooser)
            {
                case 1: SwitchColumn(Ary1, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
                case 2: SwitchColumn(Ary2, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
                case 3: SwitchColumn(Ary3, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
                case 4: SwitchColumn(Ary4, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
            }
        }
        static void SwitchingRow(string input, int[,] Ary1, int[,] Ary2, int[,] Ary3, int[,] Ary4)
        {
            input = Regex.Replace(input, "[^0-9]", "");
            int ArrayChooser = int.Parse(input[0].ToString());
            int nRowSwap = int.Parse(input[1].ToString());
            int mRowSwap = int.Parse(input[2].ToString());
            switch (ArrayChooser)
            {
                case 1: SwitchRow(Ary1, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
                case 2: SwitchRow(Ary2, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
                case 3: SwitchRow(Ary3, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
                case 4: SwitchRow(Ary4, nRowSwap, mRowSwap); Console.WriteLine("Done"); break;
            }
        }
        static int[,] AddSubMult1(string input, int position, int[,] Ary1, int[,] Ary2, int[,] Ary3, int[,] Ary4)
        {
            int[,] firstArray = new int[0, 0];
            string firstPart = input.Substring(0, input.Length - position - 1); // Ary1 + Ary2
            switch (firstPart)
            {
                case "ary1": firstArray = Ary1; break;
                case "ary2": firstArray = Ary2; break;
                case "ary3": firstArray = Ary3; break;
                case "ary4": firstArray = Ary4; break;
            }
            return firstArray;
        }
        static int[,] AddSubMult2(string input, int position, int[,] Ary1, int[,] Ary2, int[,] Ary3, int[,] Ary4)
        {
            int[,] secondArray = new int[0, 0];
            string secondPart = input.Substring(position + 1, input.Length - position - 1);
            switch (secondPart)
            {
                case "ary1": secondArray = Ary1; break;
                case "ary2": secondArray = Ary2; break;
                case "ary3": secondArray = Ary3; break;
                case "ary4": secondArray = Ary4; break;
            }
            return secondArray;
        }
    }
}
