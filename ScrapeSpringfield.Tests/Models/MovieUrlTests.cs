using ScrapeSpringfield.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapeSpringfield.Tests.Models
{
    public class MovieUrlTests
    {
        [Fact]
        public void ParseFull()
        {
            var url = "https://www.springfieldspringfield.co.uk/movie_script.php?movie=1929-the-great-crash";

            var b1 = MovieUrl.TryParse(new Uri(url), out MovieUrl movie);

            Assert.True(b1);
            Assert.Equal("1929-the-great-crash", movie.Name);
        }
        [Fact]
        public void ParseOrder()
        {
            var url = "https://www.springfieldspringfield.co.uk/movie_scripts.php?order=0";

            var b1 = MovieUrl.TryParse(new Uri(url), out MovieUrl movie);

            Assert.False(b1);
            Assert.Null(movie);
        }
        [Fact]
        public void ParseRoot()
        {
            var url = "https://www.springfieldspringfield.co.uk";

            var b1 = MovieUrl.TryParse(new Uri(url), out MovieUrl movie);

            Assert.False(b1);
            Assert.Null(movie);
        }
    }
}
