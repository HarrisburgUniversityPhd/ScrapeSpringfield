using ScrapeSpringfield.Tools;
using System;

namespace ScrapeSpringfield.Models
{
    class TVUrl
    {
        const string _name = "TV-SHOW";
        const string _episode = "EPISODE";


        TVUrl(string name, string episode)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Episode = episode ?? throw new ArgumentNullException(nameof(episode));
        }

        public string Name { get; }
        public string Episode { get; }

        public static bool TryParse(Uri url, out TVUrl result)
        {
            result = null;

            if (url == null) return false;

            var hash = url.ParseQueryString();

            if (!hash.ContainsKey(_name)) return false;
            if (!hash.ContainsKey(_episode)) return false;

            result = new TVUrl(hash[_name], hash[_episode]);
            return true;
        }
    }
}
