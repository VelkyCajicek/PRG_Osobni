using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace HomeWork3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var codeToEval = @"
                int test = 0;
                var count = test + 15;
                count++;
                return count;";

            var options = ScriptOptions.Default;
            var result = CSharpScript.EvaluateAsync(codeToEval, options);
        }
    }
}
