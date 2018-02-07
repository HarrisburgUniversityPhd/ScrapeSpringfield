using HtmlAgilityPack;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScrapeSpringfield.Tools
{
    static class HttpClientExtensions
    {
        static readonly XName _sitemapSelector = XName.Get("loc", "http://www.sitemaps.org/schemas/sitemap/0.9");

        public static async Task<IList<Uri>> DownloadAndParseSiteMapAsync(this HttpClient client, Uri url)
        {
            XDocument doc = null;

            await Policy.Handle<HttpRequestException>()
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
        public static async Task<HtmlDocument> DownloadScriptAsync(this HttpClient client, Uri url)
        {
            var doc = new HtmlDocument();

            var res = await Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAndCaptureAsync(async () =>
                {
                    var msg = await client.GetAsync(url);
                    if (msg.IsSuccessStatusCode)
                        doc.LoadHtml(await msg.Content.ReadAsStringAsync());
                    else if (msg.StatusCode == HttpStatusCode.Gone || msg.StatusCode == HttpStatusCode.NotFound)
                        doc = null;
                    else
                        throw new HttpRequestException($"Error {msg.StatusCode} retrieving url {url}");
                });

            if (res.Outcome == OutcomeType.Successful)
                return doc;
            else
                return null;
        }
    }
}