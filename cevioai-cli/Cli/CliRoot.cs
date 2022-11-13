using System;
using System.Diagnostics.CodeAnalysis;
using CeVIO.Talk.RemoteService2;
using CommandLine;

namespace CevioAiCli.Cli
{
    public class CliRoot
    {
        public static void Parse(string[] args)
        {
            var parseResult = Parser.Default.ParseArguments <CliList, CliPlay, CliInfo, CliSave>(args);

            parseResult
                .WithParsed<CliList>(op => op.Execute())
                .WithParsed<CliPlay>(op => op.Execute())
                .WithParsed<CliInfo>(op => op.Execute())
                .WithParsed<CliSave>(op => op.Execute());
        }
    }
}
