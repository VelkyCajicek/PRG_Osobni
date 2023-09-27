using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Made by Jan Borecky for PRG seminar at Gymnazium Voderadska, year 2023-2024.
 * Extended by students.
 */

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Defining shit
            List<string> realOperators = new List<string>();
            List<string> Numbs = new List<string>();
            List<float> NumbFloats = new List<float>();
            float partialResult = 0; int firstStringPosition = 0; int secondStringPosition = 0;

            string input = Console.ReadLine();

            //Making sure there is no BS
            if (input.Last() == '=') { input = input.Remove(input.Length - 1); }

            //Zavorky

            string inputModBracket = input;
            while (inputModBracket.Contains('(') == true)
            {
                for (int i = 0; i < inputModBracket.Length; i++)
                {
                    if (inputModBracket[i] == '(') { firstStringPosition = i; }
                    if (inputModBracket[i] == ')') { secondStringPosition = i; }
                }
                string Bracket = inputModBracket.Substring(firstStringPosition, secondStringPosition - firstStringPosition + 1);
                string BracketMod = Bracket.Replace('+', 'p').Replace('-', 'm').Replace('*', 't').Replace('/', 'd');
                input = input.Replace(Bracket, BracketMod);
                inputModBracket = inputModBracket.Replace(Bracket, "");
            }
            //Console.WriteLine(inputMod);
            
            //Finding the numbers in input
            String[] seperator = { "+", "-", "*", "/" };
            int count = 50;
            String[] strlist = input.Split(seperator, count, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                Numbs.Add(s);
            }
            Numbs.ForEach(Console.WriteLine);
            //Finding the operations in input 
            /*
            for (int i = 0; i < input.Length; i++)
            {
                char Position = input[i];
                if (Position == '+') { realOperators.Add("+"); }
                if (Position == '-') { realOperators.Add("-"); }
                if (Position == '*') { realOperators.Add("*"); }
                if (Position == '/') { realOperators.Add("/"); }
            }
            //Calculating multiplication/division, Line 56 first index + 1 to not change index of first position
            NumbFloats = Numbs.Select(float.Parse).ToList();
            while (realOperators.Count != 0)
            {
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "*") { partialResult = NumbFloats[i] * NumbFloats[i + 1]; }
                    if (realOperators[i] == "/") { partialResult = NumbFloats[i] / NumbFloats[i + 1]; }
                    if (realOperators[i] == "+") { partialResult = NumbFloats[i] + NumbFloats[i + 1]; }
                    if (realOperators[i] == "-") { partialResult = NumbFloats[i] - NumbFloats[i + 1]; }
                    realOperators.Remove(realOperators[i]);
                    NumbFloats.Remove(NumbFloats[i + 1]);
                    NumbFloats.Remove(NumbFloats[i]);
                    NumbFloats.Insert(i, partialResult);
                }
            }
            //Console.WriteLine(NumbFloats.Count);
            NumbFloats.ForEach(Console.WriteLine);
            //realOperators.ForEach(Console.WriteLine);
            */
            Console.ReadKey();
            
        }
    }
}
