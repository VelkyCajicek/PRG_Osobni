using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> test = new Dictionary<string, string>
            {
                { @"Vymažte vsechny soubory s priponou.BAK, které lezí v pracovnim adresári. (C:\\TEXT602)", @"DEL C: \\TEXT602\\* .BAK" },
                { @"V adresári TEXT602 na pevném disku C vymazte vsechny soubory s priponou.BAK (C:\)", @"DEL C: \\TEXT602\\*.BAK" },
                { @"V hlavnim adresári na disku A vymazte soubor DOPIS1.TXT (C:\)", @"DEL A:\DOPIS1. TXT"},
                { @"V adresári TPASCAL na pevném disku C vymazte vechny soubory s priponou DOC. (C:\)", @"DEL C:\TPASCAL\*.DOC" },
                { @"V adresári TEXT602 na disku C vymate soubor TEXT.BAK. (C:\)", @"DEL C:\TEXT602\TEXT.BAK" },
                { @"V adresári ARCHIV na disku A vymazte vsechny soubory, jejichz nazev zaciná na TEX a maji libovolnou priponu. (C:\)", @"DEL A:\ARCHIV\TEX*.*"},
                { @"V adresári PROGRAMY na disku C vymazte vsechny soubory s priponou EXE. (A:\)", @"DEL C: \PROGRAMY\* .EXE"},
                { @"Vypiste obsah soubor AUTOEXEC.BAT z hlavního adresáre na disku C. (C:\TOOLS)", @"TYPE C:\AUTOEXEC.BAT" },
                { @"Vypiste obsah soubor TX.BAT z hlavního adresáre na disku C. (C:\TEXT602)", @"TYPE C:\TX.BAT" },
                { @"Vypiste obsah souboru CTIMNE.TXT z hlavního adresáre na disku C. (C:\TOOLS)", @"TYPE C:\CTIMNE. TXT" },
                { @"Vypiste obsah souboru TURBO.TXT z adresáre PROGRAMY na pevném disku C. (C:\)", @"TYPE C:\PROGRAMY\TURBO.TXT" },
                { @"Prekopirujte soubor TEXT.TXT z pracovniho adresáre na disku cdo hlavniho adresáre na disku A. Novy název souboru je ZPRAVA.TXT. (C:\TEXT602)",
                  @"COPY C:NTEXT602\TEXT.TXT A:\ZPRAVA.TXT"},
                { @"Prekopirujte soubor ZPRAVA.TXT z hlavního adresáre na disku A do C:\TEXT602. Novy název souboru je TEXT.TXT" ,
                  @"COPY A:\PRAVA.TXT C:\TEXT602\TEXT.TXT" }
            }; // Always (question (path) : answer)
        }
    }
}
