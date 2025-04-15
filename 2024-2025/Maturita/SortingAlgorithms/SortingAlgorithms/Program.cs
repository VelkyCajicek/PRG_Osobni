using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Line to change the language
            // System.Globalization.CultureInfo.DefaultThreadCurrentUICulture=System.Globalization.CultureInfo.GetCultureInfo("en-US");
            int arraySize = 10;
            int[] array = new int[arraySize];
            FillArray(array);
            DisplayArray(array);
            QuickSort(array, 0, arraySize - 1);
            DisplayArray(array);
            Console.ReadKey();
        }
        static void FillArray(int[] anyArray)
        {
            Random rnd = new Random();
            for (int i = 0; i < anyArray.Length; i++)
            {
                anyArray[i] = rnd.Next(0, 100);
            }
        }

        static void DisplayArray(int[] anyArray)
        {
            for (int i = 0; i < anyArray.Length; i++)
            {
                Console.Write($"{anyArray[i]}, ");
            }
            Console.WriteLine();
        }

        static int[] SelectionSort(int[] anyArray) // Time complexity O(n^2)
        {
            // Main idea - Starts with first element, checks every one after that for a minimum, swaps accordingly
            int n = anyArray.Length;

            int iMin;
            int temp;

            for (int i = 0; i < n - 1; i++)
            {
                iMin = i; // Assumes the minimum is the first element
                for (int j = i + 1; j < n; j++) // Tests against all elements after i to find the smallest
                {
                    if (anyArray[j] < anyArray[iMin]) // If this element is less, then it is the new minimum
                    {
                        iMin = j;
                    }
                }
                // Swaps the elements
                temp = anyArray[i];
                anyArray[i] = anyArray[iMin];
                anyArray[iMin] = temp;
            }
            return anyArray;
        }
        static int[] BubbleSortWorse(int[] anyArray) // Time complexity O(n^2) 
        {
            int n = anyArray.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (anyArray[j] > anyArray[j + 1])
                    {
                        int temp = anyArray[j - 1];
                        anyArray[j - 1] = anyArray[j];
                        anyArray[j] = temp;
                    }
                }
            }

            return anyArray; // Doesn't account for the list already potentially being sorted
        }

        static int[] BubbleSortBetter(int[] anyArray) // Time complexity O(n^2)
        {
            int n = anyArray.Length;

            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (anyArray[j] > anyArray[j + 1])
                    {
                        int temp = anyArray[j - 1];
                        anyArray[j - 1] = anyArray[j];
                        anyArray[j] = temp;
                    }
                }
                if (!swapped) // In case the array is already sorted then this stops the algorithm from checking
                {
                    break;
                }
            }

            return anyArray;
        }

        static int[] InsertionSort(int[] anyArray) // Time complexity O(n^2)
        {
            int n = anyArray.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    // Swap if the element at j - 1 position is greater than the element at j position
                    if (anyArray[j - 1] > anyArray[j])
                    {
                        int temp = anyArray[j - 1];
                        anyArray[j - 1] = anyArray[j];
                        anyArray[j] = temp;
                    }
                }
            }
            return anyArray; 
        }

        public static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                QuickSort(arr, left, pivot - 1);
                QuickSort(arr, pivot + 1, right);
            }
        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int temp1 = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = temp1;

            return i + 1;
        }

    }
}
