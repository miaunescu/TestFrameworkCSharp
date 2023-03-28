using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFrame.Fixtures
{
    public class CatsTestsFixture : RestClientFixture
    {
        public int Limit { get; set; }
        public string Api { get; set; }

    }
}
