using Mono.Options;
using System;
using System.IO;

namespace ScrapeSpringfield.Models
{
    class Configuration
    {
        public Type Type { get; }
        public string Start { get; }
        public string End { get; }
        public string SaveLocation { get; }

        public Configuration(string[] arrs)
        {
            var type = "tv";
            var start = "0";
            var end = "z";
            var save = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "springfield");

            var options = new OptionSet {
                { "t|type=", "The type of scripts to download.", p => type =  p },
                { "s|start=", "The first letter of the show's name from which to start the pull (inclusive).", p => start = p },
                { "e|end=", "The first letter of the show's name on which to end the pull (inclusive).", p => end = p },
                { "p|path=", "The path to save files to.", p => save = p }
            };

            var extras = options.Parse(arrs);

            if (Enum.TryParse<Type>(type, true, out var tmp))
                Type = tmp;
            else
                throw new ArgumentException($"Expected: movie/tv. Found: {type}", nameof(Type));
            Start = start;
            End = end;
            SaveLocation = save;
        }
    }
}
