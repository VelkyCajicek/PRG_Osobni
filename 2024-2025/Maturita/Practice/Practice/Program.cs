using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = @"\test.txt";
            string path = desktopPath + fileName;
            // Option 1
            CreateTextFile(path);
            ReadTextFile(path);
            File.Delete(path);
            // Option 2
            File.WriteAllText(path, "Works");
            string text = File.ReadAllText(path);
            Console.WriteLine(text);
            File.Delete(path);

            Console.ReadKey();
        }
        static void CreateTextFile(string path)
        {
            if (!File.Exists(path))
            {
                using(StreamWriter sw = File.CreateText(path))
                {
                    sw.Write("Works");
                }
            }
        }
        static void ReadTextFile(string path)
        {
            if (File.Exists(path))
            {
                using(StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
        }
    }
}
