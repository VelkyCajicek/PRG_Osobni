using System;
using System.Collections.Generic;
using System.Data;
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
            Explanation(); //This is just so the user knows what the program can do
            List<string> variableName = new List<string>();
            List<string> variableValue = new List<string>();
            List<string> realOperators = new List<string>();
            List<string> Numbs = new List<string>();
            bool test = true;
            while (test == true)
            {
                string input = ""; bool check1 = false; string inputFinal = ""; realOperators.Clear(); Numbs.Clear(); string inputMod1 = "";
                while (check1 == false)
                {
                    int counter = 0;
                    while (true) // This is for when a user defines a variable
                    {
                        input = Console.ReadLine().Replace(" ", "");
                        if (Regex.IsMatch(input, "[A-Za-z]=[0-9]+") == true)
                        {
                            if (variableName.Contains(input[0].ToString()) == true) //When a variable is given a different value
                            {
                                int index = variableName.IndexOf(input[0].ToString());
                                string previousString = variableValue[index];
                                variableValue[index] = input.Remove(0, 2);
                                Console.WriteLine("'{0}' now has the value {1}, (previous value {2})", variableName[index], input.Remove(0, 2), previousString);
                            }
                            else //When a new variable is added
                            {
                                variableName.Add(input[0].ToString());
                                variableValue.Add(input.Remove(0, 2));
                                Console.WriteLine("'{0}' now has the value {1}", variableName[counter], variableValue[counter]);
                                counter++;
                            }
                        }
                        else { break; }
                    }
                    while (check1 == false) //Getting rid of trigoniometric functions so input can be checked
                    {
                        int counterBracket = 0; bool breakCheck = false;
                        input = replacingVariables(variableName, input, variableValue);
                        inputMod1 = input;
                        if (input.Contains("pi")) { input = input.Replace("pi", Math.PI.ToString()); }
                        if (input.Contains("sin(")) { input = input.Replace("sin", ""); }
                        if (input.Contains("cos(")) { input = input.Replace("cos", ""); }
                        if (input.Contains("tg(")) { input = input.Replace("tg", ""); }
                        for (int i = 0; i < input.Length; i++) // Simple check to make sure input hasnt got any undefined variables
                        {
                            if (Regex.IsMatch(input[i].ToString(), @"^[a-zA-Z]+$") == true)
                            {
                                if (variableName.Contains(input[i].ToString()) == false) { Console.WriteLine("Syntax error, '{0}' not defined", input[i]); breakCheck = true; }
                                else { counterBracket++; }
                            }
                            else { counterBracket++; }
                            if (counterBracket == input.Length) { check1 = true; }
                        }
                        if (breakCheck == true) { break; }
                    }
                }
                input = inputMod1;
                for (int i = 0; i < input.Length; i++) //Adding the operators in the input to appropriate list
                {
                    if (input[i] == '+') { realOperators.Add("+"); }
                    if (input[i] == '-') { realOperators.Add("-"); }
                    if (input[i] == '*') { realOperators.Add("*"); }
                    if (input[i] == '/') { realOperators.Add("/"); }
                }
                String[] seperator = { "+", "-", "*", "/" }; // Spliting the string to find all numbers/parentheses
                String[] strlist = input.Split(seperator, 50, StringSplitOptions.RemoveEmptyEntries);
                foreach (String s in strlist) { Numbs.Add(s); }
                for (int i = 0; i < Numbs.Count; i++)
                {
                    if (Numbs[i].Contains("^")) // Dealing with exponentials
                    {
                        string[] parts = Numbs[i].Split('^');
                        string x = parts[0]; string n = parts[1];
                        float xFloat = float.Parse(x); float nFloat = float.Parse(n);
                        float r = (float)Math.Pow(xFloat, nFloat);
                        Numbs[i] = Numbs[i].Replace(Numbs[i], r.ToString());
                    }//Dealing with trigoniometric functions
                    if (Numbs[i].Contains("sin(")) { Sin(Numbs, i); }
                    if (Numbs[i].Contains("cos(")) { Cos(Numbs, i); }
                    if (Numbs[i].Contains("tg(")) { Tg(Numbs, i); }
                }
                for (int i = 0; i < Numbs.Count; i++) //Reconstructing string so it can go into datatable without any error
                {
                    try { inputFinal += Numbs[i]; inputFinal += realOperators[i]; }
                    catch (ArgumentOutOfRangeException) { break; }
                }
                string inputMod = inputFinal;
                try
                {
                    double result = Calculation(inputMod);
                    Console.WriteLine(result);
                } //In case of multiple operators after each other (e.g. 4*-4) or missing parentheses ( 4+(4+4 )
                catch (SyntaxErrorException) { Console.WriteLine("Syntax error, Invalid expression"); }
            }
            Console.ReadKey();
        }
        static void Explanation()
        {
            Console.WriteLine("This calculator can calculate:");
            Console.WriteLine("- Basic operations (+,-,*,/)");
            Console.WriteLine("- Brackets and exponentials (to get square root enter 0.5 to exponent)");
            Console.WriteLine("- Trigonimetric functions (sin, cos, tg)");
            Console.WriteLine("- User defined variables (e.g. x = 4)\n");
        }
        static string replacingVariables(List<string> variableName, string input, List<string> variableValue) //Replacing the variables with their corresponding value
        {
            for (int i = 0; i < variableName.Count; i++)
            {
                if (input.Contains(variableName[i]))
                {
                    input = input.Replace(variableName[i], variableValue[i]);
                }
            }
            return (input);
        }
        static double Calculation(string inputMod) // Only lines of code that were inspired (written) by ChatGPT, uses the columns in the datatable to calculate simple mathematical operations
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("input", typeof(string), inputMod);
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);
            return (double.Parse((string)row["input"]));
        }
        static void Sin(List<string> Numbs, int i) // The following 3 methods just calculate the 3 implemented trigoniometric functions
        {
            Numbs[i] = Numbs[i].Replace("sin", "");
            string inputMod = Numbs[i];
            string result = Math.Sin(Calculation(inputMod) * (Math.PI / 180)).ToString();
            Numbs[i] = Numbs[i].Replace(inputMod, result);
        }
        static void Cos(List<string> Numbs, int i)
        {
            Numbs[i] = Numbs[i].Replace("cos", "");
            string inputMod = Numbs[i];
            string result = Math.Cos(Calculation(inputMod) * (Math.PI / 180)).ToString();
            Numbs[i] = Numbs[i].Replace(inputMod, result);
        }
        static void Tg(List<string> Numbs, int i)
        {
            Numbs[i] = Numbs[i].Replace("tg", "");
            string inputMod = Numbs[i];
            string result = Math.Tan(Calculation(inputMod) * (Math.PI / 180)).ToString();
            Numbs[i] = Numbs[i].Replace(inputMod, result);
        }
    }
}
