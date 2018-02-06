using ScrapeSpringfield.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapeSpringfield.Tests.Tools
{
    public class UrlHelpersTests
    {
        [Fact]
        public void DoubleParameters()
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
        public void SingleParameter()
        {
            var url = "http://localhost/foo.aspx?a=xxx";

            var parameters = new Uri(url).ParseQueryString();

            Assert.Equal(1, parameters.Count);
            Assert.True(parameters.ContainsKey("A"));
            Assert.Equal("xxx", parameters["A"]);
        }
        [Fact]
        public void NoParameters()
        {
            var url = "http://localhost/foo.aspx";

            var parameters = new Uri(url).ParseQueryString();

            Assert.Equal(0, parameters.Count);
        }
    }
}
