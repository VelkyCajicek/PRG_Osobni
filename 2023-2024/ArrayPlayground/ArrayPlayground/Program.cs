using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 3, 4, 5 };

            //foreach (int i in nums) { Console.WriteLine(i); }

            int sum = 0;
            foreach(int i in nums)
            {
                sum += i;
            }

            int average = 0;
            foreach(int i in nums)
            {
                average += i;
            }
            average = average / nums.Length;

            int max = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > max) {  max = nums[i]; }
            }

            Random rnd = new Random();
            int min = rnd.Next(nums.Length);
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < min) { min = nums[i]; }
            }
            
            int input = Convert.ToInt32(Console.ReadLine());
            int index = Array.IndexOf(nums, input);
            //Console.WriteLine(index);

            Random rndTodoEight = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(rndTodoEight.Next(9));
            }
            int[] TodoEight = list.ToArray();
            //foreach (int i in TodoEight) { Console.WriteLine(i); }
            //Console.WriteLine(TodoEight.Length);

            int[] counts = new int[10];
            List<int> ints = TodoEight.ToList();
            var grp = ints.GroupBy(i => i);
            foreach (var g in grp)
            {
                Console.WriteLine("{0} {1}", g.Key, g.Count());
            }

            List<int> Nums2 = nums.ToList();
            Nums2.Reverse();
            int [] nums2 = Nums2.ToArray();
            //foreach(int  i in nums2) { Console.WriteLine(i); }
            Console.ReadKey();
        }
    }
}
