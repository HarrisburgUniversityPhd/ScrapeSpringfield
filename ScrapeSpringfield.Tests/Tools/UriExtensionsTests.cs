using ScrapeSpringfield.Tools;
using System;
using Xunit;

namespace ScrapeSpringfield.Tests.Tools
{
    public class UriExtensionsTests
    {
        [Fact]
        public void NoParameters()
        {
            var url = "http://localhost/foo.aspx";

            var parameters = new Uri(url).ParseQueryString();

            Assert.Equal(0, parameters.Count);
        }
        [Fact]
        public void OneParameter()
        {
            var url = "http://localhost/foo.aspx?a=xxx";

            var parameters = new Uri(url).ParseQueryString();

            Assert.Equal(1, parameters.Count);
            Assert.True(parameters.ContainsKey("A"));
            Assert.Equal("xxx", parameters["A"]);
        }
        [Fact]
        public void ManyParameters()
        {
            var url = "http://localhost/foo.aspx?a=xxx&c=yyy";

            var parameters = new Uri(url).ParseQueryString();

            Assert.Equal(2, parameters.Count);
            Assert.True(parameters.ContainsKey("A"));
            Assert.True(parameters.ContainsKey("C"));
            Assert.Equal("xxx", parameters["A"]);
            Assert.Equal("yyy", parameters["C"]);
        }
        [Fact]
        public void ManyParametersIrl()
        {
            var url = "https://www.springfieldspringfield.co.uk/view_episode_scripts.php?tv-show=10-things-i-hate-about-you&episode=s01e01";

            var parameters = new Uri(url).ParseQueryString();

            Assert.Equal(2, parameters.Count);
            Assert.True(parameters.ContainsKey("TV-SHOW"));
            Assert.True(parameters.ContainsKey("EPISODE"));
            Assert.Equal("10-things-i-hate-about-you", parameters["TV-SHOW"]);
            Assert.Equal("s01e01", parameters["EPISODE"]);
        }
        [Fact]
        public void ParameterWithTrailingHash()
        {
            var url = "http://localhost/foo.aspx?a=xxx&c=yyy#foo";

            var parameters = new Uri(url).ParseQueryString();

            Assert.Equal(2, parameters.Count);
            Assert.True(parameters.ContainsKey("A"));
            Assert.True(parameters.ContainsKey("C"));
            Assert.Equal("xxx", parameters["A"]);
            Assert.Equal("yyy", parameters["C"]);
        }
    }
}
