using HtmlAgilityPack;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScrapeSpringfield.Tools
{
    static class HttpClientExtensions
    {
        const string _titleXPath = "//body//div//div[@id='content_container']//div[@class='main-content']//div[@class='main-content-left']//h1";
        const string _scriptXPath = "//body//div//div[@id='content_container']//div[@class='main-content']//div[@class='main-content-left']//div//div[@class='scrolling-script-container']";
        static readonly XName _sitemapSelector = XName.Get("loc", "http://www.sitemaps.org/schemas/sitemap/0.9");

        public static async Task<IList<Uri>> DownloadAndParseSiteMapAsync(this HttpClient client, Uri url)
        {
            XDocument doc = null;

            await Policy.Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    using (var stream = await client.GetStreamAsync(url))
                        doc = XDocument.Load(stream, LoadOptions.None);
                });

            if (doc == null)
                return new List<Uri>();

            var links = doc.Descendants(_sitemapSelector)
                .OrderBy(p => p.Value)
                .Select(p => new Uri(p.Value))
                .ToList();

            return links;
        }
        public static async Task DownloadAndParseScript(this HttpClient client, Uri url, string saveLocation)
        {
            string content = null;

            await Policy.Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    content = await client.GetStringAsync(url);
                });

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            var dir = Path.GetDirectoryName(saveLocation);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var writer = new StreamWriter(saveLocation, false, Encoding.UTF8))
            {
                var title = doc.DocumentNode.SelectSingleNode(_titleXPath).InnerText;
                await writer.WriteLineAsync(title.Trim());

                await writer.WriteLineAsync();
                await writer.WriteLineAsync();

                var script = doc.DocumentNode.SelectSingleNode(_scriptXPath);
                foreach (var node in script.ChildNodes.Where(p => p.Name == "#text"))
                {
                    var line = node.InnerText?.Trim();
                    if (!string.IsNullOrWhiteSpace(line))
                        await writer.WriteLineAsync(line);
                }
            }
        }
    }
}
