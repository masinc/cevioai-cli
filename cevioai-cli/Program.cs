using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CevioAiCli.Cli;

namespace CevioAiCli
{

    internal class Program
    {
        static void Main(string[] args)
        {
            CliRoot.Parse(args);
        }
    }
}
