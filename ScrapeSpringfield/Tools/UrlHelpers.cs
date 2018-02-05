using ScrapeSpringfield.Models;
using System;
using System.IO;

namespace ScrapeSpringfield.Tools
{
    class UrlHelpers
    {
        const string _movies = "https://www.springfieldspringfield.co.uk/sitemaps/movies.xml";
        const string _tv = "https://www.springfieldspringfield.co.uk/sitemaps/tv_shows.xml";
        const string _moviesPath = "/movie_script.php?movie=";
        const string _tvPath = "/view_episode_scripts.php?tv-show=";

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
                    name = MovieUrl.Parse(url).Name;
                    break;
                case Models.Type.TV:
                    name = TVUrl.Parse(url).Name;
                    break;
                default:
                    return false;
            }

            if (name != null)
            {
                var b1 = name.StartsWith(_config.Start);
                var b2 = name.StartsWith(_config.End);
                var b3 = string.Compare(_config.Start, name, true) <= 0;
                var b4 = string.Compare(name, _config.End, true) <= 0;

                return (b1 || b3) && (b2 || b4);
            }
            else
                return false;
        }

        public string SaveLocation(Uri url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            switch (_config.Type)
            {
                case Models.Type.Movie:
                    var movie = MovieUrl.Parse(url);   
                    return Path.Combine(_config.SaveLocation, "movie", $"{movie.Name}.txt");
                case Models.Type.TV:
                    var tv = TVUrl.Parse(url);
                    return Path.Combine(_config.SaveLocation, "tv", tv.Name, $"{tv.Episode}.txt");
                default:
                    return Path.Combine(_config.SaveLocation, Guid.NewGuid().ToString());
            }
        }

        string QueryPath()
        {
            if (_config.Type == Models.Type.Movie)
                return _moviesPath;
            else if (_config.Type == Models.Type.Movie)
                return _tvPath;
            else
                return null;
        }
        string Query(Uri url)
        {
            var path = QueryPath();
            if ((path != null) && (url.PathAndQuery.StartsWith(path)))
                return url.PathAndQuery.Substring(path.Length);
            else
                return null;
        }
    }
}
