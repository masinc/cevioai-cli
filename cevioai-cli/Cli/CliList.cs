using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CeVIO.Talk.RemoteService2;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CevioAiCli.Cli
{
    [JsonObject]
    public class CliListSimpleOutput
    {
        [JsonProperty("name")] 
        public string? Name { get; set; }
    }

    [JsonObject]
    public class Component
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("value")]
        public uint? Value { get; set; }


        public static Component From(TalkerComponent2 component)
        {
            return new Component()
            {
                Id = component.Id,
                Name = component.Name,
                Value = component.Value
            };
        }
    }
    
    
    [JsonObject]
    public class CliListVerboseOutput
    {
        [JsonProperty("name")] 
        public string? Name { get; set; }
        
        [JsonProperty("components")]
        public List<Component>? Components { get; set; }

        public static CliListVerboseOutput From(Talker2 talker)
        {
            return new CliListVerboseOutput()
            {
                Name = talker.Cast,
                Components = talker.Components.Select(Component.From).ToList()
            };
        }
    }


    [Verb("list")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class CliList
    {
        [Option(longName: "verbose", shortName: 'v', Default = false)]
        public bool Verbose { get; set; }

        [Option(longName: "format", shortName: 'f', Default = Cli.Format.text)]
        public Format Format { get; set; }

        public void Execute()
        {
            ServiceControl2.StartHost(false);

            if (!this.Verbose)
            {
                var v = TalkerAgent2.AvailableCasts.Select(t => new CliListSimpleOutput() { Name = t }).ToList();
                switch (this.Format)
                {
                    case Format.text:
                        foreach (var t in v)
                        {
                            Console.WriteLine(t.Name);
                        }

                        break;
                    case Format.json:
                        Console.WriteLine(JsonConvert.SerializeObject(v));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                var v = TalkerAgent2.AvailableCasts.Select(name => new Talker2(name)).Select(CliListVerboseOutput.From)
                    .ToList();

                switch (this.Format)
                {
                    case Format.text:
                        foreach (var t in v)
                        {
                            var componentNames = String.Join(",", t.Components!.Select(c => c.Name));
                            Console.WriteLine($"{t.Name}\t{componentNames}");
                        }
                        break;
                    case Format.json:
                        Console.WriteLine(JsonConvert.SerializeObject(v));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
