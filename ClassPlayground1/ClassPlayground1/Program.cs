using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClassPlayground1
{
    internal class Program
    {
        class Human
        {
            public int age;
            public int height;
            public int weight;
            public string name;
            public Human partner;
            public Human(int age, int height, int weight, string name) 
            {
                this.age = age;
                this.height = height;
                this.weight = weight;
                this.name = name;
            }

            public Human() 
            { 
                
            }

            public void Speak()
            {
                Console.WriteLine($"My name is {name}, i am {age} years old, i am {height} cms tall");
            }
            public float BodyMassIndex()
            {
                float heightForBMI = height / (float)weight;
                float bmi = weight / (heightForBMI * heightForBMI);
                return bmi;
            }
        }
        class BankAccount
        {
            public int bal;
            public int num;
            public BankAccount(int bal, int num) 
            {
                this.bal = bal;
                this.num = num;
            }
            public static void Transfer()
            {
                Dictionary<string, int> Currency = new Dictionary<string, int>() { { "Euro", 1 }, { "Kc", 25} };
            }
        }
        
        static void Main(string[] args)
        {
            

            Console.ReadKey();
        }
    }
}
