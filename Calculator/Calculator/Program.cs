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
        static void Calculation(int i, float partialResult, List<string> realOperators, List<float> NumbFloats)
        {
            realOperators.Remove(realOperators[i]);
            NumbFloats.Remove(NumbFloats[i + 1]);
            NumbFloats.Remove(NumbFloats[i]);
            NumbFloats.Insert(i, partialResult);
        }
        static void CalculationBracket(int i, float partialResult, List<string> realOperatorsInBrackets, List<float> NumbFloatsInBrackets)
        {
            realOperatorsInBrackets.Remove(realOperatorsInBrackets[i]);
            NumbFloatsInBrackets.Remove(NumbFloatsInBrackets[i + 1]);
            NumbFloatsInBrackets.Remove(NumbFloatsInBrackets[i]);
            NumbFloatsInBrackets.Insert(i, partialResult);
        }
        static void Exponentiation(List<string> Numbs)
        {
            for (int i = 0; i < Numbs.Count; i++)
            {
                if (Numbs[i].Contains("^"))
                {
                    string correctString = Numbs[i];
                    string x = correctString.Split('^')[0];
                    string n = correctString.Replace(x, "").Replace("^", "");
                    float xFloat = float.Parse(x); float nFloat = float.Parse(n);
                    float r = (float)Math.Pow(xFloat, nFloat);
                    Numbs[i] = Numbs[i].Replace(Numbs[i], r.ToString());
                }
                else { }
            }
        }
        static void Result(List<float> NumbFloats, List<string> realOperators, string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '+') { realOperators.Add("+"); }
                if (input[i] == '-') { realOperators.Add("-"); }
                if (input[i] == '*') { realOperators.Add("*"); }
                if (input[i] == '/') { realOperators.Add("/"); }
            }
            float partialResult = 0;
            while (realOperators.Count != 0)
            {
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "*")
                    {
                        partialResult = NumbFloats[i] * NumbFloats[i + 1];
                        Calculation(i, partialResult, realOperators, NumbFloats);
                    }
                }
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "/")
                    {
                        partialResult = NumbFloats[i] / NumbFloats[i + 1];
                        Calculation(i, partialResult, realOperators, NumbFloats);
                    }
                }
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "+") { partialResult = NumbFloats[i] + NumbFloats[i + 1]; }
                    if (realOperators[i] == "-") { partialResult = NumbFloats[i] - NumbFloats[i + 1]; }
                    Calculation(i, partialResult, realOperators, NumbFloats);
                }
            }
            NumbFloats.ForEach(Console.WriteLine);
        }
        static void BracketResult(List<string> Numbs)
        {
            for (int p = 0; p < Numbs.Count; p++)
            {
                float partialResult = 0;
                if (Numbs[p].Contains("("))
                {
                    List<string> realOperatorsInBrackets = new List<string>();
                    List<string> NumbsInBrackets = new List<string>();
                    List<float> NumbFloatsInBrackets = new List<float>();

                    Numbs[p] = Numbs[p].Replace("(", "").Replace(")", "");
                    String[] seperatorInBrackets = { "p", "m", "t", "d" };
                    int count = 50;
                    String[] strlistBracket = Numbs[p].Split(seperatorInBrackets, count, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String t in strlistBracket)
                    {
                        NumbsInBrackets.Add(t);
                    }
                    for (int j = 0; j < Numbs[p].Length; j++)
                    {
                        if (Numbs[p][j] == 'p') { realOperatorsInBrackets.Add("+"); }
                        if (Numbs[p][j] == 'm') { realOperatorsInBrackets.Add("-"); }
                        if (Numbs[p][j] == 't') { realOperatorsInBrackets.Add("*"); }
                        if (Numbs[p][j] == 'd') { realOperatorsInBrackets.Add("/"); }
                    }
                    NumbFloatsInBrackets = NumbsInBrackets.Select(float.Parse).ToList();
                    while (realOperatorsInBrackets.Count != 0)
                    {
                        for (int i = 0; i < realOperatorsInBrackets.Count; i++)
                        {
                            if (realOperatorsInBrackets[i] == "*")
                            {
                                partialResult = NumbFloatsInBrackets[i] * NumbFloatsInBrackets[i + 1];
                                CalculationBracket(i, partialResult, realOperatorsInBrackets, NumbFloatsInBrackets);
                            }
                        }
                        for (int i = 0; i < realOperatorsInBrackets.Count; i++)
                        {
                            if (realOperatorsInBrackets[i] == "/")
                            {
                                partialResult = NumbFloatsInBrackets[i] / NumbFloatsInBrackets[i + 1];
                                CalculationBracket(i, partialResult, realOperatorsInBrackets, NumbFloatsInBrackets);
                            }
                        }
                        for (int i = 0; i < realOperatorsInBrackets.Count; i++)
                        {
                            if (realOperatorsInBrackets[i] == "+") { partialResult = NumbFloatsInBrackets[i] + NumbFloatsInBrackets[i + 1]; }
                            if (realOperatorsInBrackets[i] == "-") { partialResult = NumbFloatsInBrackets[i] - NumbFloatsInBrackets[i + 1]; }
                            CalculationBracket(i, partialResult, realOperatorsInBrackets, NumbFloatsInBrackets);
                        }
                    }
                    Numbs[p] = Numbs[p].Replace(Numbs[p], NumbFloatsInBrackets[0].ToString());
                }
            }
        }
        static void Main(string[] args)
        {
            bool test = true;
            List<string> realOperators = new List<string>();
            List<string> Numbs = new List<string>();
            List<float> NumbFloats = new List<float>();
            List<string> jmenoPromenne = new List<string>();
            List<string> hodnotaPromenne = new List<string>();
            string pattern = "[A-Za-z]=[0-9]+"; Regex promenna = new Regex(pattern); string input = "";
            while (test == true)
            {
                int firstStringPosition = 0; int secondStringPosition = 0; bool bracketCheck = false; bool check1 = false; bool check2 = false;
                bool exponentiationCheck = false; Numbs.Clear(); NumbFloats.Clear();
                while (check1 == false && check2 == false)
                {
                    int counter = 0;
                    while (true)
                    {
                        input = Console.ReadLine();
                        input = input.Replace(" ", "");
                        if (promenna.IsMatch(input) == true)
                        {
                            string jmenoPromenneHodnota = input[0].ToString();
                            string hodnotaPromenneHodnota = input.Remove(0, 2);
                            if (jmenoPromenne.Contains(input[0].ToString()) == true)
                            {
                                int index = jmenoPromenne.IndexOf(jmenoPromenneHodnota);
                                string previousString = hodnotaPromenne[index];
                                hodnotaPromenne[index] = hodnotaPromenneHodnota;
                                Console.WriteLine("'{0}' now has the value {1}, (previous value {2})", jmenoPromenne[index], hodnotaPromenneHodnota, previousString);
                            }
                            else
                            {
                                jmenoPromenne.Add(jmenoPromenneHodnota);
                                hodnotaPromenne.Add(hodnotaPromenneHodnota);
                                Console.WriteLine("'{0}' now has the value {1}", jmenoPromenne[counter], hodnotaPromenne[counter]);
                            }
                            counter++;
                        }
                        else { break; }
                    }
                    while (check1 == false && check2 == false)
                    {
                        check1 = false; check2 = false; bracketCheck = false; int counterBracket = 0; bool breakCheck = false;
                        if (input.Contains("pi") == true) { input = input.Replace("pi", Math.PI.ToString()); }
                        for (int i = 0; i < input.Length; i++)
                        {
                            if (Regex.IsMatch(input[i].ToString(), @"^[a-zA-Z]+$") == true)
                            {
                                if (jmenoPromenne.Contains(input[i].ToString()) == false) { Console.WriteLine("Syntax error, '{0}' not defined", input[i]); breakCheck = true; }
                                else { counterBracket++; }
                            }
                            else { counterBracket++; }
                            if (counterBracket == input.Length) { check1 = true; }
                        }
                        if (breakCheck == true) { break; }
                        if (input.Contains('(') == true)
                        {
                            bracketCheck = true;
                            if (input.Contains(")") == false) { Console.WriteLine("Syntax error, missing ')'"); break; }
                            else { check2 = true; }
                        }
                    }
                }
                if (input.Contains('^') == true) { exponentiationCheck = true; }
                for (int i = 0; i < jmenoPromenne.Count; i++) // Replacing the variables
                {
                    if (input.Contains(jmenoPromenne[i]))
                    {
                        input = input.Replace(jmenoPromenne[i], hodnotaPromenne[i]);
                    }
                }
                if (bracketCheck == true) //Modifying the string so it can be calculated 
                {
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
                }
                //Finding the numbers in input
                String[] seperator = { "+", "-", "*", "/" };
                int count = 50;
                String[] strlist = input.Split(seperator, count, StringSplitOptions.RemoveEmptyEntries);
                foreach (String s in strlist)
                {
                    Numbs.Add(s);
                }
                if (exponentiationCheck == true) // Calculating the exponents
                {
                    Exponentiation(Numbs);
                }
                if (bracketCheck == true) //Getting rid of brackets
                {
                    BracketResult(Numbs);
                }
                NumbFloats = Numbs.Select(float.Parse).ToList();
                Result(NumbFloats, realOperators, input);
            }
            Console.ReadKey();
        }
    }
}
