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
            int n = 10;
            int[] arr = new int[n];
            FillArray(arr, n);
            Console.Write("Unsorted array: ");
            DisplayArray(arr, n);
            // Create clones of main array
            int[] arr1 = (int[])arr.Clone();
            int[] arr2 = (int[])arr.Clone();
            int[] arr3 = (int[])arr.Clone();
            int[] arr4 = (int[])arr.Clone();
            // O(n^2) algorithms
            BubbleSort(arr1, n);
            SelectionSort(arr2, n);
            InsertionSort(arr3, n);
            // Quicksort
            QuickSort(arr4, 0, n - 1);
            Console.Write("Quick sort: ");
            DisplayArray(arr4, n);

            Console.ReadKey();
        }
        static void FillArray(int[] arr, int n)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(0, 100);
            }
        }
        static void DisplayArray(int[] arr, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(i < n - 1 ? $"{arr[i]}, " : $"{arr[i]}\n");
            }
        }
        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        static void BubbleSort(int[] arr, int n)
        {
            Console.Write("Bubble sort: ");
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
            DisplayArray(arr, n);
        }
        static void SelectionSort(int[] arr, int n)
        {
            Console.Write("Selection sort: ");
            for (int i = 0; i < n; i++)
            {
                int iMin = i;
                for (int j = i; j < n; j++)
                {
                    if (arr[j] < arr[iMin]) iMin = j;
                }
                Swap(ref arr[i], ref arr[iMin]);
            }
            DisplayArray(arr, n);
        }
        static void InsertionSort(int[] arr, int n)
        {
            Console.Write("Selection sort: ");
            for (int i = 0; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
            DisplayArray(arr, n);
        }
        static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                QuickSort(arr, left, pivot - 1);
                QuickSort(arr, pivot + 1, right);
            }
        }
        static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (pivot > arr[j])
                {
                    i++;
                    Swap(ref arr[i], ref arr[j]);
                }
            }
            Swap(ref arr[i + 1], ref arr[right]);

            return i + 1;
        }
    }
}
