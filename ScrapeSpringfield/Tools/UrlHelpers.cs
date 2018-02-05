using ScrapeSpringfield.Models;
using System;
using System.IO;

namespace ScrapeSpringfield.Tools
{
    class UrlHelpers
    {
        const string _movies = "https://www.springfieldspringfield.co.uk/sitemaps/movies.xml";
        const string _tv = "https://www.springfieldspringfield.co.uk/sitemaps/tv_shows.xml";

        Configuration _config;

        public UrlHelpers(Configuration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public Uri SiteMap
        {
            get
            {
                switch (_config.Type)
                {
                    case Models.Type.Movie:
                        return new Uri(_movies);
                    case Models.Type.TV:
                        return new Uri(_tv);
                    default:
                        throw new Exception($"Unknown Model Type: {_config.Type}");
                }
            }
        }

        public bool ShouldFollow(Uri url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            string name;
            switch (_config.Type)
            {
                case Models.Type.Movie:
                    name = MovieUrl.TryParse(url, out MovieUrl movie) ? movie.Name : null;
                    break;
                case Models.Type.TV:
                    name = TVUrl.TryParse(url, out TVUrl tv) ? tv.Name : null;
                    break;
                default:
                    return false;
            }

            if (name == null)
                return false;
            else
            {
                var b1 = name.StartsWith(_config.Start);
                var b2 = name.StartsWith(_config.End);
                var b3 = string.Compare(_config.Start, name, true) <= 0;
                var b4 = string.Compare(name, _config.End, true) <= 0;

                return (b1 || b3) && (b2 || b4);
            }
        }
        public string SaveLocation(Uri url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            var defLoc = Path.Combine(_config.SaveLocation, $"{ Guid.NewGuid()}.txt");

            switch (_config.Type)
            {
                case Models.Type.Movie:
                    return MovieUrl.TryParse(url, out MovieUrl movie) ? Path.Combine(_config.SaveLocation, "movie", $"{movie.Name}.txt") : defLoc;
                case Models.Type.TV:
                    return TVUrl.TryParse(url, out TVUrl tv) ? Path.Combine(_config.SaveLocation, "tv", tv.Name, $"{tv.Episode}.txt") : defLoc;
                default:
                    return defLoc;
            }
        }
    }
}
