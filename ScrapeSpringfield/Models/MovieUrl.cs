using ScrapeSpringfield.Tools;
using System;

namespace ScrapeSpringfield.Models
{
    class MovieUrl
    {
        const string _moviesPath = "/movie_script.php?movie=";
        const string _name = "MOVIE";

        MovieUrl(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public static bool TryParse(Uri url, out MovieUrl result)
        {
            result = null;

            if (url == null) return false;

            var hash = url.ParseQueryString();

            if (!hash.ContainsKey(_name)) return false;

            result = new MovieUrl(hash[_name]);
            return true;
        }
    }
}
