using System;
using System.Diagnostics.CodeAnalysis;
using CeVIO.Talk.RemoteService2;
using CommandLine;

namespace CevioAiCli.Cli;

[Verb("save")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class CliSave
{
    [Option(longName: "name", shortName: 'n', Required = true, HelpText = "Specify the talker name.")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Name { get; set; }

    [Option(longName: "volume", shortName: 'v', HelpText = "Specify the volume. (音量) value: 0-100")]
    public uint? Volume { get; set; }

    [Option(longName: "tone", shortName: 't', HelpText = "Specify the tone. (音の高さ) value: 0-100")]
    public uint? Tone { get; set; }

    [Option(longName: "tone-scale", shortName: 'T', HelpText = "Specify the tone scale. (抑揚) value: 0-100")]
    public uint? ToneScale { get; set; }

    [Option(longName: "alpha", shortName: 'a', HelpText = "Specify the alpha. (声質) value: 0-100")]
    public uint? Alpha { get; set; }

    [Option(longName: "speed", shortName: 's', HelpText = "Specify the speed. (話す速さ) value: 0-100")]
    public uint? Speed { get; set; }

    
    [Option(Group = "input")]
    public string? Text { get; set; }
    
    [Option(Group = "input")]
    public string? File { get; set; }
    
    [Option(longName: "output-file", shortName: 'o', Required = true)]
    public string? OutputFile { get; set; }
    
    public void Execute()
    {
        ServiceControl2.StartHost(false);
        var t = new Talker2(this.Name);
        t.Volume = this.Volume ?? t.Volume;
        t.Tone = this.Tone ?? t.Tone;
        t.ToneScale = this.ToneScale ?? t.ToneScale;
        t.Alpha = this.Alpha ?? t.Alpha;

        
        if (this.Text != null)
        {
            t.OutputWaveToFile(this.Text, this.OutputFile);            
        } else if (this.File != null)
        {
            var s = System.IO.File.OpenText(this.File).ReadToEnd();
            t.OutputWaveToFile(s, this.OutputFile);
        }
        else
        {
            throw new NotSupportedException();
        }
    }
}
