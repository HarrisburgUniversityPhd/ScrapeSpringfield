using ScrapeSpringfield.Models;
using ScrapeSpringfield.Tools;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScrapeSpringfield.Jobs
{
    class ScrapeJob
    {
        Configuration _config;

        public ScrapeJob(Configuration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task ScrapeAsync()
        {
            Console.WriteLine($"Getting all the {_config.Type}s for {_config.Start} to {_config.End}");

            await Task.Delay(0);
        }
    }
}
