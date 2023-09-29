using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            List<string> jmenoPromenne = new List<string>();
            List<string> hodnotaPromenne = new List<string>();
            float partialResult = 0; int firstStringPosition = 0; int secondStringPosition = 0; bool bracketCheck = false; string pattern = "[A-Za-z]=[0-9]+";
            int counter = 0; string input; Regex promenna = new Regex(pattern);

            //Kontrola jestli chce nastavit promennou

            while (true)
            {
                input = Console.ReadLine();
                input = input.Replace(" ", "");
                if (promenna.IsMatch(input))
                {
                    jmenoPromenne.Add(input[0].ToString());
                    input = input.Remove(0, 2);
                    string cislo = input;
                    hodnotaPromenne.Add(input);
                    Console.WriteLine("Promenna {0} ma ted hodnotu {1}", jmenoPromenne[counter], hodnotaPromenne[counter]);
                    counter++;
                }
                else { break; }
            }
            input = input.Replace(" ", "");
            if (input.Contains("pi") == true) { input = input.Replace("pi", Math.PI.ToString()); }
            for (int i = 0; i < jmenoPromenne.Count; i++)
            {
                if (input.Contains(jmenoPromenne[i]))
                {
                    input = input.Replace(jmenoPromenne[i], hodnotaPromenne[i]);
                }
            }

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

            if (bracketCheck == true)
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
