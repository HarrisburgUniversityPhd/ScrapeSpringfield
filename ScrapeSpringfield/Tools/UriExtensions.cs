using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrapeSpringfield.Tools
{
    static class UriExtensions
    {
        public static IReadOnlyDictionary<string, string> ParseQueryString(this Uri uri)
        {
            var nvc = HttpUtility.ParseQueryString(uri.Query);

            return nvc.AllKeys.ToDictionary(p => p.ToUpperInvariant(), p => nvc[p]);
        }
    }
}
