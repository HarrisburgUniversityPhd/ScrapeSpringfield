/// copy paste from https://stackoverflow.com/questions/2884551/get-individual-query-parameters-from-uri
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScrapeSpringfield.Tools
{
    static class UriExtensions
    {
        static readonly Regex _regex = new Regex(@"[?|&]([\w\.]+)=([^?|^&]+)", RegexOptions.Compiled);

        public static IReadOnlyDictionary<string, string> ParseQueryString(this Uri uri)
        {
            var match = _regex.Match(uri.PathAndQuery);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                paramaters.Add(match.Groups[1].Value.ToUpperInvariant(), match.Groups[2].Value);
                match = match.NextMatch();
            }
            return paramaters;
        }
    }
}
