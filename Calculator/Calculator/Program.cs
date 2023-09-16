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
            /*
             * Pokud se budes chtit na neco zeptat a zrovna budu pomahat jinde, zkus se zeptat ChatGPT ;) - https://chat.openai.com/
             * 
             * ZADANI
             * Vytvor program ktery bude fungovat jako kalkulacka. Kroky programu budou nasledujici:
             * 1) Nacte vstup pro prvni cislo od uzivatele (vyuzijte metodu Console.ReadLine() - https://learn.microsoft.com/en-us/dotnet/api/system.console.readline?view=netframework-4.8.
             * 2) Zkonvertuje vstup od uzivatele ze stringu do integeru - https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number.
             * 3) Nacte vstup pro druhe cislo od uzivatele a zkonvertuje ho do integeru. (zopakovani kroku 1 a 2 pro druhe cislo)
             * 4) Nacte vstup pro ciselnou operaci. Rozmysli si, jak operace nazves. Muze to byt "soucet", "rozdil" atd. nebo napr "+", "-", nebo jakkoliv jinak.
             * 5) Nadefinuj integerovou promennou result a prirad ji prozatimne hodnotu 0.
             * 6) Vytvor podminky (if statement), podle kterych urcis, co se bude s cisly dit podle zadane operace
             *    a proved danou operaci - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements.
             * 7) Vypis promennou result do konzole
             * 
             * Mozna rozsireni programu pro rychliky / na doma (na poradi nezalezi):
             * 1) Vypis do konzole pred nactenim kazdeho uzivatelova vstupu co po nem chces
             * 2) a) Kontroluj, ze uzivatel do vstupu zadal, co mel (cisla, popr. ciselnou operaci). Pokud zadal neco jineho, napis mu, co ma priste zadat a program ukoncete.
             * 2) b) To same, co a) ale misto ukonceni programu opakovane cti vstup, dokud uzivatel nezada to, co ma
             *       - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement
             * 3) Umozni uzivateli zadavat i desetinna cisla, tedy prekopej kalkulacku tak, aby umela pracovat s floaty
             */

            //Tento komentar smaz a misto nej zacni psat svuj prdacky kod.

            float check; float FloInp2; float FloInp1;
            Console.WriteLine("Tohle je kalkulacka, napis prvni cislo");
            while (true)
            {
                string input1 = Console.ReadLine();
                bool success1 = float.TryParse(input1, out check);
                if (success1 == true)
                {
                    FloInp1 = float.Parse(input1);
                    Console.WriteLine("A ted napis druhe cislo");
                    break;
                }
                else
                {
                    Console.WriteLine("To neni cislo, napis cislo");
                }
            }
            while (true)
                {
                string input2 = Console.ReadLine();
                bool success2 = float.TryParse(input2, out check);
                if (success2 == true)
                {
                    FloInp2 = float.Parse(input2);
                    break;
                }
                else
                {
                    Console.WriteLine("To neni cislo, napis cislo");
                }
                
            }

            Console.WriteLine("Ted napis ciselnou operaci");
            string znamenko = Console.ReadLine();
            while (true)
            {
                    if (znamenko ==  "+")
                    {
                        Console.WriteLine("{0} + {1} = {2}", FloInp1, FloInp2, FloInp1 + FloInp2);
                    }
                    if (znamenko == "-")
                    {
                        Console.WriteLine("{0} - {1} = {2}", FloInp1, FloInp2, FloInp1 - FloInp2);
                    }
                    if (znamenko == "*")
                    {
                        Console.WriteLine("{0} * {1} = {2}", FloInp1, FloInp2, FloInp1 * FloInp2);
                    }
                    if (znamenko == "/")
                    {
                        Console.WriteLine("{0} / {1} = {2}", FloInp1, FloInp2, FloInp1 / FloInp2);
                    }
                Console.ReadKey(); //Toto nech jako posledni radek, aby se program neukoncil ihned, ale cekal na stisk klavesy od uzivatele.         
            }
            
        }
    }
}
