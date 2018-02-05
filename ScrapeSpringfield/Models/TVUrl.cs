using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeSpringfield.Models
{
    class TVUrl
    {
        const string _tvPath = "/view_episode_scripts.php?tv-show=";

        TVUrl(string name, string episode)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Episode = name ?? throw new ArgumentNullException(nameof(episode));
        }

        public string Name { get; }
        public string Episode { get; }

        public static TVUrl Parse(Uri url)
        {
            var res = new TVUrl(null, null);

            return res;
        }
    }
}
