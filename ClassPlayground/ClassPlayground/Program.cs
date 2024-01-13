using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RectangleCode();
            BankCode();
            Console.ReadKey();
        }
        static void RectangleCode()
        {
            Rectangle rect = new Rectangle();
            rect.height = 10;
            rect.width = 10;
            Console.WriteLine($"Obsah je {rect.CalculateArea()}");

            double ratio = rect.CalculateAspectRatio();
            if (ratio > 1) { Console.WriteLine("Obdelnik je siroky"); }
            if (ratio < 1) { Console.WriteLine("Obdelnik je vysoky"); }
            if (ratio == 1) { Console.WriteLine("Je to ctverec"); }

            int inputX = 5; int inputY = 5;
            Console.WriteLine(rect.ContainsPoints(inputX, inputY));
        }
        static void BankCode()
        {
            BankAccount Account1 = new BankAccount();
            Account1.balance = 100;
            Account1.accountNumber = 1;
            Account1.holderName = "Bob";
            Account1.currency = "Kc";
            
            BankAccount Account2 = new BankAccount();
            Account2.balance = 1000;
            Account2.accountNumber = 2;
            Account2.holderName = "Tom";
            Account2.currency = "Kc";

            int amount = 100;

            Console.WriteLine($"{amount} has been deposited and the balance now stands at {Account1.Deposit(amount)}");
            Console.WriteLine($"{amount} has been withdrawn and the balance now stands at {Account2.Withdrawel(amount)}");
            Transfer(Account1 , Account2, amount);
            Console.WriteLine($"{amount} has been transfered between {Account1.accountNumber} and {Account2.accountNumber}. The balances now stand at {Account1.balance} and {Account2.balance}");
        }
        static void Transfer(BankAccount account1, BankAccount account2, int amount)
        {
            account1.balance -= amount;
            account2.balance += amount;
        }
    }
}
class Rectangle
{
    public int height;
    public int width;

    public Rectangle(int height, int width)
    {
        this.width = width;
        this.height = height;
    }
    public Rectangle()
    {
    }
    public int CalculateArea()
    {
        int area = height * width;
        return area;
    }
    public double CalculateAspectRatio()
    {
        return width / height;
    }
    public bool ContainsPoints(int inputX, int inputY)
    {
        if((0 < inputX && inputX < height) && (0 < inputY && inputY < width)) 
        {
            return true;
        }
        else { return false; }
    }
}

class BankAccount
{
    public int accountNumber;
    public string holderName;
    public string currency;
    public int balance;
    public BankAccount(int accountNumber, string holderName, string currency, int balance)
    {
        this.accountNumber = accountNumber;
        this.holderName = holderName;
        this.currency = currency;
        this.balance = balance;
    }
    public BankAccount()
    {
    }
    public int Deposit(int amount)
    {
        return balance + amount;
    }
    public int Withdrawel(int amount)
    {
        return balance - amount;
    }
}
class Student
{
    public int year;
    public int id;
    public Dictionary<string, string> Subjects;
    public string name;
}