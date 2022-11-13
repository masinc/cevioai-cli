using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CeVIO.Talk.RemoteService2;
using CommandLine;
using Newtonsoft.Json;

namespace CevioAiCli.Cli;

[Verb("info")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class CliInfo
{
    [Option(longName: "name", shortName: 'n', Required = true, HelpText = "Specify the talker name.")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Name { get; set; }
    
    [Option(longName: "format", shortName: 'f',  Default = Cli.Format.text)]
    public Format Format { get; set; }
    
    public void Execute()
    {
        ServiceControl2.StartHost(false);

        var t = new Talker2(this.Name);
        
        switch (this.Format)
        {
            case Format.text:

                Console.WriteLine($"Name: {t.Cast}");
                Console.WriteLine($"Speed: {t.Speed}");
                Console.WriteLine($"Volume {t.Volume}");
                Console.WriteLine($"Tone: {t.Tone}");
                Console.WriteLine($"Alpha: {t.Alpha}");
                Console.WriteLine($"ToneScale: {t.ToneScale}");
                Console.WriteLine("Component:");
                
                foreach (var c in t.Components)
                {
                    Console.WriteLine($"\t{c.Id}: {c.Name}\t{c.Value}");
                }

                break;
            case Format.json:
                var v = CliListVerboseOutput.From(t);
                Console.WriteLine(JsonConvert.SerializeObject(v));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        } 
        
    }
}
