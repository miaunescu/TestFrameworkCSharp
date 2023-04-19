using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFrame.Tests.TheCatAPITests;

namespace TestFrame.Fixtures
{
    public class TheCatApiTestFixture : RestClientFixture
    {
        public string ApiKey { get; set; }
        public string Api { get; set; }
        public int VoteId { get; set; }
        public string CorrelationId { get; set; } = $"{typeof(TheCatAPITests).FullName}-{Guid.NewGuid()}";

    }
}
