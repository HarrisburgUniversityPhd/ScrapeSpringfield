using ScrapeSpringfield.Models;
using System;
using Xunit;

namespace ScrapeSpringfield.Tests.Models
{
    public class TVUrlTests
    {
        [Fact]
        public void ParseFull()
        {
            var url = "https://www.springfieldspringfield.co.uk/view_episode_scripts.php?tv-show=10-things-i-hate-about-you&episode=s01e01";

            var b1 = TVUrl.TryParse(new Uri(url), out TVUrl tv);

            Assert.True(b1);
            Assert.Equal("10-things-i-hate-about-you", tv.Name);
            Assert.Equal("s01e01", tv.Episode);
        }
        [Fact]
        public void ParseSeason()
        {
            var url = "https://www.springfieldspringfield.co.uk/episode_scripts.php?tv-show=10-things-i-hate-about-you&amp;season=1";

            var b1 = TVUrl.TryParse(new Uri(url), out TVUrl tv);

            Assert.False(b1);
            Assert.Null(tv);
        }
        [Fact]
        public void ParseTitle()
        {
            var url = "https://www.springfieldspringfield.co.uk/episode_scripts.php?tv-show=10-things-i-hate-about-you";

            var b1 = TVUrl.TryParse(new Uri(url), out TVUrl tv);

            Assert.False(b1);
            Assert.Null(tv);
        }
        [Fact]
        public void ParseOrder()
        {
            var url = "https://www.springfieldspringfield.co.uk/tv_show_episode_scripts.php?order=Z";

            var b1 = TVUrl.TryParse(new Uri(url), out TVUrl tv);

            Assert.False(b1);
            Assert.Null(tv);
        }
        [Fact]
        public void ParseRoot()
        {
            var url = "https://www.springfieldspringfield.co.uk";

            var b1 = TVUrl.TryParse(new Uri(url), out TVUrl tv);

            Assert.False(b1);
            Assert.Null(tv);
        }
    }
}
