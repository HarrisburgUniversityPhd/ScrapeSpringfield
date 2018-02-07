using HtmlAgilityPack;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeSpringfield.Tools
{
    static class HtmlDocumentExtensions
    {
        const string _titleXPath = "//body//div//div[@id='content_container']//div[@class='main-content']//div[@class='main-content-left']//h1";
        const string _scriptXPath = "//body//div//div[@id='content_container']//div[@class='main-content']//div[@class='main-content-left']//div//div[@class='scrolling-script-container']";

        public static async Task ParseScriptAsync(this HtmlDocument document, string saveLocation)
        {
            var dir = Path.GetDirectoryName(saveLocation);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var writer = new StreamWriter(saveLocation, false, Encoding.UTF8))
            {
                var title = document.DocumentNode.SelectSingleNode(_titleXPath).InnerText;
                await writer.WriteLineAsync(title.Trim());

                await writer.WriteLineAsync();
                await writer.WriteLineAsync();

                var script = document.DocumentNode.SelectSingleNode(_scriptXPath);
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
