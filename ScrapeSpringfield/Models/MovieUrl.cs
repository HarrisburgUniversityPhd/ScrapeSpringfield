using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeSpringfield.Models
{
    class MovieUrl
    {
        const string _moviesPath = "/movie_script.php?movie=";


        MovieUrl(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public static MovieUrl Parse(Uri url)
        {
            var res = new MovieUrl(null);

            return res;
        }
    }
}
