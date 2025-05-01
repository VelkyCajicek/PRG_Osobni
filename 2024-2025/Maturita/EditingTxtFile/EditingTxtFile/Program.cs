using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditingTxtFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"\test.txt"; // Musn't forget '\' otherwise it will be placed into users

            string pathSpecial = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Special folders (Desktop, Documents, Programs)
            string pathCurrentDir = Directory.GetCurrentDirectory(); // Gets current dir
            string pathSpecific = @"some/path/to/directory"; // Must use @ otherwise '\' will throw an error

            string path = pathSpecial + fileName;
            string[] text = { "Check", "if", "this", "works" }; // Random sequence to print into .txt file
            Console.WriteLine(path);

            // OPTION 1
            WriteToTxt(path, text);
            ReadTxt(path);
            
            // OPTION 2
            string createText = string.Join(" ", text);
            File.WriteAllText(path, createText);
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);

            /*
             - Why not use this: 
            
            private void Form1_Load(object sender, EventArgs e)
            {
            souborjmena1 = new StreamWriter(&quot;jmena.txt&quot;);
            souborjmena1.Write(&quot;&quot;);
            souborjmena1.Close();
            }

            - It will not close file/reader/writer in case of exceptions (in other words it's safer)
             */

            if (File.Exists(path)) File.Delete(path); // Delete file after you are done
            Console.ReadKey();
        }
        static void WriteToTxt(string path, string[] textToWrite)
        {
            if (!File.Exists(path)) // If file exists, don't create another one
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    for (int i = 0; i < textToWrite.Length; i++)
                    {
                        sw.WriteLine(textToWrite[i]);
                    }
                }
            }
        }
        static void ReadTxt(string path)
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = ""; // Placeholder string
                while ((s = sr.ReadLine()) != null) // Technically doesn't have to be there, but more for safety
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
