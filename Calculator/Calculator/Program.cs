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
            float partialResult = 0; int firstStringPosition = 0; int secondStringPosition = 0; bool bracketCheck = false;

            string input = Console.ReadLine();

            //Making sure there is no BS
            if (input.Last() == '=') { input = input.Remove(input.Length - 1); }
            //Zavorky
            string inputModBracket = input;
            if (inputModBracket.Contains('(') == true) { bracketCheck = true; }
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
            //Finding the numbers in input
            String[] seperator = { "+", "-", "*", "/" };
            int count = 50;
            String[] strlist = input.Split(seperator, count, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                Numbs.Add(s);
            }
            //Getting rid of brackets
            if(bracketCheck == true)
            {
                for (int p = 0; p < Numbs.Count; p++)
                {
                    partialResult = 0;
                    if (Numbs[p].Contains("("))
                    {
                        List<string> realOperatorsInBrackets = new List<string>();
                        List<string> NumbsInBrackets = new List<string>();
                        List<float> NumbFloatsInBrackets = new List<float>();
                        
                        Numbs[p] = Numbs[p].Replace("(", "").Replace(")", "");
                        String[] seperatorInBrackets = { "p", "m", "t", "d" };
                        count = 50;
                        String[] strlistBracket = Numbs[p].Split(seperatorInBrackets, count, StringSplitOptions.RemoveEmptyEntries);
                        foreach (String t in strlistBracket)
                        {
                            NumbsInBrackets.Add(t);
                        }
                        for (int j = 0; j < Numbs[p].Length; j++)
                        {
                            char Position = Numbs[p][j];
                            if (Position == 'p') { realOperatorsInBrackets.Add("+"); }
                            if (Position == 'm') { realOperatorsInBrackets.Add("-"); }
                            if (Position == 't') { realOperatorsInBrackets.Add("*"); }
                            if (Position == 'd') { realOperatorsInBrackets.Add("/"); }
                        }
                        NumbFloatsInBrackets = NumbsInBrackets.Select(float.Parse).ToList();
                        while (realOperatorsInBrackets.Count != 0)
                        {
                            for (int i = 0; i < realOperatorsInBrackets.Count; i++)
                            {
                                if (realOperatorsInBrackets[i] == "*") { partialResult = NumbFloatsInBrackets[i] * NumbFloatsInBrackets[i + 1]; }
                                if (realOperatorsInBrackets[i] == "/") { partialResult = NumbFloatsInBrackets[i] / NumbFloatsInBrackets[i + 1]; }
                                if (realOperatorsInBrackets[i] == "+") { partialResult = NumbFloatsInBrackets[i] + NumbFloatsInBrackets[i + 1]; }
                                if (realOperatorsInBrackets[i] == "-") { partialResult = NumbFloatsInBrackets[i] - NumbFloatsInBrackets[i + 1]; }
                                realOperatorsInBrackets.Remove(realOperatorsInBrackets[i]);
                                NumbFloatsInBrackets.Remove(NumbFloatsInBrackets[i + 1]);
                                NumbFloatsInBrackets.Remove(NumbFloatsInBrackets[i]);
                                NumbFloatsInBrackets.Insert(i, partialResult);
                            }
                        }
                        Numbs[p] = Numbs[p].Replace(Numbs[p], NumbFloatsInBrackets[0].ToString());
                    }
                }
            }
            //Finding the operations in input 
            for (int i = 0; i < input.Length; i++)
            {
                char Position = input[i];
                if (Position == '+') { realOperators.Add("+"); }
                if (Position == '-') { realOperators.Add("-"); }
                if (Position == '*') { realOperators.Add("*"); }
                if (Position == '/') { realOperators.Add("/"); }
            }
 
            //Calculating, Line 128 first index + 1 to not change index of first position
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
            NumbFloats.ForEach(Console.WriteLine);
            Console.ReadKey();

        }
    }
}
