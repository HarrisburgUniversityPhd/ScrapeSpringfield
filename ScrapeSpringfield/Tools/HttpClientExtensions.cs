using Polly;
using ScrapeSpringfield.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScrapeSpringfield.Tools
{
    static class HttpClientExtensions
    {
        public static async Task<string> DownloadAndUnzipIndexAsync(this HttpClient client, string url)
        {
            var tmpfile = Path.GetTempFileName();
            var unzippath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

            await Policy.Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    using (var streamin = await client.GetStreamAsync(url))
                    using (var streamout = new FileStream(tmpfile, FileMode.Open, FileAccess.Write))
                        await streamin.CopyToAsync(streamout);
                });

            File.Delete(tmpfile);

            return Path.Combine(unzippath, "master.idx");
        }
    }
}
