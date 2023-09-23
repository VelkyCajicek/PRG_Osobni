using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamen_Nuzky_Papir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string confirm = "y";
            List<string> list = new List<string>();
            list.Add("kamen"); list.Add("papir"); list.Add("nuzky");
            List <string> moznosti = new List<string>();
            moznosti.Add("hra skoncila remizou"); moznosti.Add("jsi vyhral"); moznosti.Add("jsi prohral");
            string vysledek = "";
            double skore_hrace = 0;
            double skore_pocitace = 0;
            Console.WriteLine("Tohle je kamen/nuzky/papir, vyber jsi jednu ze tri: ");
            while (true)
            {
                bool yn_check = false; string input = "";
                Random rnd = new Random();
                int pocitac_choice = rnd.Next(0, 2);
                string pocitac_vyber = list[pocitac_choice];
                input = Console.ReadLine();
                while (list.Contains(input) == false)
                {
                    Console.WriteLine("To neni platna moznost");
                    input = Console.ReadLine();
                }
                if (list.Contains(input) == true)
                {
                    if (pocitac_vyber == input) { vysledek = moznosti[0]; skore_hrace += 0.5; skore_pocitace += 0.5; }
                    switch (input)
                    {
                        case "kamen":
                            if (pocitac_vyber == "papir") { vysledek = moznosti[2]; skore_pocitace += 1; }
                            if (pocitac_vyber == "nuzky") { vysledek = moznosti[1]; skore_hrace += 1; }
                            break;
                        case "papir":
                            if (pocitac_vyber == "nuzky") { vysledek = moznosti[2]; skore_pocitace += 1; }
                            if (pocitac_vyber == "kamen") { vysledek = moznosti[1]; skore_hrace += 1; }
                            break;
                        case "nuzky":
                            if (pocitac_vyber == "kamen") { vysledek = moznosti[2]; skore_pocitace += 1; }
                            if (pocitac_vyber == "papir") { vysledek = moznosti[1]; skore_hrace += 1; }
                            break;
                    }
                    Console.WriteLine("Pocitac vybral {0}, takze {1} a skore je momentalne {2}:{3} (Ty:Pocitac)\nChces hrat znovu? (y/n)", pocitac_vyber, vysledek, skore_hrace, skore_pocitace);
                    //confirm = Console.ReadLine();
                    //if (confirm == "y"){ Console.WriteLine("Hraje se znovu, opet si vyber: "); } else { Environment.Exit(0); }
                    List<string> yn = new List<string>();
                    yn.Add("y"); yn.Add("n");
                    while (yn_check == false)
                    {
                        string input_yn = Console.ReadLine();
                        if (yn.Contains(input_yn) == true)
                        {
                            if (input_yn == yn[0]) { yn_check = true; }
                            if (input_yn == yn[1]) { yn_check = false; Environment.Exit(0); }
                        }
                        else { Console.WriteLine("Wrong input"); }
                    }
                }
                Console.WriteLine("Vyber si opet jednu ze 3 moznosti: ");
                Console.ReadKey(); 
            }
        }
    }
}
