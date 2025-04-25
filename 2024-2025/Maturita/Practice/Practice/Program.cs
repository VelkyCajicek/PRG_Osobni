using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 11;
            int[] arr = new int[n];
            FillArray(arr, n);
            MergeSort(arr, 0, n - 1);
            DisplayArray(arr, n);
            Console.ReadKey();
        }
        static void DisplayArray(int[] arr, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(i < n - 1 ? $"{arr[i]}, " : $"{arr[i]}\n");
            }
        }
        static void FillArray(int[] arr, int n)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(0, 100);
            }
            DisplayArray(arr, n);
        }
        static void MergeSort(int[] arr, int left, int right)
        {
            if(left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
        }
        static void Merge(int[] arr, int left, int mid, int right)
        {
            int[] leftArray = new int[mid - left + 1];
            int[] rightArray = new int[right - mid];

            Array.Copy(arr, left, leftArray, 0, leftArray.Length);
            Array.Copy(arr, mid + 1, rightArray, 0, rightArray.Length);

            int i = 0; 
            int j = 0; 
            int k = left;

            while (i < leftArray.Length && j < rightArray.Length)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    arr[k++] = leftArray[i++];
                }
                else
                {
                    arr[k++] = rightArray[j++];
                }
            }

            // Copy any remaining elements
            while (i < leftArray.Length)
                arr[k++] = leftArray[i++];

            while (j < rightArray.Length)
                arr[k++] = rightArray[j++];
        }
    }
}
