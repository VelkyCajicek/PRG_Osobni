using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ClassPlayground
{
    class Rectangle
    {
        public int width;
        public int height;

        public int CalculateArea()
        {
            return this.width * this.height;
        }
        public string CalculateAspectRatio()
        {
            if (this.width / this.height > 1) return "Rectangle is thick";
            else return "Rectangle is tall";
        }
        public bool ContainsPoint(int x1, int y1)
        {
            if (x1 <= this.width && y1 <= this.height) return true;
            else return false;
        }
        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
    class BankAccount
    {
        public int accountNumber;
        public string holderName;
        public string currency;
        public float balance;

        public int GenerateAccountNumber()
        {
            Random rnd = new Random();
            return rnd.Next(0, 1000000);
        }
        public void Deposit(int amount)
        {
            this.balance += amount;
        }
        public void Withdraw(int amount)
        {
            this.balance -= amount;
        }
        public void Transfer(int amount, BankAccount recipient)
        {
            recipient.balance += amount;
            this.balance -= amount;
        }
        public BankAccount(string holderName, string currency)
        {
            this.accountNumber = GenerateAccountNumber();
            this.holderName = holderName;
            this.currency = currency;
            this.balance = 0;
        }
    }
    
    class Student
    {
        public int year;
        public int id;
        public Dictionary<string, List<int>> subjects;
        public string name;

        public void AddSubject(string subject)
        {
            this.subjects.Add(subject, new List<int>(0));
        }
        public void AddGrade(int grade, string subject)
        {
            this.subjects[subject].Add(grade);
        }
        public double CalculateSubjectGrade(string subject)
        {
            return this.subjects[subject].Average();
        }
        public double CalculateTotalGrade()
        {
            double total = 1f;
            return total;
        }
        public int GenerateID()
        {
            Random rnd = new Random();
            return rnd.Next(1, 100000);
        }


        public Student(int year, string name)
        {
            this.year = year;
            this.id = GenerateID();
            this.subjects = new Dictionary<string, List<int>>(0);
            this.name = name;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle1 = new Rectangle(3, 3);
            int temp = rectangle1.CalculateArea();
            Console.WriteLine(temp);
            
            Console.ReadKey();
        }
    }
}
