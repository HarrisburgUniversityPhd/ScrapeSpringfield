using ScrapeSpringfield.Models;
using ScrapeSpringfield.Tools;
using System;
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

            var urlhelpres = new UrlHelpers(_config);

            using (var client = new HttpClient())
            {
                var sitemap = urlhelpres.SiteMap;
                Console.WriteLine($"Downloading sitmap {sitemap}");
                var links = await client.DownloadAndParseSiteMapAsync(sitemap);

                foreach (var link in links.Where(p => urlhelpres.ShouldFollow(p)))
                {
                    Console.WriteLine($"Downloading script {link.PathAndQuery}");
                    var script = await client.DownloadScriptAsync(link);

                    if (script != null)
                    {
                        Console.WriteLine($"Parsing script {link.PathAndQuery}");
                        await script.ParseScriptAsync(urlhelpres.SaveLocation(link));
                    }
                }
            }
        }
    }
}
