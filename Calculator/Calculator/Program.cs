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
            float result = 0;
            float partialResult = 0;


            string input = Console.ReadLine();

            //Making sure there is no BS
            string inputMod = input.Replace('+', 'P').Replace('-', 'M').Replace('*', 'T').Replace('/', 'D');
            if (input.Last() == '=') { input = input.Remove(input.Length - 1); }

            //Finding the numbers in input
            String[] seperator = { "M", "P", "T", "D" };
            int count = 50;
            String[] strlist = inputMod.Split(seperator, count, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                Numbs.Add(s);
            }

            //Finding the operations in input
            for (int i = 0; i < inputMod.Length; i++)
            {
                char Position = inputMod[i];
                if (Position == 'P') { realOperators.Add("+"); }
                if (Position == 'M') { realOperators.Add("-"); }
                if (Position == 'T') { realOperators.Add("*"); }
                if (Position == 'D') { realOperators.Add("/"); }
            }
            //Calculating multiplication/division, Line 56 first index + 1 to not change index of first position
            NumbFloats = Numbs.Select(float.Parse).ToList();
            while (realOperators.Count != 0)
            {
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "*")
                    {
                        partialResult = NumbFloats[i] * NumbFloats[i + 1];
                        realOperators.Remove(realOperators[i]);
                        NumbFloats.Remove(NumbFloats[i + 1]);
                        NumbFloats.Remove(NumbFloats[i]);
                        NumbFloats.Insert(i, partialResult);
                        //result += partialResult; Mam pocit ze tohle bude potreba
                    }
                }
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "/")
                    {
                        partialResult = NumbFloats[i] / NumbFloats[i + 1];
                        realOperators.Remove(realOperators[i]);
                        NumbFloats.Remove(NumbFloats[i + 1]);
                        NumbFloats.Remove(NumbFloats[i]);
                        NumbFloats.Insert(i, partialResult);
                    }
                }
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "+")
                    {

                        partialResult = NumbFloats[i] + NumbFloats[i + 1];
                        realOperators.Remove(realOperators[i]);
                        NumbFloats.Remove(NumbFloats[i + 1]);
                        NumbFloats.Remove(NumbFloats[i]);
                        NumbFloats.Insert(i, partialResult);
                    }
                }
                for (int i = 0; i < realOperators.Count; i++)
                {
                    if (realOperators[i] == "-")
                    {
                        partialResult = NumbFloats[i] - NumbFloats[i + 1];
                        realOperators.Remove(realOperators[i]);
                        NumbFloats.Remove(NumbFloats[i + 1]);
                        NumbFloats.Remove(NumbFloats[i]);
                        NumbFloats.Insert(i, partialResult);
                    }
                }
            }
            //Console.WriteLine(NumbFloats.Count);
            NumbFloats.ForEach(Console.WriteLine);
            //realOperators.ForEach(Console.WriteLine);
            Console.ReadKey();

        }
    }
}
