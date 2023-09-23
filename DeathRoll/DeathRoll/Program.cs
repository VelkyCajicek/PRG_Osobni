using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DeathRoll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int gold = 1000; bool result = false; string text; int change; int zmena_goldu = gold; bool state = true; int int_sazka = 5000000;
            
            while (state == true) 
            {
                bool yn_check = false; 
                Console.WriteLine("Kolik chces vsadit? Pocet goldu: {0}", zmena_goldu);
                string sazka = Console.ReadLine();
                int_sazka = int.Parse(sazka);
                if (int.TryParse(sazka, out change) == true)
                {
                    int_sazka = int.Parse(sazka);
                    if (int_sazka > zmena_goldu) { Console.WriteLine("Sazka prevysuje stavajici pocet goldu, zadejte odpovídající částku: "); }
                }
                while (int_sazka > zmena_goldu) 
                {
                    sazka = Console.ReadLine();
                    if(int.TryParse(sazka, out change) == true)
                    {
                        int_sazka = int.Parse(sazka);
                        if(int_sazka > zmena_goldu) { Console.WriteLine("Sazka prevysuje stavajici pocet goldu, zadejte odpovídající částku: "); }
                    }
                    else { Console.WriteLine("Zadej cislo"); }
                }
                int new_value = int_sazka;
                Console.WriteLine("Hra zacina se sazkou {0}", int_sazka);
                while (new_value != 1)
                {
                    Random rnd = new Random();
                    int roll1 = rnd.Next(1, new_value);
                    Console.WriteLine("Padla ti {0}, [1-{1}]", roll1, new_value);
                    new_value = roll1;
                    if (new_value == 1) { result = true; break; }
                    int roll2 = rnd.Next(1, new_value);
                    Console.WriteLine("Pocitaci padla {0}, [1-{1}]", roll2, new_value);
                    new_value = roll2;
                    if (new_value == 1) { result = false; break; }
                }
                if (result == false) { text = "Vyhral jsi"; change = 1; }
                else { text = "Prohral jsi"; change = -1; }
                zmena_goldu = zmena_goldu + int_sazka * change;
                Console.WriteLine("{0} {1} goldu, takze ted mas {2}\nChces hrat znovu? (y/n)", text, int_sazka, zmena_goldu);
                List<string> yn = new List<string>();
                yn.Add("y"); yn.Add("n");
                while (yn_check == false)
                {
                    string input = Console.ReadLine();
                    if (yn.Contains(input) == true)
                    {
                        if (input == yn[0]) { yn_check = true; }
                        if (input == yn[1]) { yn_check = false; Environment.Exit(0); }
                    }
                    else { Console.WriteLine("Wrong input"); }
                }
                Console.ReadKey();
            } 
        }
    }
}
