using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingPlayground
{
    internal class Program
    {
        static int[] BubbleSort(int[] array)
        {
            int[] sortedArray = (int[])array.Clone(); // Řaď v tomto poli, ve kterém je výchoze zkopírováno všechno ze vstupního pole.
            for (int i = 0; i < sortedArray.GetLength(0); i++)
            {
                for (int j = 0; j < sortedArray.GetLength(0) - 1; j++)
                {
                    if (sortedArray[j] > sortedArray[j + 1])
                    {
                        int temp = sortedArray[j];
                        sortedArray[j] = sortedArray[j + 1];
                        sortedArray[j + 1] = temp;
                    }
                }
            }
            return sortedArray;
        }

        static int[] SelectionSort(int[] array)
        {
            int[] sortedArray = (int[])array.Clone(); // Řaď v tomto poli, ve kterém je výchoze zkopírováno všechno ze vstupního pole.
            int minimum = 0; int index = 0;
            for (int i = 0; i < sortedArray.GetLength(0); i++)
            {
                minimum = sortedArray[i];
                for (int j = i; j < sortedArray.GetLength(0); j++)
                {
                    if (sortedArray[j] < minimum)
                    {
                        minimum = sortedArray[j]; index = j;
                    }
                }
                sortedArray[index] = sortedArray[i];
                sortedArray[i] = minimum;
            }
            return sortedArray;
        }

        static int[] InsertionSort(int[] array)
        {
            int[] sortedArray = (int[])array.Clone(); // Řaď v tomto poli, ve kterém je výchoze zkopírováno všechno ze vstupního pole.
            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < sortedArray.Length; j++)
                {
                    if (sortedArray[j] < sortedArray[min])
                    {
                        min = j;
                    }
                }
                var tempVar = sortedArray[min];
                sortedArray[min] = sortedArray[i];
                sortedArray[i] = tempVar;
            }
            return sortedArray;
        }

        //Naplní pole náhodnými čísly mezi 1 a velikostí pole.
        static void FillArray(int[] array)
        {
            Random rng = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rng.Next(1, array.Length + 1);
            }
        }

        //Vypíše pole do konzole.
        static void WriteArrayToConsole(int[] array, string arrayName)
        {
            Console.Write(arrayName + " = [");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                if (i < array.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.Write("]\n\n");
        }

        //Zavolá postupně Bubble sort, Selection sort a Insertion sort pro zadané pole (a vypíše jeho jméno pro přehlednost)
        static void SortArray(int[] array, string arrayName)
        {
            Console.WriteLine($"Řadím {arrayName}:");
            int[] sortedArray;

            sortedArray = BubbleSort(array);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Bubble sortem");

            sortedArray = SelectionSort(array);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Selection sortem");

            sortedArray = InsertionSort(array);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Insertion sortem");

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] smallArray = new int[10];
            FillArray(smallArray);

            //int[] mediumArray = new int[100];
            //FillArray(mediumArray);

            //int[] largeArray = new int[1000];
            //FillArray(largeArray);

            WriteArrayToConsole(smallArray, "Malé pole");
            SortArray(smallArray, "Malé pole");

            //WriteArrayToConsole(mediumArray, "Střední pole");
            //SortArray(mediumArray, "Střední pole");

            //WriteArrayToConsole(largeArray, "Velké pole");
            //SortArray(largeArray, "Velké pole");

            Console.ReadKey();
        }
    }
}
